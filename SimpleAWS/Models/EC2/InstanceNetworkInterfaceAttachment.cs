using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceNetworkInterfaceAttachment
    {
        [XmlElement("attachmentId")]
        public string AttachmentId { get; set; }

        [XmlElement("attachTime")]
        public DateTime AttachTime { get; set; }

        [XmlElement("deleteOnTermination")]
        public bool DeleteOnTermination { get; set; }

        [XmlElement("deviceIndex")]
        public int DeviceIndex { get; set; }

        [XmlElement("status")]
        public AttachmentStatus Status { get; set; }
    }
}
