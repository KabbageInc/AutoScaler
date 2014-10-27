using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class PrivateIpAddressSpecification
    {
        [XmlElement("primary")]
        public bool Primary { get; set; }

        [XmlElement("privateIpAddress")]
        public string PrivateIpAddress { get; set; }
    }
}
