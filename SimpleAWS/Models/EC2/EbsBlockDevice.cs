using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class EbsBlockDevice
    {
        [XmlElement("deleteOnTermination")]
        public bool DeleteOnTermination { get; set; }

        [XmlElement("iops")]
        public int Iops { get; set; }

        [XmlElement("snapshotId")]
        public string SnapshotId { get; set; }

        [XmlElement("volumeSize")]
        public int VolumeSize { get; set; }

        [XmlElement("volumeType")]
        public VolumeType VolumeType { get; set; }
    }
}
