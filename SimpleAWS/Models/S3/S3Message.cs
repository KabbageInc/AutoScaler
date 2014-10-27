using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.S3
{
    public class S3Message
    {
        public string Bucket { get; set; }
        public string Key { get; set; }
    }
}
