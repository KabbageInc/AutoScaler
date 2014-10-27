using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class Instance
    {
        [XmlElement("amiLaunchIndex")]
        public int AmiLaunchIndex { get; set; }

        [XmlElement("architecture")]
        public ArchitectureValues Architecture { get; set; }

        [XmlArray(ElementName = "blockDeviceMapping")]
        [XmlArrayItem("item")]
        public List<InstanceBlockDeviceMapping> BlockDeviceMappings { get; set; }

        [XmlElement("clientToken")]
        public string ClientToken { get; set; }

        [XmlElement("ebsOptimized")]
        public bool EbsOptimized { get; set; }

        [XmlElement("hypervisor")]
        public HypervisorType Hypervisor { get; set; }

        [XmlElement("iamInstanceProfile")]
        public IamInstanceProfile IamInstanceProfile { get; set; } //did not see it

        [XmlElement("imageId")]
        public string ImageId { get; set; }

        [XmlElement("instanceId")]
        public string InstanceId { get; set; }

        [XmlElement("instanceType")]
        public InstanceType InstanceType { get; set; }

        [XmlElement("kernelId")]
        public string KernelId { get; set; }

        [XmlElement("keyName")]
        public string KeyName { get; set; }

        [XmlElement("launchTime")]
        public DateTime LaunchTime { get; set; }

        [XmlElement("monitoring")]
        public Monitoring Monitoring { get; set; }

        [XmlArray(ElementName = "networkInterfaceSet")]
        [XmlArrayItem("item")]
        public List<InstanceNetworkInterface> NetworkInterfaces { get; set; } //was there but empty, need to verify

        [XmlElement("placement")]
        public Placement Placement { get; set; }

        [XmlElement("platform")]
        public PlatformValues Platform { get; set; }

        [XmlElement("privateDnsName")]
        public string PrivateDnsName { get; set; }

        [XmlElement("privateIpAddress")]
        public string PrivateIpAddress { get; set; }

        [XmlArray(ElementName = "productCodes")]
        [XmlArrayItem("item")]
        public List<ProductCode> ProductCodes { get; set; }

        [XmlElement("dnsName")]
        public string PublicDnsName { get; set; }

        [XmlElement("ipAddress")]
        public string PublicIpAddress { get; set; }

        public string RamdiskId { get; set; }

        [XmlElement("rootDeviceName")]
        public string RootDeviceName { get; set; }

        [XmlElement("rootDeviceType")]
        public DeviceType RootDeviceType { get; set; }

        [XmlArray(ElementName = "groupSet")]
        [XmlArrayItem("item")]
        public List<GroupIdentifier> SecurityGroups { get; set; }

        [XmlElement("sourceDestCheck")]
        public bool SourceDestCheck { get; set; } //did not see it

        [XmlElement("spotInstanceRequestId")]
        public string SpotInstanceRequestId { get; set; } //did not see it

        [XmlElement("sriovNetSupport")]
        public string SriovNetSupport { get; set; } //did not see it

        [XmlElement("instanceState")]
        public InstanceState State { get; set; }

        [XmlElement("reason")]
        public StateReason StateReason { get; set; }

        [XmlElement("stateTransitionReason")]
        public string StateTransitionReason { get; set; } //did not see it

        [XmlElement("subnetId")]
        public string SubnetId { get; set; } //did not see it

        [XmlArray(ElementName = "tagSet")]
        [XmlArrayItem("item")]
        public List<Tag> Tags { get; set; }

        [XmlElement("virtualizationType")]
        public VirtualizationType VirtualizationType { get; set; }

        [XmlElement("vpcId")]
        public string VpcId { get; set; } //did not see it
    }
}
