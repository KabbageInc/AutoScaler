using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.SQS
{
    [XmlRoot(Namespace = "http://queue.amazonaws.com/doc/2012-11-05/", IsNullable = false)]
    public class DeleteMessageResponse
    {
    }
}
