//=====================================
//  Used to track temporary CS files
//=====================================
public class TempCS
{
    private void BtnSplitFile_Click(object sender, EventArgs e)
    {
        WriteOutput("Starting to split the file: " + TxtBoxFileLoc.Text);
        //PleaseWaitForm pleaseWait = new PleaseWaitForm();
        try
        {
            //pleaseWait.Show(this);
            //Application.DoEvents();
            //ShowProgress(true);
            Application.DoEvents();
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
            //pleaseWait.Close();
            ShowProgress(false);
        }
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
            WriteOutput("Failed to open file: " + OpenFileDlg.FileName + ". " + ex.Message);
        }
    }
}