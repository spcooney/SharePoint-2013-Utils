namespace Spc.SharePoint.Utils.WinForm.Panels
{
    using log4net;
    using Spc.SharePoint.Utils.Core.Helper;
    using Spc.SharePoint.Utils.WinForm.Forms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public partial class FileSplitterUsrCtrl : UserControl
    {
        #region "Properties"

        private static readonly ILog Log = LogManager.GetLogger(typeof(FileSplitterUsrCtrl));
        private BackgroundWorker bwReadLines = null;
        private BackgroundWorker bwSplitFile = null;

        #endregion

        #region "Constructors"

        public FileSplitterUsrCtrl()
        {
            InitializeComponent();
            EnsureSplitDistance();
            ModifyBtnAbility(BtnCancel, false);
            ModifyBtnAbility(BtnSplitFile, false);
            ModifyBtnAbility(BtnSelectFile, true);
        }

        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            StopBgWorker(bwReadLines);
            StopBgWorker(bwSplitFile);
            ModifyBtnAbility(BtnCancel, true);
            ModifyBtnAbility(BtnSplitFile, true);
            ModifyBtnAbility(BtnSelectFile, true);
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                TxtBoxFileLoc.Text = OpenFileDlg.FileName;
                ShowProgress(true);
                bwReadLines = new BackgroundWorker();
                bwReadLines.WorkerReportsProgress = true;
                bwReadLines.WorkerSupportsCancellation = true;
                bwReadLines.DoWork += BackgroundWorker_ReadFileLines;
                bwReadLines.ProgressChanged += BackgroundWorker_ReadProgressChanged;
                bwReadLines.RunWorkerCompleted += BackgroundWorker_ReadCompleted;
                if (bwReadLines.IsBusy != true)
                {
                    WriteOutput("Starting to count the lines in file " + TxtBoxFileLoc.Text + "...");
                    bwReadLines.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorker_ReadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                WriteOutput("Reading the file lines stopped");
            }
            else if (!(e.Error == null))
            {
                WriteOutput(e.Error.Message);
            }
            if (e.Result != null)
            {
                NumSelLinesPerFile.Value = ConvertUtil.ToInt32(e.Result.ToString(), 1);
                NumSelLinesPerFile.Maximum = NumSelLinesPerFile.Value;
                NumSelLinesPerFile.Minimum = 1;
                WriteOutput(TxtBoxFileLoc.Text + " has " + NumSelLinesPerFile.Value + " lines.  Please select how many lines you want per file.");
                ModifyBtnAbility(BtnSplitFile, true);
            }
            ModifyBtnAbility(BtnSelectFile, true);
            ShowProgress(false);
        }

        protected void BackgroundWorker_ReadFileLines(object sender, DoWorkEventArgs e)
        {
            ModifyBtnAbility(BtnSelectFile, false);
            // Can't cancel during the read
            ModifyBtnAbility(BtnCancel, false);
            try
            {
                e.Result = File.ReadAllLines(TxtBoxFileLoc.Text).Length;
            }
            catch (Exception ex)
            {
                e.Result = 0;
                Log.Error(ex);
                bwReadLines.ReportProgress(100, "Failed to open file: " + OpenFileDlg.FileName + ". " + ex.Message);
            }
        }

        private void BackgroundWorker_ReadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                WriteOutput(e.UserState.ToString());
            }
        }

        private void BtnSplitFile_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TxtBoxFileLoc.Text))
            {
                WriteOutput("Please select a file first.");
                return;
            }
            ShowProgress(true);
            bwSplitFile = new BackgroundWorker();
            bwSplitFile.WorkerReportsProgress = true;
            bwSplitFile.WorkerSupportsCancellation = true;
            bwSplitFile.DoWork += BackgroundWorker_SplitFileLines;
            bwSplitFile.ProgressChanged += BackgroundWorker_SplitProgressChanged;
            bwSplitFile.RunWorkerCompleted += BackgroundWorker_SplitCompleted;
            if (bwSplitFile.IsBusy != true)
            {
                WriteOutput("Starting to split the file into " + NumSelLinesPerFile.Value + " lines...");
                ModifyBtnAbility(BtnCancel, true);
                ModifyBtnAbility(BtnSelectFile, false);
                ModifyBtnAbility(BtnSplitFile, false);
                bwSplitFile.RunWorkerAsync();
            }
        }

        private void BackgroundWorker_SplitFileLines(object sender, DoWorkEventArgs e)
        {
            try
            {
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
                    if (bwSplitFile.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    sb.AppendLine(lines[i]);
                    lineCount++;
                    if ((lineCount == NumSelLinesPerFile.Value) || (i >= (numOfLines - 1)))
                    {
                        string fileName = (fileNameNoEx + CharUtil.UnderscoreStr + fileNum + fileExt);
                        File.WriteAllText(Path.Combine(filePath, fileName), sb.ToString());
                        bwSplitFile.ReportProgress(0, "File created: " + Path.Combine(filePath, fileName));
                        sb = new StringBuilder();
                        fileNum++;
                        lineCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                bwSplitFile.ReportProgress(100, "Failed to split the file: " + OpenFileDlg.FileName + ". " + ex.Message);
            }
        }

        private void BackgroundWorker_SplitProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                WriteOutput(e.UserState.ToString());
            }
        }

        private void BackgroundWorker_SplitCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                WriteOutput("Splitting the file was stopped.");
            }
            else if (!(e.Error == null))
            {
                WriteOutput(e.Error.Message);
            }
            if (e.Result != null)
            {
                WriteOutput("Split complete.");
            }
            ModifyBtnAbility(BtnCancel, false);
            ModifyBtnAbility(BtnSplitFile, true);
            ModifyBtnAbility(BtnSelectFile, true);
            ShowProgress(false);
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

        /// <summary>
        /// Sets the button to enabled or disabled.
        /// </summary>
        /// <param name="bttn">The button.</param>
        /// <param name="isEnabled">True, to enable.  Otherwise, disabled.</param>
        private void ModifyBtnAbility(Button bttn, bool isEnabled)
        {
            if ((this != null) && (bttn != null))
            {
                if (InvokeRequired)
                {
                    try
                    {
                        this.Invoke((Action)delegate()
                        {
                            bttn.Enabled = isEnabled;
                            if (!isEnabled)
                            {
                                bttn.BackColor = Color.Silver;
                            }
                            else
                            {
                                bttn.BackColor = Color.FromArgb(213, 229, 251);
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.LogError(ex);
                    }
                }
                else
                {
                    bttn.Enabled = isEnabled;
                    if (!isEnabled)
                    {
                        bttn.BackColor = Color.Silver;
                    }
                    else
                    {
                        bttn.BackColor = Color.FromArgb(213, 229, 251);
                    }
                }
            }
        }

        private void ShowProgress(bool show)
        {
            if (this.Parent != null)
            {
                StatusStrip tspb = this.ParentForm.Controls["StsBar"] as StatusStrip;
                tspb.Items["ToolStripProgress"].Visible = show;
            }
        }

        /// <summary>
        /// Sends the background worker cancel async if the worker is not busy or null.
        /// </summary>
        /// <param name="worker">The background worker.</param>
        private void StopBgWorker(BackgroundWorker worker)
        {
            if ((worker != null) && (worker.IsBusy) && (worker.WorkerSupportsCancellation))
            {
                worker.CancelAsync();
            }
        }

        private void WriteOutput(string message)
        {
            if ((this != null) && (RchTxtOutput != null))
            {
                string lineMsg = (String.Format(WinFormStrConstants.ConsoleEntryFormat, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), message) + Environment.NewLine);
                if (!String.IsNullOrWhiteSpace(message) && (RchTxtOutput != null))
                {
                    try
                    {
                        RchTxtOutput.AppendText(lineMsg);
                        // Set the current caret position to the end
                        RchTxtOutput.SelectionStart = RchTxtOutput.Text.Length;
                        // Scroll it automatically
                        RchTxtOutput.ScrollToCaret();
                        Log4NetHelper.LogInfo(typeof(FileSplitterUsrCtrl), lineMsg);
                    }
                    catch (Exception ex)
                    {
                        // Likely happens when the form is being disposed
                        Log4NetHelper.LogError(ex);
                    }
                }
            }
        }

        #endregion

        #region "Accessors"

        #endregion
    }
}