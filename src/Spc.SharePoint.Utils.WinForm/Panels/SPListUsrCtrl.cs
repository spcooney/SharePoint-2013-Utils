namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Microsoft.SharePoint;
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
        private const string SPListAbsUrl = "Please enter your SharePoint list absolute URL...";
        private SPList curList = null;

        #endregion

        #region "Constructors"

        public SPListUsrCtrl()
        {
            InitializeComponent();
            SetPlaceholderText();
        }

        #endregion

        #region "Form Events"

        private void BtnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            QueryList();
        }

        private void TxtSPListUrl_Enter(object sender, EventArgs e)
        {
            if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
            {
                return;
            }
            if (TxtSPListUrl.Text.Trim().Equals(SPListAbsUrl, StringComparison.OrdinalIgnoreCase))
            {
                TxtSPListUrl.Text = String.Empty;
            }
        }

        private void TxtSPListUrl_Leave(object sender, EventArgs e)
        {
            if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
            {
                SetPlaceholderText();
            }
        }

        #endregion

        #region "Methods"

        private void QueryList()
        {
            if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
            {
                return;
            }
            using (SPSite site = new SPSite(TxtSPListUrl.Text))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    Uri curUri = new Uri(TxtSPListUrl.Text);
                    curList = SPListHelper.TryGetListByRelativeUrl(web, curUri.PathAndQuery);

                }
            }
        }

        private void SetPlaceholderText()
        {
            TxtSPListUrl.Text = SPListAbsUrl;
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}