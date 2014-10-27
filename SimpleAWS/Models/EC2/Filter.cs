using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAWS.Models.EC2
{
    public class Filter
    {
        public Filter()
        {
            Values = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
