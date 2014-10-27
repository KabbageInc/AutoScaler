using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class EbsInstanceBlockDevice
    {
        [XmlElement("attachTime")]
        public DateTime AttachTime { get; set; }

        [XmlElement("deleteOnTermination")]
        public bool DeleteOnTermination { get; set; }

        [XmlElement("status")]
        public AttachmentStatus Status { get; set; }

        [XmlElement("volumeId")]
        public string VolumeId { get; set; }
    }
}
