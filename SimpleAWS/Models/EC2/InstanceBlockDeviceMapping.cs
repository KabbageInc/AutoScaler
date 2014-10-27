using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceBlockDeviceMapping
    {
        [XmlElement("deviceName")]
        public string DeviceName { get; set; }

        [XmlElement("ebs")]
        public EbsInstanceBlockDevice Ebs { get; set; }
    }
}
