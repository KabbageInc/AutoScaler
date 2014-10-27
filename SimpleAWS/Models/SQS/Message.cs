using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.SQS
{
    public class Message
    {
        public string Body { get; set; }
        public string MD5OfBody { get; set; }
        public string MessageId { get; set; }
        public string ReceiptHandle { get; set; }
    }
}
