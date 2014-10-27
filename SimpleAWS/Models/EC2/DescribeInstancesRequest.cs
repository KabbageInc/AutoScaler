using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.EC2
{
    public class DescribeInstancesRequest
    {
        public DescribeInstancesRequest()
        {
            Filters = new List<Filter>();
            InstanceIds = new List<string>();
        }

        public List<Filter> Filters { get; set; }
        public List<string> InstanceIds { get; set; }
        public int MaxResults { get; set; }
        public string NextToken { get; set; }
    }
}
