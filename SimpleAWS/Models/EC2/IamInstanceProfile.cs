using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class IamInstanceProfile
    {
        [XmlElement("arn")]
        public string Arn { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }
    }
}
