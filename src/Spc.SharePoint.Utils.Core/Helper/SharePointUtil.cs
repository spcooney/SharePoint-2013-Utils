namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Globalization;

    public static class SharePointUtil
    {
        #region "Properties"

        private const string DefaultPage = "Default.aspx";
        private const string AccessDeniedPage = "AccessDenied.aspx";
        private const string ErrorPage = "error.aspx";
        private const string BlogCategoryPage = "Category.aspx";
        private const string BlogPostPage = "Post.aspx";
        private const string BlogDatePage = "Date.asxp";
        private const string BlogMontlyArchivePage = "MonthlyArchive.aspx";
        private const string AspPageSuffix = ".aspx";
        private const string AspWebServiceSuffix = ".svc";
        private const string AspHandlerSuffix = ".ashx";
        private const string AspActiveServiceSuffix = ".asmx";
        private const string SPTemplatePath = "Template\\";
        private const string SPLayoutsRelativePath = SPTemplatePath + "layouts";
        private const string SPImagesRelativePath = SPTemplatePath + "images";
        private const string SPLayoutsUrl = "/_layouts/";

        public static string[] AspWebSuffixes = new string[4]
        {
            AspPageSuffix,
            AspWebServiceSuffix,
            AspHandlerSuffix,
            AspActiveServiceSuffix
        };

        public static string[] BlogPages = new string[5]
        {
            BlogCategoryPage,
            BlogPostPage,
            DefaultPage,
            BlogDatePage,
            BlogMontlyArchivePage
        };

        public static string[] GlobalListNames = new string[10]
        {
            "Lists",
            "Docs",
            "WebParts",
            "ComMd",
            "Webs",
            "Workflow",
            "WFTemp",
            "Solutions",
            "Self",
            "UserInfo"
        };

        #endregion

        #region "Methods"

        public static string AppendDialogToUrl(string url)
        {
            if (IsDialog(url))
            {
                return url;
            }
            return String.Format(CultureInfo.InvariantCulture, "{0}{1}IsDlg=1", url, (url.IndexOf(CharUtil.QuestionMark) < 0 ? CharUtil.QuestionMark : CharUtil.Ampersand));
        }

        public static bool IsDialog(string url)
        {
            return ((url.Contains("?IsDlg=1")) || (url.Contains("&IsDlg=1")));
        }

        #endregion
    }
}