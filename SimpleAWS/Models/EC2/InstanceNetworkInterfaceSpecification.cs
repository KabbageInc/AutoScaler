using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceNetworkInterfaceSpecification
    {
        [XmlElement("associatePublicIpAddress")]
        public bool AssociatePublicIpAddress { get; set; }

        [XmlElement("deleteOnTermination")]
        public bool DeleteOnTermination { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("deviceIndex")]
        public int DeviceIndex { get; set; }

        [XmlElement("groups")]
        public List<string> Groups { get; set; }

        [XmlElement("networkInterfaceId")]
        public string NetworkInterfaceId { get; set; }

        [XmlElement("privateIpAddress")]
        public string PrivateIpAddress { get; set; }

        [XmlElement("privateIpAddresses")]
        public List<PrivateIpAddressSpecification> PrivateIpAddresses { get; set; }

        [XmlElement("secondaryPrivateIpAddressCount")]
        public int SecondaryPrivateIpAddressCount { get; set; }

        [XmlElement("subnetId")]
        public string SubnetId { get; set; }
    }
}
