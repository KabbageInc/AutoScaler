using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public class ProductCode
    {
        [XmlElement("productCodeId")]
        public string ProductCodeId { get; set; }

        [XmlElement("productCodeType")]
        public ProductCodeValues ProductCodeType { get; set; }
    }
}
