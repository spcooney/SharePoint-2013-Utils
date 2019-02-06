namespace Spc.SharePoint.Utils.WinForm.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// Form used for showing progress in the application.
    /// </summary>
    public partial class PleaseWaitForm : Form
    {
        #region "Properties"

        private const int CP_NOCLOSE_BUTTON = 0x200;

        #endregion

        #region "Constructors"

        public PleaseWaitForm()
        {
            InitializeComponent();
        }

        #endregion

        #region "Accessors"

        /// <summary>
        /// Prevents the window from being closed.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = (myCp.ClassStyle | CP_NOCLOSE_BUTTON);
                return myCp;
            }
        }

        #endregion
    }
}