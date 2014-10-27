using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceNetworkInterfaceAssociation
    {
        [XmlElement("ipOwnerId")]
        public string IpOwnerId { get; set; }

        [XmlElement("publicDnsName")]
        public string PublicDnsName { get; set; }

        [XmlElement("publicIp")]
        public string PublicIp { get; set; }
    }
}
