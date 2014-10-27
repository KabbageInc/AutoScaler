using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.S3
{
    public class S3Response
    {
        public string Body { get; set; }
        public DateTime LastModified { get; set; }
    }
}
