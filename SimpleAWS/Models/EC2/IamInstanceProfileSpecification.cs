using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class IamInstanceProfileSpecification
    {
        [XmlElement("arn")]
        public string Arn { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
