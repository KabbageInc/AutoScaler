using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    [XmlRoot(Namespace = "http://ec2.amazonaws.com/doc/2012-07-20/", IsNullable = false)]
    public class RunInstancesResponse
    {
        [XmlElement("requestId")]
        public string RequestId { get; set; }

        [XmlElement("reservationId")]
        public string ReservationId { get; set; }

        [XmlElement("ownerId")]
        public string OwnerId { get; set; }

        [XmlArray(ElementName = "instancesSet")]
        [XmlArrayItem("item")]
        public List<Instance> Instances { get; set; }
    }
}
