using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.S3
{
    public enum S3StorageClass
    {
        GLACIER,
        REDUCEDREDUNDANCY,
        STANDARD,
    }
}
