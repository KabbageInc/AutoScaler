using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.SQS
{
    public class SendMessageBatchRequest
    {
        public List<SendMessageBatchRequestEntry> Entries { get; set; }
        public string QueueUrl { get; set; }
    }
}
