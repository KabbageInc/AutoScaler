using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AutoScaler
{
    public class Service : ServiceBase
    {
        public Service()
        {
            ServiceName = "AutoScaler";
        }

        protected override void OnStart(string[] args)
        {
            AutoScaler.Start(args);
        }

        protected override void OnStop()
        {
            AutoScaler.Stop();
        }
    }
}