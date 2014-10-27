using SimpleAWS.Models;
using SimpleAWS.Models.EC2;
using SimpleAWS.Models.S3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleAWS
{
    public class EC2Client
    {
        private string URL { get; set; }
        private string AccessKey { get; set; }
        private string SecretKey { get; set; }
        private int RetryCount { get; set; }

        public EC2Client(string accessKey, string secretKey, int retryCount = 0, string url = "https://ec2.amazonaws.com/")
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            URL = url;
            AccessKey = accessKey;
            SecretKey = secretKey;
            RetryCount = retryCount;
        }

        public DescribeInstancesResponse DescribeInstances(DescribeInstancesRequest request)
        {
            return Util.RetryMethod<DescribeInstancesResponse>(() => DoDescribeInstances(request), RetryCount);
        }

        public void TerminateInstances(TerminateInstancesRequest request)
        {
            Util.RetryMethod(() => DoTerminateInstances(request), RetryCount);
        }

        public RunInstancesResponse RunInstances(RunInstancesRequest request)
        {
            return Util.RetryMethod<RunInstancesResponse>(() => DoRunInstances(request), RetryCount);
        }

        public DescribeInstancesResponse DoDescribeInstances(DescribeInstancesRequest request)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("Action={0}", "DescribeInstances"));
            lParams.Add(string.Format("SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("SignatureVersion={0}", "2"));
            lParams.Add(string.Format("Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("Version={0}", "2012-07-20"));

            for (int i = 0; i < request.Filters.Count; i++)
            {
                var filter = request.Filters[i];

                lParams.Add(string.Format("Filter.{0}.Name={1}", i + 1, Util.UrlEncode(filter.Name)));

                for (int a = 0; a < filter.Values.Count; a++)
                {
                    lParams.Add(string.Format("Filter.{0}.Value.{1}={2}", i + 1, a + 1, Util.UrlEncode(filter.Values[a])));
                }
            }

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("&", lParams);

            string sig = Util.GetSignature(URL, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", URL, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(DescribeInstancesResponse));
                var describeInstancesResponse = (DescribeInstancesResponse)serializer.Deserialize(element.CreateReader());
                return describeInstancesResponse;
            }
        }

        public void DoTerminateInstances(TerminateInstancesRequest request)
        {
            int PostMessageSize = 9;

            for (var i = 0; i < request.InstanceIds.Count; i += PostMessageSize)
            {
                List<string> chunk = request.InstanceIds.Skip(i).Take(PostMessageSize).ToList();

                List<string> lParams = new List<string>();

                lParams.Add(string.Format("AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
                lParams.Add(string.Format("Action={0}", "TerminateInstances"));
                lParams.Add(string.Format("SignatureMethod={0}", "HmacSHA256"));
                lParams.Add(string.Format("SignatureVersion={0}", "2"));
                lParams.Add(string.Format("Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
                lParams.Add(string.Format("Version={0}", "2012-07-20"));

                for (int a = 0; a < chunk.Count; a++)
                {
                    lParams.Add(string.Format("InstanceId.{0}={1}", a + 1, Util.UrlEncode(chunk[a])));
                }

                lParams.Sort(StringComparer.Ordinal);

                var parameters = string.Join("&", lParams);

                string sig = Util.GetSignature(URL, "GET", parameters, SecretKey);
                parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

                var wRequest = WebRequest.Create(string.Format("{0}?{1}", URL, parameters)) as HttpWebRequest;
                wRequest.Method = "GET";
                wRequest.ContentType = "application/x-www-form-urlencoded";
                wRequest.KeepAlive = false;

                using (var response = wRequest.GetResponse() as HttpWebResponse)
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var body = reader.ReadToEnd();
                    var element = XElement.Parse(body);

                    XmlSerializer serializer = new XmlSerializer(typeof(TerminateInstancesResponse));
                    var terminateInstancesResponse = (TerminateInstancesResponse)serializer.Deserialize(element.CreateReader());
                }
            }
        }

        public RunInstancesResponse DoRunInstances(RunInstancesRequest request)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("Action={0}", "RunInstances"));
            lParams.Add(string.Format("SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("SignatureVersion={0}", "2"));
            lParams.Add(string.Format("Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("Version={0}", "2012-07-20"));

            var propValues = typeof(RunInstancesRequest).GetProperties();

            for (int i = 0; i < propValues.Length; i++)
            {
                if (propValues[i].PropertyType.IsValueType || propValues[i].PropertyType == typeof(string))
                {
                    var value = propValues[i].GetValue(request);

                    if (propValues[i].PropertyType == typeof(bool))
                        value = value.ToString().ToLower();

                    if (value != null)
                    {
                        FieldInfo info = propValues[i].PropertyType.GetField(value.ToString());

                        if (info != null && info.IsDefined(typeof(XmlEnumAttribute), false))
                        {
                            object[] o = info.GetCustomAttributes(typeof(XmlEnumAttribute), false);
                            XmlEnumAttribute att = (XmlEnumAttribute)o[0];
                            value = att.Name;
                        }

                        lParams.Add(string.Format("{0}={1}", propValues[i].Name, Util.UrlEncode(value.ToString())));
                    }
                }
                else if (propValues[i].PropertyType == typeof(List<string>))
                {
                    var value = (List<string>)propValues[i].GetValue(request);

                    for (int a = 0; a < value.Count; a++)
                    {
                        lParams.Add(string.Format("{0}.{1}={2}", propValues[i].Name, a + 1, Util.UrlEncode(value[a])));
                    }
                }
            }

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("&", lParams);

            string sig = Util.GetSignature(URL, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", URL, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(RunInstancesResponse));
                var runInstancesResponse = (RunInstancesResponse)serializer.Deserialize(element.CreateReader());
                return runInstancesResponse;
            }
        }
    }
}
