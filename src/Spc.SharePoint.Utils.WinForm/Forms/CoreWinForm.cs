namespace Spc.SharePoint.Utils.WinForm.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class CoreWinForm : Form
    {
        private IntPtr _handle;

        internal CoreWinForm()
        {
        }

        /// <summary>
        /// Finds all the controls within the form.
        /// </summary>
        /// <param name="container">The control to search.</param>
        /// <param name="controls">The existing list of controls found.</param>
        /// <returns>A list of controls.</returns>
        internal List<Control> GetAllControls(Control container, ref List<Control> controls)
        {
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is Control)
                {
                    controls.Add(ctrl);
                }
                if (ctrl.Controls.Count > 0)
                {
                    controls = GetAllControls(ctrl, ref controls);
                }
            }
            return controls;
        }

        protected void EnsureFormFits()
        {
            Rectangle rect = Screen.GetWorkingArea(this);
            if (base.Width > rect.Width)
            {
                base.Width = rect.Width;
            }
            if (base.Height > rect.Height)
            {
                base.Height = rect.Height;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            this._handle = base.Handle;
            base.OnHandleCreated(e);
        }

        internal IntPtr ThreadSafeHandle
        {
            get { return (base.InvokeRequired) ? this._handle : base.Handle; }
        }
    }
}