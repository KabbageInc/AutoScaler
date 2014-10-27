using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.SQS
{
    public class SendMessageRequest
    {
        public int DelaySeconds { get; set; }
        public string MessageBody { get; set; }
        public string QueueUrl { get; set; }
    }
}
