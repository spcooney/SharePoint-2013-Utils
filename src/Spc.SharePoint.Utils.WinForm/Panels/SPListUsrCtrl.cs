namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Microsoft.SharePoint;
    using Spc.SharePoint.Utils.Core.Helper;
    using Spc.SharePoint.Utils.WinForm.Forms;
    using Spc.SharePoint.Utils.WinForm.Properties;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;

    public partial class SPListUsrCtrl : UserControl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(SPListUsrCtrl));
        private SPList curList = null;

        #endregion

        #region "Constructors"

        public SPListUsrCtrl()
        {
            InitializeComponent();
            SetPlaceholderText();
            SplitterGrids.Panel1Collapsed = true;
        }

        #endregion

        #region "Form Events"

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            QueryList();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            QueryList();
        }

        private void BtnQueryProp_Click(object sender, EventArgs e)
        {
            SPList curList = TryGetSPList();
            if (curList == null)
            {
                return;
            }
            try
            {
                RchTxtProperties.Text = curList.GetType().GetProperty(DdListProperties.Text).GetValue(curList, null).ToString();
            }
            catch (SPException spex)
            {
                RchTxtProperties.Text = spex.Message;
                Log.Error(spex);
            }
            catch (Exception ex)
            {
                RchTxtProperties.Text = ex.Message;
                Log.Error(ex);
            }
        }

        private void SplitterControls_SizeChanged(object sender, EventArgs e)
        {
            EnsureSplitterSize();
        }

        private void SplitterControls_Resize(object sender, EventArgs e)
        {
            EnsureSplitterSize();
        }

        private void SplitContProperties_Layout(object sender, LayoutEventArgs e)
        {
            EnsureSplitterSize();
        }

        private void SplitContProperties_SizeChanged(object sender, EventArgs e)
        {
            EnsureSplitterSize();
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
            SetPlaceholderText();
        }

        private void TxtSPListUrl_TextChanged(object sender, EventArgs e)
        {
            AppSettings.Instance.LastSPListUrl = TxtSPListUrl.Text;
            AppSettings.Save();
        }

        #endregion

        #region "Methods"

        private void EnsureSplitterSize()
        {
            SplitterControls.SplitterDistance = 25;
            SplitContProperties.SplitterDistance = 25;
        }

        private SPList TryGetSPList()
        {
            if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
            {
                return null;
            }
            PleaseWaitForm pleaseWait = new PleaseWaitForm();
            try
            {
#if (RELEASE)
                pleaseWait.Show(this.Parent);
                Application.DoEvents();
#endif
                using (SPSite site = new SPSite(TxtSPListUrl.Text))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Uri curUri = new Uri(TxtSPListUrl.Text);
                        return SPListHelper.TryGetListByRelativeUrl(web, curUri.PathAndQuery);
                    }
                }
            }
            catch (SPException spex)
            {
                MessageBox.Show(spex.Message);
                Log.Error(spex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex);
            }
            finally
            {
#if (RELEASE)
                pleaseWait.Close();
#endif
            }
            return null;
        }

        private void QueryList()
        {
            PleaseWaitForm pleaseWait = new PleaseWaitForm();
            try
            {
#if (RELEASE)
                pleaseWait.Show(this.Parent);
                Application.DoEvents();
#endif
                if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
                {
                    return;
                }
                curList = TryGetSPList();
                if (curList == null)
                {
                    return;
                }
                PopulateSchema(curList.SchemaXml);
                PopulateListData(curList, curList.Items.GetDataTable());
                PopulatePropertiesDropDown(curList);
                ListTabs.SelectedTab = TabData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex);
            }
            finally
            {
#if (RELEASE)
                pleaseWait.Close();
#endif
            }
        }

        private void SetPlaceholderText()
        {
            if (StringUtil.IsNotNullOrWhitespace(AppSettings.Instance.LastSPListUrl))
            {
                TxtSPListUrl.Text = AppSettings.Instance.LastSPListUrl;
            }
            if (StringUtil.IsNullOrWhitespace(TxtSPListUrl.Text))
            {
                TxtSPListUrl.Text = Resources.EnterSPUrl;
            }
        }

        private void PopulateListData(SPList list, DataTable spListData)
        {
            try
            {
                GridData.DataSource = spListData;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                // Sometimes SharePoint complains about a column so attempt to re-bind
                GridData.DataSource = curList.Items.GetDataTable();
            }
            GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            // Add the checkboxes to allow the user to show or hide columns
            if ((spListData != null) && (spListData.Columns.Count > 0))
            {
                ChkBoxListFilters.Items.Clear();
                foreach (DataColumn col in spListData.Columns)
                {
                    ChkBoxListFilters.Items.Add(col.ColumnName, true);
                }
                //ChkBoxListFilters.Items.Add("Hide All", false);
                ChkBoxListFilters.ItemCheck += ChkBoxListFilters_ItemCheck;
            }
        }

        private void ChkBoxListFilters_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (GridData.Columns[e.Index] != null)
            {
                // Hiding the column when it's checked
                GridData.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
            }
        }

        private void ListTabs_Selected(object sender, TabControlEventArgs e)
        {
            // Show the column panel if the Data tab is selected
            if (e.TabPage.Text.Equals("Data"))
            {
                SplitterGrids.Panel1Collapsed = false;
                SplitterGrids.Panel1.Show();
            }
            else
            {
                SplitterGrids.Panel1Collapsed = true;
                SplitterGrids.Panel1.Hide();
            }
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
            xws.IndentChars = CharUtil.TabStr;
            xws.NewLineOnAttributes = true;
            using (XmlWriter xmlWr = XmlWriter.Create(sb, xws))
            {
                elem.Save(xmlWr);
            }
            RchTxtSchema.Text = sb.ToString();
        }

        private void PopulatePropertiesDropDown(SPList list)
        {
            DdListProperties.Items.Clear();
            PropertyInfo[] props = list.GetType().GetProperties();
            List<PropertyInfo> sortedProps = props.OrderBy(p => p.Name).ToList();
            foreach (PropertyInfo pi in sortedProps)
            {
                DdListProperties.Items.Add(pi.Name);
            }
        }

        #endregion

        private void TsmiShowAll_Click(object sender, EventArgs e)
        {
            ToggleShowHideChecks(true);
        }

        private void TsmiShowNone_Click(object sender, EventArgs e)
        {
            ToggleShowHideChecks(false);
        }

        private void ToggleShowHideChecks(bool chkd)
        {
            for (int i = 0; i < ChkBoxListFilters.Items.Count; i++)
            {
                ChkBoxListFilters.SetItemChecked(i, chkd);
            }
        }

        #region "Accessors"

        #endregion
    }
}