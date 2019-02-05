namespace Spc.SharePoint.Utils.WinForm.Panels
{
    partial class WinSvcsUsrCtrl
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
            this.components = new System.ComponentModel.Container();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.GridProcesses = new Zuby.ADGV.AdvancedDataGridView();
            this.TblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.PicBoxTitle = new System.Windows.Forms.PictureBox();
            this.LblHeader = new System.Windows.Forms.Label();
            this.GridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridProcesses)).BeginInit();
            this.TblHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).BeginInit();
            this.GridContextMenu.SuspendLayout();
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
            this.Splitter.Panel1.Controls.Add(this.BtnRefresh);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.GridProcesses);
            this.Splitter.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Splitter.Size = new System.Drawing.Size(992, 581);
            this.Splitter.SplitterDistance = 28;
            this.Splitter.SplitterWidth = 1;
            this.Splitter.TabIndex = 1;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.ForeColor = System.Drawing.Color.Black;
            this.BtnRefresh.Location = new System.Drawing.Point(3, 3);
            this.BtnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(104, 23);
            this.BtnRefresh.TabIndex = 0;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // GridProcesses
            // 
            this.GridProcesses.AllowUserToAddRows = false;
            this.GridProcesses.AllowUserToDeleteRows = false;
            this.GridProcesses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridProcesses.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridProcesses.Location = new System.Drawing.Point(3, 3);
            this.GridProcesses.Name = "GridProcesses";
            this.GridProcesses.ReadOnly = true;
            this.GridProcesses.Size = new System.Drawing.Size(986, 546);
            this.GridProcesses.TabIndex = 1;
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
            this.TblHeader.TabIndex = 4;
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
            this.LblHeader.Text = "Windows Services";
            this.LblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridContextMenu
            // 
            this.GridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopToolStripMenuItem,
            this.startToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.GridContextMenu.Name = "GridContextMenu";
            this.GridContextMenu.Size = new System.Drawing.Size(111, 70);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.Stop_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.Start_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.Restart_Click);
            // 
            // WinSvcsUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Splitter);
            this.Controls.Add(this.TblHeader);
            this.Name = "WinSvcsUsrCtrl";
            this.Size = new System.Drawing.Size(992, 606);
            this.Load += new System.EventHandler(this.IISMgrUsrCtrl_Load);
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.Splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridProcesses)).EndInit();
            this.TblHeader.ResumeLayout(false);
            this.TblHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).EndInit();
            this.GridContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer Splitter;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.TableLayoutPanel TblHeader;
        private System.Windows.Forms.PictureBox PicBoxTitle;
        private System.Windows.Forms.Label LblHeader;
        private Zuby.ADGV.AdvancedDataGridView GridProcesses;
        private System.Windows.Forms.ContextMenuStrip GridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
    }
}
