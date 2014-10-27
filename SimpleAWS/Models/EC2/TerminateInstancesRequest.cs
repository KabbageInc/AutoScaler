using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.EC2
{
    public class TerminateInstancesRequest
    {
        public TerminateInstancesRequest()
        {
            InstanceIds = new List<string>();
        }

        public List<string> InstanceIds { get; set; }
    }
}
