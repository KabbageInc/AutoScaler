using SimpleAWS;
using SimpleAWS.Models.EC2;
using SimpleAWS.Models.SQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;

namespace AutoScaler
{
    public class AutoScaler
    {
        private static System.Timers.Timer dispatcher;
        private static DateTime? emptyQueueStartTime = null;

        private static string AWSSQSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static string AWSSQSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        private static string AWSEC2AccessKey = ConfigurationManager.AppSettings["AWSEC2AccessKey"];
        private static string AWSEC2SecretKey = ConfigurationManager.AppSettings["AWSEC2SecretKey"];

        private static int CheckInterval = Int32.Parse(ConfigurationManager.AppSettings["CheckInterval"]);
        private static string queueUrl = ConfigurationManager.AppSettings["QueueUrl"];
        private static string ImageId = ConfigurationManager.AppSettings["ImageId"];
        private static string SecurityGroupId = ConfigurationManager.AppSettings["SecurityGroupId"];
        private static string SubnetId = ConfigurationManager.AppSettings["SubnetId"];
        private static int NoActivityShutdownMinutes = Int32.Parse(ConfigurationManager.AppSettings["NoActivityShutdownMinutes"]);
        private static int ServerSpeedByHour = Int32.Parse(ConfigurationManager.AppSettings["ServerSpeedByHour"]);
        private static int MaxServersAllowed = Int32.Parse(ConfigurationManager.AppSettings["MaxServersAllowed"]);

        private static SQSClient sqsClient = null;
        private static EC2Client ec2Client = null;

        private static void WriteError(string message)
        {
            if (!EventLog.SourceExists("AutoScaler"))
                EventLog.CreateEventSource("AutoScaler", "Application");

            EventLog.WriteEntry("AutoScaler", message, EventLogEntryType.Error);
        }

        public static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;

            if (!Environment.UserInteractive)
            {
                using (var service = new Service())
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        public static void Start(string[] args)
        {
            sqsClient = new SQSClient(AWSSQSAccessKey, AWSSQSSecretKey, 4);
            ec2Client = new EC2Client(AWSEC2AccessKey, AWSEC2SecretKey, 4);

            dispatcher = new System.Timers.Timer(CheckInterval);
            dispatcher.Elapsed += dispatcher_Elapsed;
            dispatcher.Start();
        }

        public static void Stop()
        {
            if (dispatcher != null)
                dispatcher.Dispose();
        }

        private static int NeededServers(int messagecount, int instancecount)
        {
            double totalServers;

            if (messagecount <= ServerSpeedByHour)
                totalServers = 1;
            else
                totalServers = Math.Min(Math.Ceiling((double)messagecount / (double)ServerSpeedByHour), MaxServersAllowed);

            return (int)(((totalServers - instancecount) > 0) ? (totalServers - instancecount) : 0);
        }

        private static void dispatcher_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            dispatcher.Stop();

            int instanceCount = 0;
            int messageCount = 0;
            bool emptyQueue = true;
            List<string> instanceIds = new List<string>();
            GetQueueAttributesResponse queueResponse = null;
            DescribeInstancesResponse instanceResponse = null;

            try
            {
                queueResponse = sqsClient.GetQueueAttributes(new GetQueueAttributesRequest { QueueUrl = queueUrl, AttributeNames = new List<string> { "ApproximateNumberOfMessages", "ApproximateNumberOfMessagesNotVisible", "ApproximateNumberOfMessagesDelayed" } });

                var filter = new Filter();
                filter.Name = "image-id";
                filter.Values.Add(ImageId);

                emptyQueue = (queueResponse.GetQueueAttributesResult.ApproximateNumberOfMessages == 0 && queueResponse.GetQueueAttributesResult.ApproximateNumberOfMessagesNotVisible == 0 && queueResponse.GetQueueAttributesResult.ApproximateNumberOfMessagesDelayed == 0);
                messageCount = queueResponse.GetQueueAttributesResult.ApproximateNumberOfMessages;

                instanceResponse = ec2Client.DescribeInstances(new DescribeInstancesRequest { Filters = new List<Filter> { filter } });

                var allInstances = instanceResponse.Reservations.SelectMany(x => x.Instances).Where(x => x.State.Name != InstanceStateName.terminated);

                instanceCount = allInstances.Count();
                instanceIds = allInstances.Select(i => i.InstanceId).ToList();
            }
            catch (Exception exc)
            {
                queueResponse = null;
                WriteError(string.Format("Exception getting instance details: {0}", exc.ToString()));
            }

            if (queueResponse != null)
            {
                if (instanceCount > 0 && emptyQueue)
                {
                    if (!emptyQueueStartTime.HasValue)
                    {
                        emptyQueueStartTime = DateTime.Now;
                    }
                    else if ((DateTime.Now - emptyQueueStartTime.Value).TotalMinutes >= NoActivityShutdownMinutes)
                    {
                        try
                        {
                            ec2Client.TerminateInstances(new TerminateInstancesRequest { InstanceIds = instanceIds });
                        }
                        catch (Exception exc)
                        {
                            WriteError(string.Format("Exception terminated instances: {0}", exc.ToString()));
                        }

                        emptyQueueStartTime = null;
                    }
                }
                else if (!emptyQueue)
                {
                    emptyQueueStartTime = null;

                    int neededServers = NeededServers(messageCount, instanceCount);

                    if (neededServers > 0)
                    {
                        try
                        {
                            ec2Client.RunInstances(new RunInstancesRequest
                            {
                                ImageId = ImageId,
                                MinCount = neededServers,
                                MaxCount = neededServers,
                                SecurityGroupId = new List<string> { SecurityGroupId },
                                InstanceType = InstanceType.m3large,
                                SubnetId = SubnetId,
                            });
                        }
                        catch (Exception exc)
                        {
                            WriteError(string.Format("Exception running instances: {0}", exc.ToString()));
                        }
                    }
                }
            }

            dispatcher.Start();
        }
    }
}
