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
        }

        #endregion

        #region "Methods"

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