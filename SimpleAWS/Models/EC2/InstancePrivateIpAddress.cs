using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstancePrivateIpAddress
    {
        [XmlElement("association")]
        public InstanceNetworkInterfaceAssociation Association { get; set; }

        [XmlElement("primary")]
        public bool Primary { get; set; }

        [XmlElement("privateDnsName")]
        public string PrivateDnsName { get; set; }

        [XmlElement("privateIpAddress")]
        public string PrivateIpAddress { get; set; }
    }
}
