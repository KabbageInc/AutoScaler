using SimpleAWS.Models;
using SimpleAWS.Models.S3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleAWS
{
    public class S3Client
    {
        private string AccessKey { get; set; }
        private string SecretKey { get; set; }
        private int RetryCount { get; set; }

        public S3Client(string accessKey, string secretKey, int retryCount = 0)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            AccessKey = accessKey;
            SecretKey = secretKey;
            RetryCount = retryCount;
        }

        public ListBucketResult ListBucket(ListBucketRequest request)
        {
            return Util.RetryMethod<ListBucketResult>(() => DoListBucket(request), RetryCount);
        }

        public S3Response DownloadFile(string bucket, string key)
        {
            return Util.RetryMethod<S3Response>(() => DoDownloadFile(bucket, key), RetryCount);
        }

        public void UploadFile(string bucket, string key, string body)
        {
            Util.RetryMethod(() => DoUploadFile(bucket, key, body), RetryCount);
        }

        private HttpWebRequest CreateRequest(string method, string contentType, string bucket, string key, string parameters)
        {
            string httpDate = String.Format("{0:ddd,' 'dd' 'MMM' 'yyyy' 'HH':'mm':'ss' 'zz00}", DateTime.Now);
            string requestUri = string.Format("https://{0}.s3.amazonaws.com{1}{2}", bucket, (key != null) ? key : "/", (parameters != null) ? parameters : string.Empty);
            string canonicalString = string.Format("{0}\n\n{1}\n\nx-amz-acl:{2}\nx-amz-date:{3}\n/{4}{5}", method, contentType, "private", httpDate, bucket, (key != null) ? key : "/");

            Encoding ae = new UTF8Encoding();
            HMACSHA1 signature = new HMACSHA1(ae.GetBytes(SecretKey));
            string encodedCanonical = Convert.ToBase64String(signature.ComputeHash(ae.GetBytes(canonicalString)));
            string authHeader = "AWS " + AccessKey + ":" + encodedCanonical;

            var wRequest = WebRequest.Create(requestUri) as HttpWebRequest;
            wRequest.Method = method;
            wRequest.ContentType = contentType;
            wRequest.Headers.Add("x-amz-acl", "private");
            wRequest.Headers.Add("x-amz-date", httpDate);
            wRequest.Headers.Add("Authorization", authHeader);
            wRequest.KeepAlive = false;

            return wRequest;
        }

        private ListBucketResult DoListBucket(ListBucketRequest request)
        {
            StringBuilder sb = new StringBuilder("?", 256);

            if (!string.IsNullOrEmpty(request.Delimiter))
                sb.Append(String.Concat("delimiter=", Util.UrlEncode(request.Delimiter), "&"));
            if (!string.IsNullOrEmpty(request.Marker))
                sb.Append(String.Concat("marker=", Util.UrlEncode(request.Marker), "&"));
            if (request.MaxKeys > 0)
                sb.Append(String.Concat("max-keys=", request.MaxKeys, "&"));
            if (!string.IsNullOrEmpty(request.Prefix))
                sb.Append(String.Concat("prefix=", Util.UrlEncode(request.Prefix), "&"));

            string parameters = sb.ToString();

            if (parameters.EndsWith("&", StringComparison.OrdinalIgnoreCase))
                parameters = parameters.Remove(parameters.Length - 1);

            if (parameters.Length <= 1)
                parameters = null;
            
            var wRequest = CreateRequest("GET", "text/plain", request.BucketName, Util.UrlEncode(request.Delimiter, true), parameters);

            using (var response = wRequest.GetResponse() as HttpWebResponse)
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var element = XElement.Parse(body);

                XmlSerializer serializer = new XmlSerializer(typeof(ListBucketResult));
                var listBucketResult = (ListBucketResult)serializer.Deserialize(element.CreateReader());

                if (listBucketResult.IsTruncated && listBucketResult.S3Objects.Count > 0)
                    listBucketResult.NextMarker = listBucketResult.S3Objects.Last().Key;

                return listBucketResult;
            }
        }

        private S3Response DoDownloadFile(string bucket, string key)
        {
            var wRequest = CreateRequest("GET", "text/plain", bucket, Util.UrlEncode(key.StartsWith("/") ? key : "/" + key, true), null);

            using (var response = wRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();
                var date = response.Headers.Get("Last-Modified");

                return new S3Response { Body = body, LastModified = (date != null) ? DateTime.Parse(date) : DateTime.Now };
            }
        }

        private void DoUploadFile(string bucket, string key, string body)
        {
            var data = UTF8Encoding.UTF8.GetBytes(body);

            var request = CreateRequest("PUT", "text/plain", bucket, Util.UrlEncode(key.StartsWith("/") ? key : "/" + key, true), null);

            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = request.GetResponse();
            response.Dispose();
        }
    }
}
