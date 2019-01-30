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

        #endregion

        #region "Accessors"

        /// <summary>
        /// Tracks the last 50 control focus history.
        /// </summary>
        public List<Control> FocusHistory
        {
            get
            {
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
                if (_ctrlFocusHistory == null)
                {
                    _ctrlFocusHistory = new List<Control>(10);
                }
                _ctrlFocusHistory = value;
            }
        }

        #endregion
    }
}