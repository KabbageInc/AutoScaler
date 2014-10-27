using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class BlockDeviceMapping
    {
        [XmlElement("deviceName")]
        public string DeviceName { get; set; }

        [XmlElement("ebs")]
        public EbsBlockDevice Ebs { get; set; }

        [XmlElement("noDevice")]
        public string NoDevice { get; set; }

        [XmlElement("virtualName")]
        public string VirtualName { get; set; }
    }
}
