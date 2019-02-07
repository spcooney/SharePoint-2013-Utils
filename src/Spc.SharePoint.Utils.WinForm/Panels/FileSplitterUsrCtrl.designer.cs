namespace Spc.SharePoint.Utils.WinForm.Panels
{
    partial class FileSplitterUsrCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.TxtBoxFileLoc = new System.Windows.Forms.TextBox();
            this.SplContainerOutput = new System.Windows.Forms.SplitContainer();
            this.BtnSplitFile = new System.Windows.Forms.Button();
            this.NumSelLinesPerFile = new System.Windows.Forms.NumericUpDown();
            this.LblSplitFileCount = new System.Windows.Forms.Label();
            this.RchTxtOutput = new System.Windows.Forms.RichTextBox();
            this.TblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.PicBoxTitle = new System.Windows.Forms.PictureBox();
            this.LblHeader = new System.Windows.Forms.Label();
            this.recycleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplContainerOutput)).BeginInit();
            this.SplContainerOutput.Panel1.SuspendLayout();
            this.SplContainerOutput.Panel2.SuspendLayout();
            this.SplContainerOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumSelLinesPerFile)).BeginInit();
            this.TblHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // Splitter
            // 
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Splitter.IsSplitterFixed = true;
            this.Splitter.Location = new System.Drawing.Point(0, 25);
            this.Splitter.Name = "Splitter";
            this.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.Controls.Add(this.BtnSelectFile);
            this.Splitter.Panel1.Controls.Add(this.TxtBoxFileLoc);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.SplContainerOutput);
            this.Splitter.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Splitter.Size = new System.Drawing.Size(992, 581);
            this.Splitter.SplitterDistance = 28;
            this.Splitter.SplitterWidth = 1;
            this.Splitter.TabIndex = 1;
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnSelectFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSelectFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSelectFile.ForeColor = System.Drawing.Color.Black;
            this.BtnSelectFile.Location = new System.Drawing.Point(644, 3);
            this.BtnSelectFile.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(116, 23);
            this.BtnSelectFile.TabIndex = 1;
            this.BtnSelectFile.Text = "Open File";
            this.BtnSelectFile.UseVisualStyleBackColor = false;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // TxtBoxFileLoc
            // 
            this.TxtBoxFileLoc.Location = new System.Drawing.Point(3, 4);
            this.TxtBoxFileLoc.Name = "TxtBoxFileLoc";
            this.TxtBoxFileLoc.ReadOnly = true;
            this.TxtBoxFileLoc.Size = new System.Drawing.Size(636, 20);
            this.TxtBoxFileLoc.TabIndex = 0;
            // 
            // SplContainerOutput
            // 
            this.SplContainerOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplContainerOutput.IsSplitterFixed = true;
            this.SplContainerOutput.Location = new System.Drawing.Point(3, 3);
            this.SplContainerOutput.Name = "SplContainerOutput";
            this.SplContainerOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplContainerOutput.Panel1
            // 
            this.SplContainerOutput.Panel1.Controls.Add(this.BtnSplitFile);
            this.SplContainerOutput.Panel1.Controls.Add(this.NumSelLinesPerFile);
            this.SplContainerOutput.Panel1.Controls.Add(this.LblSplitFileCount);
            this.SplContainerOutput.Panel1.Resize += new System.EventHandler(this.SplContainerOutput_Panel1_Resize);
            // 
            // SplContainerOutput.Panel2
            // 
            this.SplContainerOutput.Panel2.Controls.Add(this.RchTxtOutput);
            this.SplContainerOutput.Size = new System.Drawing.Size(986, 546);
            this.SplContainerOutput.SplitterDistance = 25;
            this.SplContainerOutput.SplitterWidth = 1;
            this.SplContainerOutput.TabIndex = 1;
            this.SplContainerOutput.SizeChanged += new System.EventHandler(this.SplContainerOutput_SizeChanged);
            // 
            // BtnSplitFile
            // 
            this.BtnSplitFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnSplitFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSplitFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnSplitFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSplitFile.ForeColor = System.Drawing.Color.Black;
            this.BtnSplitFile.Location = new System.Drawing.Point(275, 2);
            this.BtnSplitFile.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSplitFile.Name = "BtnSplitFile";
            this.BtnSplitFile.Size = new System.Drawing.Size(116, 23);
            this.BtnSplitFile.TabIndex = 2;
            this.BtnSplitFile.Text = "Split File";
            this.BtnSplitFile.UseVisualStyleBackColor = false;
            this.BtnSplitFile.Click += new System.EventHandler(this.BtnSplitFile_Click);
            // 
            // NumSelLinesPerFile
            // 
            this.NumSelLinesPerFile.Location = new System.Drawing.Point(168, 4);
            this.NumSelLinesPerFile.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.NumSelLinesPerFile.Name = "NumSelLinesPerFile";
            this.NumSelLinesPerFile.Size = new System.Drawing.Size(102, 20);
            this.NumSelLinesPerFile.TabIndex = 2;
            // 
            // LblSplitFileCount
            // 
            this.LblSplitFileCount.AutoSize = true;
            this.LblSplitFileCount.Location = new System.Drawing.Point(0, 6);
            this.LblSplitFileCount.Name = "LblSplitFileCount";
            this.LblSplitFileCount.Size = new System.Drawing.Size(166, 13);
            this.LblSplitFileCount.TabIndex = 0;
            this.LblSplitFileCount.Text = "Select the number of lines per file:";
            // 
            // RchTxtOutput
            // 
            this.RchTxtOutput.BackColor = System.Drawing.SystemColors.Control;
            this.RchTxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RchTxtOutput.Location = new System.Drawing.Point(0, 0);
            this.RchTxtOutput.Name = "RchTxtOutput";
            this.RchTxtOutput.Size = new System.Drawing.Size(986, 520);
            this.RchTxtOutput.TabIndex = 0;
            this.RchTxtOutput.Text = "";
            // 
            // TblHeader
            // 
            this.TblHeader.ColumnCount = 2;
            this.TblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TblHeader.Controls.Add(this.PicBoxTitle, 0, 0);
            this.TblHeader.Controls.Add(this.LblHeader, 1, 0);
            this.TblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.TblHeader.Location = new System.Drawing.Point(0, 0);
            this.TblHeader.Name = "TblHeader";
            this.TblHeader.RowCount = 1;
            this.TblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TblHeader.Size = new System.Drawing.Size(992, 25);
            this.TblHeader.TabIndex = 3;
            // 
            // PicBoxTitle
            // 
            this.PicBoxTitle.BackgroundImage = global::Spc.SharePoint.Utils.WinForm.Properties.Resources.Services16x16;
            this.PicBoxTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PicBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.PicBoxTitle.Margin = new System.Windows.Forms.Padding(0);
            this.PicBoxTitle.Name = "PicBoxTitle";
            this.PicBoxTitle.Size = new System.Drawing.Size(30, 25);
            this.PicBoxTitle.TabIndex = 0;
            this.PicBoxTitle.TabStop = false;
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.Location = new System.Drawing.Point(30, 0);
            this.LblHeader.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(959, 25);
            this.LblHeader.TabIndex = 1;
            this.LblHeader.Text = "File Splitter";
            this.LblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // recycleToolStripMenuItem
            // 
            this.recycleToolStripMenuItem.Name = "recycleToolStripMenuItem";
            this.recycleToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.DefaultExt = "log";
            // 
            // FileSplitterUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Splitter);
            this.Controls.Add(this.TblHeader);
            this.Name = "FileSplitterUsrCtrl";
            this.Size = new System.Drawing.Size(992, 606);
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel1.PerformLayout();
            this.Splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.Splitter.ResumeLayout(false);
            this.SplContainerOutput.Panel1.ResumeLayout(false);
            this.SplContainerOutput.Panel1.PerformLayout();
            this.SplContainerOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplContainerOutput)).EndInit();
            this.SplContainerOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumSelLinesPerFile)).EndInit();
            this.TblHeader.ResumeLayout(false);
            this.TblHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer Splitter;
        private System.Windows.Forms.TableLayoutPanel TblHeader;
        private System.Windows.Forms.PictureBox PicBoxTitle;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.ToolStripMenuItem recycleToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.Windows.Forms.TextBox TxtBoxFileLoc;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.RichTextBox RchTxtOutput;
        private System.Windows.Forms.SplitContainer SplContainerOutput;
        private System.Windows.Forms.Label LblSplitFileCount;
        private System.Windows.Forms.NumericUpDown NumSelLinesPerFile;
        private System.Windows.Forms.Button BtnSplitFile;
    }
}
