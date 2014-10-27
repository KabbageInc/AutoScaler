using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.SQS
{
    public class ReceiveMessageRequest
    {
        public int MaxNumberOfMessages { get; set; }
        public string QueueUrl { get; set; }
        public int VisibilityTimeout { get; set; }
        public int WaitTimeSeconds { get; set; }
    }
}
