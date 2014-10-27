using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class GroupIdentifier
    {
        [XmlElement("groupId")]
        public string GroupId { get; set; }

        [XmlElement("groupName")]
        public string GroupName { get; set; }
    }
}
