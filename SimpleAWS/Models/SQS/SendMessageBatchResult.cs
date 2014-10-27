using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.SQS
{
    public class SendMessageBatchResult
    {
        [XmlElement("BatchResultErrorEntry")]
        public List<BatchResultErrorEntry> Failed { get; set; }

        [XmlElement("SendMessageBatchResultEntry")]
        public List<SendMessageBatchResultEntry> Successful { get; set; }
    }
}
