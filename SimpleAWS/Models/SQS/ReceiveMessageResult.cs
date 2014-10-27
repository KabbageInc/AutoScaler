using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.SQS
{
    public class ReceiveMessageResult
    {
        [XmlElement("Message")]
        public List<Message> Messages { get; set; }
    }
}
