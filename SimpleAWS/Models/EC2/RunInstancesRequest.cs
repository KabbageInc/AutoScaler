using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.EC2
{
    public class RunInstancesRequest
    {
        public RunInstancesRequest()
        {
            BlockDeviceMapping = new List<BlockDeviceMapping>();
            NetworkInterface = new List<InstanceNetworkInterfaceSpecification>();
            SecurityGroupId = new List<string>();
            SecurityGroup = new List<string>();
        }
        
        public List<BlockDeviceMapping> BlockDeviceMapping { get; set; }
        public string ClientToken { get; set; }
        public bool DisableApiTermination { get; set; }
        public bool EbsOptimized { get; set; }
        public IamInstanceProfileSpecification IamInstanceProfile { get; set; }
        public string ImageId { get; set; }
        public ShutdownBehavior InstanceInitiatedShutdownBehavior { get; set; }
        public InstanceType InstanceType { get; set; }
        public string KernelId { get; set; }
        public string KeyName { get; set; }
        public int MaxCount { get; set; }
        public int MinCount { get; set; }
        public bool Monitoring { get; set; }
        public List<InstanceNetworkInterfaceSpecification> NetworkInterface { get; set; }
        public Placement Placement { get; set; }
        public string PrivateIpAddress { get; set; }
        public string RamdiskId { get; set; }
        public List<string> SecurityGroupId { get; set; }
        public List<string> SecurityGroup { get; set; }
        public string SubnetId { get; set; }
        public string UserData { get; set; }
    }
}
