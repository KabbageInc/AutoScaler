using SimpleAWS.Models;
using SimpleAWS.Models.SQS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleAWS
{
    public class SQSClient
    {
        private string AccessKey { get; set; }
        private string SecretKey { get; set; }
        private int RetryCount { get; set; }

        public SQSClient(string accessKey, string secretKey, int retryCount = 0)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            AccessKey = accessKey;
            SecretKey = secretKey;
            RetryCount = retryCount;
        }

        public SendMessageBatchResponse SendMessageBatch(SendMessageBatchRequest request)
        {
            return Util.RetryMethod<SendMessageBatchResponse>(() => DoSendMessageBatch(request), RetryCount);
        }

        public SendMessageResponse SendMessage(SendMessageRequest request)
        {
            return Util.RetryMethod<SendMessageResponse>(() => DoSendMessage(request), RetryCount);
        }

        public DeleteMessageResponse DeleteMessage(string queueUrl, string receiptHandle)
        {
            return Util.RetryMethod<DeleteMessageResponse>(() => DoDeleteMessage(queueUrl, receiptHandle), RetryCount);
        }

        public ReceiveMessageResponse ReceiveMessage(ReceiveMessageRequest request)
        {
            return Util.RetryMethod<ReceiveMessageResponse>(() => DoReceiveMessage(request), RetryCount);
        }

        public GetQueueAttributesResponse GetQueueAttributes(GetQueueAttributesRequest request)
        {
            return Util.RetryMethod<GetQueueAttributesResponse>(() => DoGetQueueAttributes(request), RetryCount);
        }

        private SendMessageBatchResponse DoSendMessageBatch(SendMessageBatchRequest request)
        {
            if (request.Entries.Count == 0)
                throw new ArgumentOutOfRangeException();

            List<string> lParams = new List<string>();

            lParams.Add(string.Format("&AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("&Action={0}", "SendMessageBatch"));
            lParams.Add(string.Format("&SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("&SignatureVersion={0}", "2"));
            lParams.Add(string.Format("&Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("&Version={0}", "2012-11-05"));

            for (int i = 0; i < request.Entries.Count; i++)
            {
                lParams.Add(string.Format("&SendMessageBatchRequestEntry.{0}.Id={1}", i + 1, Util.UrlEncode(request.Entries[i].Id)));
                lParams.Add(string.Format("&SendMessageBatchRequestEntry.{0}.DelaySeconds={1}", i + 1, request.Entries[i].DelaySeconds));
                lParams.Add(string.Format("&SendMessageBatchRequestEntry.{0}.MessageBody={1}", i + 1, Util.UrlEncode(request.Entries[i].MessageBody)));
            }

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("", lParams);
            parameters = parameters.Substring(1);

            string sig = Util.GetSignature(request.QueueUrl, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", request.QueueUrl, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(SendMessageBatchResponse));
                var sendMessageBatchResponse = (SendMessageBatchResponse)serializer.Deserialize(element.CreateReader());
                return sendMessageBatchResponse;
            }
        }

        private SendMessageResponse DoSendMessage(SendMessageRequest request)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("&AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("&Action={0}", "SendMessage"));
            lParams.Add(string.Format("&DelaySeconds={0}", request.DelaySeconds));
            lParams.Add(string.Format("&MessageBody={0}", Util.UrlEncode(request.MessageBody)));
            lParams.Add(string.Format("&SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("&SignatureVersion={0}", "2"));
            lParams.Add(string.Format("&Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("&Version={0}", "2012-11-05"));

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("", lParams);
            parameters = parameters.Substring(1);

            string sig = Util.GetSignature(request.QueueUrl, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", request.QueueUrl, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(SendMessageResponse));
                var sendMessageResponse = (SendMessageResponse)serializer.Deserialize(element.CreateReader());
                return sendMessageResponse;
            }
        }

        private DeleteMessageResponse DoDeleteMessage(string queueUrl, string receiptHandle)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("&AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("&Action={0}", "DeleteMessage"));
            lParams.Add(string.Format("&ReceiptHandle={0}", Util.UrlEncode(receiptHandle)));
            lParams.Add(string.Format("&SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("&SignatureVersion={0}", "2"));
            lParams.Add(string.Format("&Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("&Version={0}", "2012-11-05"));

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("", lParams);
            parameters = parameters.Substring(1);

            string sig = Util.GetSignature(queueUrl, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", queueUrl, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(DeleteMessageResponse));
                var deleteMessageResponse = (DeleteMessageResponse)serializer.Deserialize(element.CreateReader());
                return deleteMessageResponse;
            }
        }

        private ReceiveMessageResponse DoReceiveMessage(ReceiveMessageRequest request)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("&AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("&Action={0}", "ReceiveMessage"));
            lParams.Add(string.Format("&MaxNumberOfMessages={0}", request.MaxNumberOfMessages));
            lParams.Add(string.Format("&SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("&SignatureVersion={0}", "2"));
            lParams.Add(string.Format("&Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("&Version={0}", "2012-11-05"));
            lParams.Add(string.Format("&VisibilityTimeout={0}", request.VisibilityTimeout));
            lParams.Add(string.Format("&WaitTimeSeconds={0}", request.WaitTimeSeconds));

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("", lParams);
            parameters = parameters.Substring(1);

            string sig = Util.GetSignature(request.QueueUrl, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", request.QueueUrl, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(ReceiveMessageResponse));
                var receiveMessageResponse = (ReceiveMessageResponse)serializer.Deserialize(element.CreateReader());
                return receiveMessageResponse;
            }
        }

        public GetQueueAttributesResponse DoGetQueueAttributes(GetQueueAttributesRequest request)
        {
            List<string> lParams = new List<string>();

            lParams.Add(string.Format("&AWSAccessKeyId={0}", Util.UrlEncode(AccessKey)));
            lParams.Add(string.Format("&Action={0}", "GetQueueAttributes"));
            lParams.Add(string.Format("&SignatureMethod={0}", "HmacSHA256"));
            lParams.Add(string.Format("&SignatureVersion={0}", "2"));
            lParams.Add(string.Format("&Timestamp={0}", Util.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))));
            lParams.Add(string.Format("&Version={0}", "2012-11-05"));

            for (int i = 0; i < request.AttributeNames.Count; i++)
            {
                lParams.Add(string.Format("&AttributeName.{0}={1}", i + 1, Util.UrlEncode(request.AttributeNames[i])));
            }

            lParams.Sort(StringComparer.Ordinal);

            var parameters = string.Join("", lParams);
            parameters = parameters.Substring(1);

            string sig = Util.GetSignature(request.QueueUrl, "GET", parameters, SecretKey);
            parameters = string.Format("{0}&Signature={1}", parameters, Util.UrlEncode(sig));

            var wRequest = WebRequest.Create(string.Format("{0}?{1}", request.QueueUrl, parameters)) as HttpWebRequest;
            wRequest.Method = "GET";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.KeepAlive = false;

            using (var response = wRequest.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(GetQueueAttributesResponse));
                var setQueueAttributesResponse = (GetQueueAttributesResponse)serializer.Deserialize(element.CreateReader());

                foreach (var attribute in setQueueAttributesResponse.GetQueueAttributesResult.Attributes)
                {
                    var propType = typeof(GetQueueAttributesResult).GetProperty(attribute.Name);
                    var converter = TypeDescriptor.GetConverter(propType.PropertyType);
                    var value = converter.ConvertFromString(attribute.Value);
                    propType.SetValue(setQueueAttributesResponse.GetQueueAttributesResult, value);
                }

                return setQueueAttributesResponse;
            }
        }
    }
}
