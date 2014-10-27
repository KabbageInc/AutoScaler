using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.S3
{
    public class ListBucketRequest
    {
        public string BucketName { get; set; }
        public string Delimiter { get; set; }
        public EncodingType Encoding { get; set; }
        public string Marker { get; set; }
        public int MaxKeys { get; set; }
        public string Prefix { get; set; }
    }
}
