using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class InstanceState
    {
        [XmlElement("code")]
        public int Code { get; set; }

        [XmlElement("name")]
        public InstanceStateName Name { get; set; }
    }
}
