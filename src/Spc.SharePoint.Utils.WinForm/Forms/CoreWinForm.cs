namespace Spc.SharePoint.Utils.WinForm.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// The core form of the entire application.
    /// </summary>
    public class CoreWinForm : Form
    {
        #region "Properties"

        private IntPtr _handle;

        #endregion

        #region "Constructors"

        public CoreWinForm()
        {
        }

        #endregion

        #region "Methods"

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

        #endregion

        #region "Accessors"

        /// <summary>
        /// Returns the assembly title.
        /// </summary>
        /// <remarks>SharePoint 2013 Util</remarks>
        protected string AssemblyTitle
        {
            get
            {
                if (Assembly.GetExecutingAssembly() == null)
                {
                    return WinFormStrConstants.ApplicationTitle;
                }
                return ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false)).Title;
            }
        }

        /// <summary>
        /// Returns the assembly title and version.
        /// </summary>
        /// <remarks>SharePoint 2013 Util vX.X.X.X.</remarks>
        protected string AssemblyTitleWithVersion
        {
            get
            {
                return String.Concat(AssemblyTitle, AssemblyVersion);
            }
        }

        /// <summary>
        /// Returns the assembly version.
        /// </summary>
        /// <remarks>vX.X.X.X.</remarks>
        protected string AssemblyVersion
        {
            get
            {
                if ((Assembly.GetExecutingAssembly() == null) || (Assembly.GetExecutingAssembly().GetName() == null))
                {
                    return String.Empty;
                }
                return String.Concat(" v", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
        }

        #endregion
    }
}