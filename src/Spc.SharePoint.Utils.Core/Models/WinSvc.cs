namespace Spc.SharePoint.Utils.Core.Models
{
    using System;
    using System.Runtime.InteropServices;
    using System.ServiceProcess;

    public class WinSvc
    {
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        public string MachineName { get; set; }
        public bool CanStop { get; set; }
        public bool CanShutdown { get; set; }
        public ServiceType CurrentSvcType { get; set; }
        public SafeHandle ServiceHandle { get; set; }
        public ServiceControllerStatus Status { get; set; }
        public DateTime LastUpdated { get; set; }

        public WinSvc()
        {

        }
    }
}