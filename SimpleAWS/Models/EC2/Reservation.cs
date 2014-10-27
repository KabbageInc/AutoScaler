using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class Reservation
    {
        [XmlArray(ElementName = "groupSet")]
        [XmlArrayItem("item")]
        public List<GroupIdentifier> Groups { get; set; }

        [XmlArray(ElementName = "instancesSet")]
        [XmlArrayItem("item")]
        public List<Instance> Instances { get; set; }

        [XmlElement("ownerId")]
        public string OwnerId { get; set; }

        [XmlElement("requesterId")]
        public string RequesterId { get; set; }

        [XmlElement("reservationId")]
        public string ReservationId { get; set; }
    }
}
