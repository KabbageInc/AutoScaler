using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceStateChange
    {
        [XmlElement("currentState")]
        public InstanceState CurrentState { get; set; }

        [XmlElement("instanceId")]
        public string InstanceId { get; set; }

        [XmlElement("previousState")]
        public InstanceState PreviousState { get; set; }
    }
}
