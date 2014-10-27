using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceNetworkInterface
    {
        [XmlElement("association")]
        public InstanceNetworkInterfaceAssociation Association { get; set; }

        [XmlElement("attachment")]
        public InstanceNetworkInterfaceAttachment Attachment { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlArray(ElementName = "groupSet")]
        [XmlArrayItem("item")]
        public List<GroupIdentifier> Groups { get; set; }

        [XmlElement("networkInterfaceId")]
        public string NetworkInterfaceId { get; set; }

        [XmlElement("ownerId")]
        public string OwnerId { get; set; }

        [XmlElement("privateDnsName")]
        public string PrivateDnsName { get; set; }

        [XmlElement("privateIpAddress")]
        public string PrivateIpAddress { get; set; }

        [XmlArray(ElementName = "privateIpAddressesSet")]
        [XmlArrayItem("item")]
        public List<InstancePrivateIpAddress> PrivateIpAddresses { get; set; }

        [XmlElement("sourceDestCheck")]
        public bool SourceDestCheck { get; set; }

        [XmlElement("status")]
        public NetworkInterfaceStatus Status { get; set; }

        [XmlElement("subnetId")]
        public string SubnetId { get; set; }

        [XmlElement("vpcId")]
        public string VpcId { get; set; }
    }
}
