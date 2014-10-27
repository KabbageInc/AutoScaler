using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.SQS
{
    public class SendMessageBatchResultEntry
    {
        public string Id { get; set; }
        public string MD5OfMessageBody { get; set; }
        public string MessageId { get; set; }
    }
}
