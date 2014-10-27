using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.S3
{
    [XmlRoot(Namespace = "http://s3.amazonaws.com/doc/2006-03-01/", IsNullable = false)]
    public class ListBucketResult
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Marker { get; set; }
        public string NextMarker { get; set; }

        public int MaxKeys { get; set; }
        public string Delimiter { get; set; }
        public bool IsTruncated { get; set; }

        [XmlElement("CommonPrefixes")]
        public List<CommonPrefix> CommonPrefixes { get; set; }

        [XmlElement("Contents")]
        public List<S3Object> S3Objects { get; set; }
    }
}
