namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Spc.SharePoint.Utils.Core.Helper;
    using Spc.SharePoint.Utils.WinForm.Forms;
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public partial class FileSplitterUsrCtrl : UserControl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(FileSplitterUsrCtrl));

        #endregion

        #region "Constructors"

        public FileSplitterUsrCtrl()
        {
            InitializeComponent();
            EnsureSplitDistance();
        }

        #endregion

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                TxtBoxFileLoc.Text = OpenFileDlg.FileName;
                PleaseWaitForm pleaseWait = new PleaseWaitForm();
                try
                {
                    WriteOutput("Opening file: " + OpenFileDlg.FileName);
                    pleaseWait.Show(this);
                    Application.DoEvents();
                    NumSelLinesPerFile.Value = File.ReadAllLines(OpenFileDlg.FileName).Length;
                    NumSelLinesPerFile.Maximum = NumSelLinesPerFile.Value;
                    NumSelLinesPerFile.Minimum = 1;
                    WriteOutput("Select the number of lines per file");
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    WriteOutput("Failed to open file: " + OpenFileDlg.FileName + ". " + ex.Message);
                }
                finally
                {
                    pleaseWait.Close();
                }
            }
        }

        private void BtnSplitFile_Click(object sender, EventArgs e)
        {
            try
            {
                WriteOutput("Starting to split the file: " + TxtBoxFileLoc.Text);
                string[] lines = File.ReadAllLines(TxtBoxFileLoc.Text);
                int numOfLines = lines.Length;
                int lineCount = 0;
                int fileNum = 1;
                string fileNameNoEx = Path.GetFileNameWithoutExtension(TxtBoxFileLoc.Text);
                string filePath = Path.GetDirectoryName(TxtBoxFileLoc.Text);
                string fileExt = Path.GetExtension(TxtBoxFileLoc.Text);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < numOfLines; i++)
                {
                    sb.AppendLine(lines[i]);
                    lineCount++;
                    if ((lineCount == NumSelLinesPerFile.Value) || (i >= (numOfLines - 1)))
                    {
                        string fileName = (fileNameNoEx + CharUtil.UnderscoreStr + fileNum + fileExt);
                        File.WriteAllText(Path.Combine(filePath, fileName), sb.ToString());
                        WriteOutput("File created: " + Path.Combine(filePath, fileName));
                        sb = new StringBuilder();
                        fileNum++;
                        lineCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                WriteOutput("Failed to split the file: " + OpenFileDlg.FileName);
            }
            finally
            {
                WriteOutput("Split complete");
            }
        }

        private void SplContainerOutput_Panel1_Resize(object sender, EventArgs e)
        {
            EnsureSplitDistance();
        }

        private void SplContainerOutput_SizeChanged(object sender, EventArgs e)
        {
            EnsureSplitDistance();
        }

        #region "Form Events"

        #endregion

        #region "Methods"

        private void EnsureSplitDistance()
        {
            SplContainerOutput.SplitterDistance = 25;
        }

        private void WriteOutput(string message)
        {
            RchTxtOutput.AppendText(String.Format(WinFormStrConstants.ConsoleEntryFormat, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), message) + Environment.NewLine);
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}