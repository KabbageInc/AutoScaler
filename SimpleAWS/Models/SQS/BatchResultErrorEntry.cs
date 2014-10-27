using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.SQS
{
    public class BatchResultErrorEntry
    {
        public string Code { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public bool SenderFault { get; set; }
    }
}
