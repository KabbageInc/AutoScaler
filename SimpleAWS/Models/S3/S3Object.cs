using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.S3
{
    public class S3Object
    {
        public string ETag { get; set; }
        public string Key { get; set; }
        public DateTime LastModified { get; set; }
        public Owner Owner { get; set; }
        public long Size { get; set; }

        [XmlElement("StorageClass")]
        public S3StorageClass StorageClass { get; set; }
    }
}
