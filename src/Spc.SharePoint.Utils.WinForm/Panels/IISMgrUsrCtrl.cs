﻿namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using Microsoft.Web.Administration;
    using Spc.SharePoint.Utils.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using SWF = System.Windows.Forms;

    public partial class IisMgrUsrCtrl : UserControl
    {
        #region "Properties"

        SWF.Timer FormTimer = new SWF.Timer();
        SWF.Timer GridTimer = new SWF.Timer();

        #endregion

        #region "Constructors"

        public IisMgrUsrCtrl()
        {
            InitializeComponent();
            // Refresh the grid every 15 minutes
            GridTimer.Interval = (15 * 1000 * 60);
            GridTimer.Tick += new EventHandler(GridTimer_Tick);
            GridTimer.Start();
        }

        #endregion

        #region "Form Events"

        private void GridContextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        protected void Recycle_Click(object sender, EventArgs e)
        {
            using (ServerManager iis = new ServerManager())
            {
                ApplicationPool curAppPool = iis.ApplicationPools[GridProcesses.CurrentRow.Cells[0].Value.ToString()];
                curAppPool.Recycle();
            }
            CreateWinFormTimer(3);
        }

        private void IISMgrUsrCtrl_Load(object sender, EventArgs e)
        {
            BindAppPools();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            BindAppPools();
        }

        protected void GridTimer_Tick(object sender, EventArgs e)
        {
            BindAppPools();
        }

        protected void FormTimer_Tick(object sender, EventArgs e)
        {
            BindAppPools();
            FormTimer.Stop();
        }

        #endregion

        #region "Methods"

        private void CreateWinFormTimer(int seconds)
        {
            FormTimer.Interval = (seconds * 1000);
            FormTimer.Tick += new EventHandler(FormTimer_Tick);
            FormTimer.Start();
        }

        private void BindAppPools()
        {
            List<IisAppPool> appPools = null;
            using (ServerManager iis = new ServerManager())
            {
                appPools = new List<IisAppPool>(iis.ApplicationPools.Count);
                for (int i = 0; i < iis.ApplicationPools.Count; i++)
                {
                    appPools.Add(new IisAppPool(iis.ApplicationPools[i]));
                }
            }
            GridProcesses.DataSource = appPools.OrderBy(p => p.ProcessID).ToList();
            // Add the context menu to the grid columns
            foreach (DataGridViewColumn col in GridProcesses.Columns)
            {
                col.ContextMenuStrip = GridContextMenu;
                col.ContextMenuStrip.Items.Add(recycleToolStripMenuItem);
            }
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}