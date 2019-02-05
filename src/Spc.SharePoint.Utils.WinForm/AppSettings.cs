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

        public static bool Save()
        {
            if (Instance != null)
            {
                Instance.Save();
                return true;
            }
            return false;
        }

        #endregion

        #region "Accessors"

        internal static Settings Instance
        {
            get { return _settingsInstance; }
        }

        #endregion
    }
}