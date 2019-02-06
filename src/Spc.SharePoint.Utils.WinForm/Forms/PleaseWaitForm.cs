namespace Spc.SharePoint.Utils.WinForm.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

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