using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    [XmlRoot(Namespace = "http://ec2.amazonaws.com/doc/2012-07-20/", IsNullable = false)]
    public class DescribeInstancesResponse
    {
        [XmlElement("requestId")]
        public string RequestId { get; set; }

        [XmlArray(ElementName = "reservationSet")]
        [XmlArrayItem("item")]
        public List<Reservation> Reservations { get; set; }

        [XmlElement("nextToken")]
        public string NextToken { get; set; }
    }
}
