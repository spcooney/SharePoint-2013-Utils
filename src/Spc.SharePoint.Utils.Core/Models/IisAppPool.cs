namespace Spc.SharePoint.Utils.Core.Models
{
    using Microsoft.Web.Administration;
    using System;

    public sealed class IisAppPool
    {
        #region "Auto Properties"

        public string Name { get; set; }
        public int ProcessID { get; set; }
        public string State { get; set; }
        public string RuntimeVersion { get; set; }
        public int WorkerProcessCount { get; set; }
        public DateTime LastUpdated { get; set; }
        public ApplicationPool CurrentAppPool { get; set; }

        #endregion

        #region "Constructors"

        public IisAppPool(ApplicationPool appPool)
        {
            this.CurrentAppPool = appPool;
            this.Name = appPool.Name;
            this.LastUpdated = DateTime.Now;
            FindProcessID();
        }

        #endregion

        #region "Methods"

        private void FindProcessID()
        {
            this.WorkerProcessCount = this.CurrentAppPool.WorkerProcesses.Count;
            for (int i = 0; i < this.WorkerProcessCount; i++)
            {
                this.ProcessID = this.CurrentAppPool.WorkerProcesses[i].ProcessId;
                this.State = this.CurrentAppPool.WorkerProcesses[i].State.ToString();
                this.RuntimeVersion = this.CurrentAppPool.ManagedRuntimeVersion;
            }
        }

        #endregion
    }
}