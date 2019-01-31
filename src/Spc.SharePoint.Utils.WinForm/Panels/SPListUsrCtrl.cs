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

    public partial class SPListUsrCtrl : CoreUsrCtrl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(SPListUsrCtrl));

        #endregion

        #region "Constructors"

        public SPListUsrCtrl()
        {
            InitializeComponent();
        }

        #endregion

        private void BtnRefresh_Click(object sender, EventArgs e)
        {

        }

        #region "Form Events"



        #endregion

        #region "Methods"

        

        #endregion

        #region "Accessors"

        #endregion
    }
}