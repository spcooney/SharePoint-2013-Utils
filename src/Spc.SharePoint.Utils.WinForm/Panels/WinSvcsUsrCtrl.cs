namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Spc.SharePoint.Utils.Core.Helper;
    using Spc.SharePoint.Utils.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.ServiceProcess;
    using System.Windows.Forms;
    using SWF = System.Windows.Forms;

    public partial class WinSvcsUsrCtrl : UserControl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(WinSvcsUsrCtrl));
        SWF.Timer FormTimer = new SWF.Timer();
        SWF.Timer GridTimer = new SWF.Timer();
        private static readonly string[] WinServices = new string[4] { "W3SVC", "MSSQLSERVER", "SPTimerV4", "SPSearchHostController" };
        private static string[] WinSvcStr;

        #endregion

        #region "Constructors"

        public WinSvcsUsrCtrl()
        {
            InitializeComponent();
            string winSvcs = AppSettings.Instance.WinServiceNames;
            if (StringUtil.IsNotNullOrWhitespace(winSvcs))
            {
                WinSvcStr = winSvcs.Split(new string[1] { CharUtil.SemiColon.ToString() }, StringSplitOptions.RemoveEmptyEntries);
            }
            // Refresh the grid every 15 minutes
            GridTimer.Interval = (15 * 1000 * 60);
            GridTimer.Tick += new EventHandler(GridTimer_Tick);
            GridTimer.Start();
            GridProcesses.FontChanged += new EventHandler(GridProcesses_FontChanged);
        }

        #endregion

        #region "Form Events"

        private void GridProcesses_FontChanged(object sender, EventArgs e)
        {
            BindCustomServices();
        }

        protected void Restart_Click(object sender, EventArgs e)
        {
            string name = GridProcesses.CurrentRow.Cells[0].Value.ToString();
            try
            {
                using (ServiceController sc = new ServiceController(name, Environment.MachineName))
                {
                    if (sc != null)
                    {
                        WinSvcUtil.RestartWindowsService(name, Environment.MachineName);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            CreateWinFormTimer(3);
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            string name = GridProcesses.CurrentRow.Cells[0].Value.ToString();
            try
            {
                using (ServiceController sc = new ServiceController(name, Environment.MachineName))
                {
                    if (sc != null)
                    {
                        WinSvcUtil.StartWindowsService(name, Environment.MachineName);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            CreateWinFormTimer(3);
        }

        protected void Stop_Click(object sender, EventArgs e)
        {
            string name = GridProcesses.CurrentRow.Cells[0].Value.ToString();
            try
            {
                using (ServiceController sc = new ServiceController(name, Environment.MachineName))
                {
                    if (sc != null)
                    {
                        WinSvcUtil.StopWindowsService(name, Environment.MachineName);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            CreateWinFormTimer(3);
        }

        private void IISMgrUsrCtrl_Load(object sender, EventArgs e)
        {
            BindCustomServices();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            BindCustomServices();
        }

        protected void GridTimer_Tick(object sender, EventArgs e)
        {
            BindCustomServices();
        }

        protected void FormTimer_Tick(object sender, EventArgs e)
        {
            BindCustomServices();
            FormTimer.Stop();
        }

        #endregion

        #region "Methods"

        private void CreateWinFormTimer(int seconds)
        {
            FormTimer.Interval = (seconds * 2000);
            FormTimer.Tick += new EventHandler(FormTimer_Tick);
            FormTimer.Start();
        }

        private void BindCustomServices()
        {
            List<WinSvc> winServices = null;
            try
            {
                string[] svcs;
                if ((WinSvcStr == null) || (WinSvcStr.Length <= 0))
                {
                    svcs = WinServices;
                }
                else
                {
                    svcs = WinSvcStr;
                }
                foreach (string svcName in svcs)
                {
                    using (ServiceController sc = new ServiceController(svcName, Environment.MachineName))
                    {
                        if (winServices == null)
                        {
                            winServices = new List<WinSvc>();
                        }
                        WinSvc svc = new WinSvc();
                        svc.ServiceName = sc.ServiceName;
                        svc.DisplayName = sc.DisplayName;
                        svc.MachineName = sc.MachineName;
                        svc.CanStop = sc.CanStop;
                        svc.CanShutdown = sc.CanShutdown;
                        svc.CurrentSvcType = sc.ServiceType;
                        svc.ServiceHandle = sc.ServiceHandle;
                        svc.Status = sc.Status;
                        svc.LastUpdated = DateTime.Now;
                        winServices.Add(svc);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            GridProcesses.DataSource = winServices;
            // Add the context menu to the grid columns
            foreach (DataGridViewColumn col in GridProcesses.Columns)
            {
                col.ContextMenuStrip = GridContextMenu;
                col.ContextMenuStrip.Items.Add(stopToolStripMenuItem);
                col.ContextMenuStrip.Items.Add(startToolStripMenuItem);
                col.ContextMenuStrip.Items.Add(restartToolStripMenuItem);
            }
        }

        private void BindWinServices()
        {
            List<WinSvc> winServices = null;
            ServiceController[] scs = null;
            try
            {
                using (ServiceController sc = new ServiceController("W3SVC", Environment.MachineName))
                {
                    scs = new ServiceController[1];
                    scs[0] = sc;
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex);
            }
            for (int i = 0; i < scs.Length; i++)
            {
                winServices = new List<WinSvc>(scs.Length);
                WinSvc svc = new WinSvc();
                svc.ServiceName = scs[i].ServiceName;
                svc.DisplayName = scs[i].DisplayName;
                svc.MachineName = scs[i].MachineName;
                svc.CanStop = scs[i].CanStop;
                svc.CanShutdown = scs[i].CanShutdown;
                svc.CurrentSvcType = scs[i].ServiceType;
                svc.ServiceHandle = scs[i].ServiceHandle;
                svc.Status = scs[i].Status;
                svc.LastUpdated = DateTime.Now;
                winServices.Add(svc);
            }
            scs = null;
            GridProcesses.DataSource = winServices;
            // Add the context menu to the grid columns
            foreach (DataGridViewColumn col in GridProcesses.Columns)
            {
                col.ContextMenuStrip = GridContextMenu;
                col.ContextMenuStrip.Items.Add(stopToolStripMenuItem);
                col.ContextMenuStrip.Items.Add(startToolStripMenuItem);
                col.ContextMenuStrip.Items.Add(restartToolStripMenuItem);
            }
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}