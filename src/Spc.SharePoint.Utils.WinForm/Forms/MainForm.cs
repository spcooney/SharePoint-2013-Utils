namespace Spc.SharePoint.Utils.WinForm.Forms
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class MainForm : CoreWinForm
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(MainForm));
        private List<Control> _ctrlFocusHistory;

        #endregion

        #region "Constructors"

        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Binds a focus event handler to each Windows form control.
        /// </summary>
        private void BindFocusEventToControls()
        {
            List<Control> ctrls = new List<Control>();
            base.GetAllControls(this, ref ctrls);
            foreach (Control c in ctrls)
            {
                c.GotFocus += Control_GotFocus;
            }
        }

        /// <summary>
        /// Builds the navigation (tree view).
        /// </summary>
        private void BindTreeNodes()
        {
            TreeNode tnDevTools = new TreeNode();
            tnDevTools.Name = WinFormStrConstants.NodeNames.NodeDevTools;
            tnDevTools.Text = WinFormStrConstants.NodeText.NodeDevTools;
            tnDevTools.ImageIndex = 6;
            tnDevTools.SelectedImageIndex = 6;
            TreeNode tnJs = new TreeNode();
            tnJs.Name = WinFormStrConstants.NodeNames.NodeJavaScript;
            tnJs.Text = WinFormStrConstants.NodeText.NodeJavaScript;
            tnJs.ImageIndex = 1;
            tnJs.SelectedImageIndex = 1;
            TreeNode tnJsMin = new TreeNode();
            tnJsMin.Name = WinFormStrConstants.NodeNames.NodeJavaScriptMinify;
            tnJsMin.Text = WinFormStrConstants.NodeText.NodeJavaScriptMinify;
            tnJsMin.ImageIndex = 8;
            tnJsMin.SelectedImageIndex = 11;
            TreeNode tnCss = new TreeNode();
            tnCss.Name = WinFormStrConstants.NodeNames.NodeCss;
            tnCss.Text = WinFormStrConstants.NodeText.NodeCss;
            tnCss.ImageIndex = 2;
            tnCss.SelectedImageIndex = 2;
            TreeNode tnCssMin = new TreeNode();
            tnCssMin.Name = WinFormStrConstants.NodeNames.NodeCssMinify;
            tnCssMin.Text = WinFormStrConstants.NodeText.NodeCssMinify;
            tnCssMin.ImageIndex = 8;
            tnCssMin.SelectedImageIndex = 11;
            TreeNode tnClipboard = new TreeNode();
            tnClipboard.Name = WinFormStrConstants.NodeNames.NodeClipboard;
            tnClipboard.Text = WinFormStrConstants.NodeText.NodeClipboard;
            tnClipboard.ImageIndex = 8;
            tnClipboard.SelectedImageIndex = 11;
            TreeNode tnIIS = new TreeNode();
            tnIIS.Name = WinFormStrConstants.NodeNames.NodeIIS;
            tnIIS.Text = WinFormStrConstants.NodeText.NodeIIS;
            tnIIS.ImageIndex = 5;
            tnIIS.SelectedImageIndex = 5;
            TreeNode tnIISProcesses = new TreeNode();
            tnIISProcesses.Name = WinFormStrConstants.NodeNames.NodeIISProcesses;
            tnIISProcesses.Text = WinFormStrConstants.NodeText.NodeIISProcesses;
            tnIISProcesses.ImageIndex = 8;
            tnIISProcesses.SelectedImageIndex = 11;
            TreeNode tnWin = new TreeNode();
            tnWin.Name = WinFormStrConstants.NodeNames.NodeWindows;
            tnWin.Text = WinFormStrConstants.NodeText.NodeWindows;
            tnWin.ImageIndex = 9;
            tnWin.SelectedImageIndex = 9;
            TreeNode tnWinSvcs = new TreeNode();
            tnWinSvcs.Name = WinFormStrConstants.NodeNames.NodeWinServices;
            tnWinSvcs.Text = WinFormStrConstants.NodeText.NodeWinServices;
            tnWinSvcs.ImageIndex = 8;
            tnWinSvcs.SelectedImageIndex = 11;
            // Build the tree
            tnJs.Nodes.Add(tnJsMin);
            tnCss.Nodes.Add(tnCssMin);
            tnIIS.Nodes.Add(tnIISProcesses);
            tnWin.Nodes.Add(tnClipboard);
            tnWin.Nodes.Add(tnWinSvcs);
            tnDevTools.Nodes.Add(tnJs);
            tnDevTools.Nodes.Add(tnCss);
            tnDevTools.Nodes.Add(tnIIS);
            tnDevTools.Nodes.Add(tnWin);
            TreeNav.Nodes.Add(tnDevTools);
            TreeNav.ExpandAll();
            string selNodeName = AppSettings.Instance.LastSelectedNodeName;
            if ((!String.IsNullOrWhiteSpace(selNodeName)) && (this.TreeNav != null))
            {
                // Find the node in the tree
                TreeNode[] foundNodes = this.TreeNav.Nodes.Find(selNodeName, true);
                if ((foundNodes != null) && (foundNodes.Length > 0))
                {
                    // Select the saved node
                    this.TreeNav.SelectedNode = foundNodes[0];
                    this.DetermineCurrentControl(this.TreeNav.SelectedNode);
                }
            }
        }

        /// <summary>
        /// Adds a user control to the main panel.
        /// </summary>
        /// <param name="userControl">The user control to bind to the main panel.</param>
        private void BindUserControl(UserControl userControl)
        {
            // Determine if the panel already has a control
            if (PnlMain.HasChildren)
            {
                // Don't remove the same control that's already loaded
                if (PnlMain.Controls[0] != userControl)
                {
                    // Remove the current control from the panel
                    PnlMain.Controls.Clear();
                }
            }
            // Add the control to the panel
            PnlMain.Controls.Add(userControl);
        }

        private void InitForm()
        {
            this.Text = base.AssemblyTitleWithVersion;
            if (AppSettings.Instance.IsMaximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
            SpltCnt.SplitterDistance = AppSettings.Instance.SplitterDistance;
            // Attach the focus event to all of the controls
            BindFocusEventToControls();
            // Build the tree view
            this.BindTreeNodes();
            this.TreeNav.NodeMouseClick += TreeNav_NodeMouseClick;
            FontSize10pxMenuStr.Checked = true;
            LoadFontSize();
        }

        private void UpdateFontSize(float fontSize)
        {
            List<Control> ctrls = new List<Control>();
            this.GetAllControls(this, ref ctrls);
            foreach (Control c in ctrls)
            {
                if (c.GetType().BaseType == typeof(TextBoxBase))
                {
                    c.Font = new Font(c.Font.Name, fontSize);
                }
            }
        }

        private void UncheckFontSizes()
        {
            foreach (ToolStripMenuItem item in FontSizeMenuStr.DropDownItems)
            {
                if (item.Checked)
                {
                    item.Checked = false;
                }
            }
        }

        private void LoadFontSize()
        {
            float fontSize = AppSettings.Instance.TextFontSize;
            if (fontSize == 8)
            {
                FontSize8pxMenuStr_Click(null, null);
            }
            else if (fontSize == 10)
            {
                FontSize10pxMenuStr_Click(null, null);
            }
            else if (fontSize == 12)
            {
                FontSize12pxMenuStr_Click(null, null);
            }
            else if (fontSize == 14)
            {
                FontSize14pxMenuStr_Click(null, null);
            }
            else if (fontSize == 16)
            {
                FontSize16pxMenuStr_Click(null, null);
            }
        }

        #endregion

        #region "Form Events"

        private void Control_GotFocus(object sender, EventArgs e)
        {
            // Log the control that got focus
            this.FocusHistory.Add((Control)sender);
        }

        /// <summary>
        /// Loads the appropriate user control based on the tree node.
        /// </summary>
        /// <param name="node">The tree node.</param>
        private void DetermineCurrentControl(TreeNode node)
        {
            switch (node.Name)
            {
                //case WinFormStrConstants.NodeNames.NodeJavaScriptMinify:
                //    this.BindUserControl(this.UsrCtrlJsMinify);
                //    break;
                //case WinFormStrConstants.NodeNames.NodeCssMinify:
                //    this.BindUserControl(this.UsrCtrlCssMinify);
                //    break;
                //case WinFormStrConstants.NodeNames.NodeClipboard:
                //    this.BindUserControl(this.UsrCtrlClipboard);
                //    break;
                //case WinFormStrConstants.NodeNames.NodeIISProcesses:
                //    this.BindUserControl(this.UsrCtrlIISMgr);
                //    break;
                //case WinFormStrConstants.NodeNames.NodeWinServices:
                //    this.BindUserControl(this.UsrCtrlWinServices);
                //    break;
            }
            AppSettings.Instance.LastSelectedNodeName = node.Name;
            AppSettings.Instance.Save();
        }

        private void FontSize8pxMenuStr_Click(object sender, EventArgs e)
        {
            TreeNav.Font = new Font(TreeNav.Font.Name, 8f);
            UpdateFontSize(8f);
            UncheckFontSizes();
            FontSize8pxMenuStr.Checked = true;
        }

        private void FontSize10pxMenuStr_Click(object sender, EventArgs e)
        {
            TreeNav.Font = new Font(TreeNav.Font.Name, 10f);
            UpdateFontSize(10f);
            UncheckFontSizes();
            FontSize10pxMenuStr.Checked = true;
        }

        private void FontSize12pxMenuStr_Click(object sender, EventArgs e)
        {
            TreeNav.Font = new Font(TreeNav.Font.Name, 12f);
            UpdateFontSize(12f);
            UncheckFontSizes();
            FontSize12pxMenuStr.Checked = true;
        }

        private void FontSize14pxMenuStr_Click(object sender, EventArgs e)
        {
            TreeNav.Font = new Font(TreeNav.Font.Name, 14f);
            UpdateFontSize(14f);
            UncheckFontSizes();
            FontSize14pxMenuStr.Checked = true;
        }

        private void FontSize16pxMenuStr_Click(object sender, EventArgs e)
        {
            TreeNav.Font = new Font(TreeNav.Font.Name, 16f);
            UpdateFontSize(16f);
            UncheckFontSizes();
            FontSize16pxMenuStr.Checked = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppSettings.Instance.IsMaximized = (this.WindowState == FormWindowState.Maximized);
            AppSettings.Instance.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void SpltCnt_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Take the focus away from the splitter
            TreeNav.Focus();
        }

        private void SpltCnt_DoubleClick(object sender, EventArgs e)
        {
            if ((SpltCnt.SplitterDistance < 135) || (SpltCnt.SplitterDistance > 175))
            {
                SpltCnt.SplitterDistance = 160;
            }
        }

        private void TreeNav_AfterExpand(object sender, TreeViewEventArgs e)
        {

        }

        private void TreeNav_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.DetermineCurrentControl(e.Node);
        }

        #endregion

        #region "Accessors"

        /// <summary>
        /// Tracks the last 50 control focus history.
        /// </summary>
        public List<Control> FocusHistory
        {
            get
            {
                if (_ctrlFocusHistory == null)
                {
                    _ctrlFocusHistory = new List<Control>(10);
                }
                // Check if the focus history has more than 50 items
                if (_ctrlFocusHistory.Count > 50)
                {
                    // Remove the last 25 items added to the focus history
                    _ctrlFocusHistory.RemoveRange(25, (_ctrlFocusHistory.Count - 25));
                }
                return _ctrlFocusHistory;
            }
            set
            {
                _ctrlFocusHistory = value;
            }
        }

        #endregion
    }
}