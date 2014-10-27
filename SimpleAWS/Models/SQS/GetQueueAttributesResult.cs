using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.SQS
{
    public class GetQueueAttributesResult
    {
        [XmlElement("Attribute")]
        public List<Attribute> Attributes { get; set; }

        public int ApproximateNumberOfMessages { get; set; }
        public int ApproximateNumberOfMessagesDelayed { get; set; }
        public int ApproximateNumberOfMessagesNotVisible { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public int DelaySeconds { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public int MaximumMessageSize { get; set; }
        public int MessageRetentionPeriod { get; set; }
        public string Policy { get; set; }
        public string QueueARN { get; set; }
        public int VisibilityTimeout { get; set; }
    }
}