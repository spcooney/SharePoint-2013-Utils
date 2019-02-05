namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Microsoft.SharePoint;
    using Spc.SharePoint.Utils.Core.Helper;
    using Spc.SharePoint.Utils.Core.Models;
    using Spc.SharePoint.Utils.WinForm.Properties;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ServiceProcess;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;
    using SWF = System.Windows.Forms;

    public partial class SPListUsrCtrl : CoreUsrCtrl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(SPListUsrCtrl));
        private SPList curList = null;

        #endregion

        #region "Constructors"

        public SPListUsrCtrl()
        {
            InitializeComponent();
            if (String.IsNullOrEmpty(AppSettings.Instance.LastSPListUrl))
            {
                SetPlaceholderText();
            }
            else
            {
                TxtSPListUrl.Text = AppSettings.Instance.LastSPListUrl;
            }
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
            if (TxtSPListUrl.Text.Trim().Equals(Resources.EnterSPUrl, StringComparison.OrdinalIgnoreCase))
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

        private void TxtSPListUrl_TextChanged(object sender, EventArgs e)
        {
            AppSettings.Instance.LastSPListUrl = TxtSPListUrl.Text;
            AppSettings.Save();
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
                    PopulateSchema(curList.SchemaXml);
                    PopulateListData(curList.Items.GetDataTable());
                }
            }
        }

        private void SetPlaceholderText()
        {
            TxtSPListUrl.Text = Resources.EnterSPUrl;
        }

        private void PopulateListData(DataTable spListData)
        {
            GridData.DataSource = spListData;
        }

        private void PopulateSchema(string xml)
        {
            if (StringUtil.IsNullOrWhitespace(xml))
            {
                RchTxtSchema.Text = "No schema was found";
            }
            StringBuilder sb = new StringBuilder(xml.Length);
            XElement elem = XElement.Parse(xml);
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            xws.IndentChars = "\t";
            xws.NewLineOnAttributes = true;
            using (XmlWriter xmlWr = XmlWriter.Create(sb, xws))
            {
                elem.Save(xmlWr);
            }
            RchTxtSchema.Text = sb.ToString();
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}