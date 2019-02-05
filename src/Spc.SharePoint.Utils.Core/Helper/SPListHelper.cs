namespace Spc.SharePoint.Utils.Core.Helper
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using System;

    public static class SPListHelper
    {
        #region "Methods"

        public static SPList TryGetList(SPWeb web, string listTitle)
        {
            SPList list = null;
            try
            {
                list = web.Lists.TryGetList(listTitle);
            }
            catch
            {
            }
            if (list != null)
            {
                return list;
            }
            try
            {
                list = web.Lists[listTitle];
            }
            catch
            {
            }
            if (list != null)
            {
                return list;
            }
            return TryGetListByUrl(web, listTitle);
        }

        public static SPList TryGetListByUrl(SPWeb web, string listUrlName)
        {
            string webRelativeUrl = web.ServerRelativeUrl;
            string listRelativeUrl = String.Empty;
            string listDocRelativeUrl = String.Empty;
            SPList list = null;
            listRelativeUrl = ("/Lists/" + listUrlName);
            listDocRelativeUrl = ("/" + listUrlName);
            string listWebRelativeUrl = SPUrlUtility.CombineUrl(webRelativeUrl, listRelativeUrl);
            string listDocWebRelativeUrl = SPUrlUtility.CombineUrl(webRelativeUrl, listDocRelativeUrl);
            try
            {
                list = web.GetList(listWebRelativeUrl);
            }
            catch
            {
            }
            if (list != null)
            {
                return list;
            }
            try
            {
                list = web.GetList(listDocWebRelativeUrl);
            }
            catch
            {
            }
            return list;
        }

        public static SPList TryGetListByRelativeUrl(SPWeb web, string listUrl)
        {
            SPList list = null;
            try
            {
                list = web.GetList(listUrl);
            }
            catch
            {
            }
            return list;
        }

        public static SPList TryGetListByUrl(SPWeb web, string listUrlName, SPBaseType baseType)
        {
            string WebRelativeUrl = web.ServerRelativeUrl;
            string ListRelativeUrl = String.Empty;
            SPList list = null;
            if (baseType == SPBaseType.GenericList)
            {
                ListRelativeUrl = ("/Lists/" + listUrlName);
            }
            else
            {
                ListRelativeUrl = ("/" + listUrlName);
            }
            string ListWebRelativeUrl = SPUrlUtility.CombineUrl(WebRelativeUrl, ListRelativeUrl);
            try
            {
                list = web.GetList(ListWebRelativeUrl);
            }
            catch
            {
            }
            return list;
        }

        #endregion

        #region "Enums"

        public enum FieldOptions
        {
            None = 0,
            Required = 1,
            ReadOnly = 2,
            Sealed = 4,
            HideFromNewForm = 8,
            HideFromEditForm = 16,
            HideFromDisplayForm = 32,
            HideFromListSettings = 64
        }

        #endregion
    }
}