using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SimpleAWS
{
    public class Util
    {
        public const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static T RetryMethod<T>(Func<T> function, int retryCount)
        {
            int trys = 0;

            while(true)
            {
                try
                {
                    return function();
                }
                catch
                {
                    if (trys >= retryCount)
                        throw;

                    trys++;
                    Thread.Sleep(Util.ExponentialDelay(trys) * 1000);
                }
            }
        }

        public static void RetryMethod(Action function, int retryCount)
        {
            int trys = 0;

            while (true)
            {
                try
                {
                    function();
                    return;
                }
                catch
                {
                    if (trys >= retryCount)
                        throw;

                    trys++;
                    Thread.Sleep(Util.ExponentialDelay(trys) * 1000);
                }
            }
        }

        public static string GetSignature(string queueUrl, string method, string parameters, string secretKey)
        {
            var url = queueUrl.Replace("http://", "").Replace("https://", "");
            string[] split = url.Split('/');

            string queueName = "";
            string hostName = split[0];

            for (int i = 1; i < split.Length; i++)
            {
                queueName += "/" + split[i];
            }

            string canonicalString = string.Format("{0}\n{1}\n{2}\n{3}", method, hostName, queueName, parameters);

            Encoding ae = new UTF8Encoding();
            HMACSHA256 signature = new HMACSHA256(ae.GetBytes(secretKey));
            return Convert.ToBase64String(signature.ComputeHash(ae.GetBytes(canonicalString)));
        }

        public static int ExponentialDelay(int failedAttempts, int maxDelayInSeconds = 1024)
        {
            if (failedAttempts <= 0)
                return 0;

            var delayInSeconds = ((1d / 2d) * (Math.Pow(2d, failedAttempts) - 1d));

            return maxDelayInSeconds < delayInSeconds ? maxDelayInSeconds : (int)delayInSeconds;
        }

        public static string UrlEncode(string data, bool path = false)
        {
            if (data == null)
                return data;

            StringBuilder encoded = new StringBuilder(data.Length * 2);
            string unreservedChars = String.Concat(
                Util.ValidUrlCharacters,
                (path ? "/:" : "")
                );

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append('%').Append(String.Format("{0:X2}", (int)symbol));
                }
            }

            return encoded.ToString();
        }
    }
}
