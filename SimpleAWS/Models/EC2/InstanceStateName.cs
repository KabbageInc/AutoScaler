using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public enum InstanceStateName
    {
        [XmlEnum(Name = "pending")]
        pending,
        [XmlEnum(Name = "running")]
        running,
        [XmlEnum(Name = "shutting-down")]
        shuttingdown,
        [XmlEnum(Name = "stopped")]
        stopped,
        [XmlEnum(Name = "stopping")]
        stopping,
        [XmlEnum(Name = "terminated")]
        terminated,
    }
}
