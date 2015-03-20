using System;
using System.ServiceProcess;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.MSDTC
{
    internal sealed class MSDTCHelper
    {
        private readonly ICakeLog log;

        internal MSDTCHelper(ICakeLog log)
        {
            this.log = log;
        }

        public static MSDTCHelper Using(ICakeContext context)
        {
            return new MSDTCHelper(context.Log);
        }

        public void Start(int timeout = 10)
        {
            Activate(
               ServiceControllerStatus.Stopped,
               ServiceControllerStatus.Running,
               service => service.Start(),
               "MSDTC started.",
               "MSDTC already running.",
               "MSDTC starting...",
               timeout);
        }

        public void Stop(int timeout = 10)
        {
            Activate(
               ServiceControllerStatus.Running,
               ServiceControllerStatus.Stopped,
               service => service.Stop(),
               "MSDTC stopped.",
               "MSDTC already stopped.",
               "MSDTC stopping...",
               timeout);
        }

        public void Pause(int timeout = 10)
        {
            Activate(
                ServiceControllerStatus.Running,
                ServiceControllerStatus.Paused,
                service => service.Pause(),
                "MSDTC paused.",
                "MSDTC already paused.",
                "MSDTC pausing...",
                timeout);
        }

        public void Continue(int timeout = 10)
        {
            Activate(
                ServiceControllerStatus.Paused,
                ServiceControllerStatus.Running,
                service => service.Continue(),
                "MSDTC resumed.",
                "MSDTC already running.",
                "MSDTC resuming...",
                timeout);
        }

        private void Activate(
            ServiceControllerStatus currentStatus,
            ServiceControllerStatus newStatus,
            Action<ServiceController> activate, 
            string success, 
            string skipped, 
            string inProgress, 
            int timeout = 10)
        {
            using(var service = new ServiceController("MSDTC"))
            {
                if(service.Status == currentStatus)
                {
                    this.log.Verbose(inProgress);
                    activate(service);
                    service.WaitForStatus(newStatus, TimeSpan.FromSeconds(timeout));
                    this.log.Information(success);
                }
                else
                {
                    this.log.Information(skipped);
                }
            }
        }
    }
}