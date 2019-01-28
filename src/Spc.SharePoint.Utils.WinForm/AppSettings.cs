namespace Spc.SharePoint.Utils.WinForm
{
    using Spc.SharePoint.Utils.WinForm.Properties;
    using System;

    internal class AppSettings
    {
        #region "Properties"

        private static Settings _settingsInstance;

        #endregion

        #region "Constructors"

        static AppSettings()
        {
            _settingsInstance = Settings.Default;
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Accessors"

        internal static Settings Instance
        {
            get { return _settingsInstance; }
        }

        #endregion
    }
}