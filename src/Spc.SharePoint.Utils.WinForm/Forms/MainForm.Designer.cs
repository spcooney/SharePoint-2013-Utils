namespace Spc.SharePoint.Utils.WinForm.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SpltCnt = new System.Windows.Forms.SplitContainer();
            this.TreeNav = new System.Windows.Forms.TreeView();
            this.ImgList = new System.Windows.Forms.ImageList(this.components);
            this.PnlMain = new System.Windows.Forms.Panel();
            this.StsBar = new System.Windows.Forms.StatusStrip();
            this.ToolStripProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.MenuStr = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSizeMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize8pxMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize10pxMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize12pxMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize14pxMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize16pxMenuStr = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SpltCnt)).BeginInit();
            this.SpltCnt.Panel1.SuspendLayout();
            this.SpltCnt.Panel2.SuspendLayout();
            this.SpltCnt.SuspendLayout();
            this.StsBar.SuspendLayout();
            this.MenuStr.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpltCnt
            // 
            this.SpltCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpltCnt.Location = new System.Drawing.Point(0, 24);
            this.SpltCnt.Name = "SpltCnt";
            // 
            // SpltCnt.Panel1
            // 
            this.SpltCnt.Panel1.Controls.Add(this.TreeNav);
            // 
            // SpltCnt.Panel2
            // 
            this.SpltCnt.Panel2.Controls.Add(this.PnlMain);
            this.SpltCnt.Size = new System.Drawing.Size(1146, 607);
            this.SpltCnt.SplitterDistance = 193;
            this.SpltCnt.SplitterWidth = 6;
            this.SpltCnt.TabIndex = 0;
            this.SpltCnt.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SpltCnt_SplitterMoved);
            this.SpltCnt.DoubleClick += new System.EventHandler(this.SpltCnt_DoubleClick);
            // 
            // TreeNav
            // 
            this.TreeNav.BackColor = System.Drawing.Color.White;
            this.TreeNav.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TreeNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeNav.ImageIndex = 0;
            this.TreeNav.ImageList = this.ImgList;
            this.TreeNav.Location = new System.Drawing.Point(0, 0);
            this.TreeNav.Name = "TreeNav";
            this.TreeNav.SelectedImageIndex = 0;
            this.TreeNav.ShowLines = false;
            this.TreeNav.ShowPlusMinus = false;
            this.TreeNav.ShowRootLines = false;
            this.TreeNav.Size = new System.Drawing.Size(193, 607);
            this.TreeNav.TabIndex = 0;
            this.TreeNav.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeNav_AfterExpand);
            // 
            // ImgList
            // 
            this.ImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgList.ImageStream")));
            this.ImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgList.Images.SetKeyName(0, "Transparent.png");
            this.ImgList.Images.SetKeyName(1, "Script16x16.png");
            this.ImgList.Images.SetKeyName(2, "Photoshop16x16.png");
            this.ImgList.Images.SetKeyName(3, "Clipboard16x16.png");
            this.ImgList.Images.SetKeyName(4, "SharePoint16x16.png");
            this.ImgList.Images.SetKeyName(5, "Globe16x16.png");
            this.ImgList.Images.SetKeyName(6, "Web16x16.png");
            this.ImgList.Images.SetKeyName(7, "Repair.ico");
            this.ImgList.Images.SetKeyName(8, "BlueBullet16x16.png");
            this.ImgList.Images.SetKeyName(9, "Microsoft16x16.png");
            this.ImgList.Images.SetKeyName(10, "Services16x16.png");
            this.ImgList.Images.SetKeyName(11, "DarkBlueBullet16x16.png");
            // 
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(947, 607);
            this.PnlMain.TabIndex = 0;
            // 
            // StsBar
            // 
            this.StsBar.BackColor = System.Drawing.Color.White;
            this.StsBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripProgress});
            this.StsBar.Location = new System.Drawing.Point(0, 631);
            this.StsBar.Name = "StsBar";
            this.StsBar.Size = new System.Drawing.Size(1146, 22);
            this.StsBar.TabIndex = 1;
            this.StsBar.Text = "statusStrip1";
            // 
            // ToolStripProgress
            // 
            this.ToolStripProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.ToolStripProgress.Name = "ToolStripProgress";
            this.ToolStripProgress.Size = new System.Drawing.Size(100, 16);
            this.ToolStripProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // MenuStr
            // 
            this.MenuStr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.MenuStr.Location = new System.Drawing.Point(0, 0);
            this.MenuStr.Name = "MenuStr";
            this.MenuStr.Size = new System.Drawing.Size(1146, 24);
            this.MenuStr.TabIndex = 0;
            this.MenuStr.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontSizeMenuStr});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // FontSizeMenuStr
            // 
            this.FontSizeMenuStr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontSize8pxMenuStr,
            this.FontSize10pxMenuStr,
            this.FontSize12pxMenuStr,
            this.FontSize14pxMenuStr,
            this.FontSize16pxMenuStr});
            this.FontSizeMenuStr.Name = "FontSizeMenuStr";
            this.FontSizeMenuStr.Size = new System.Drawing.Size(121, 22);
            this.FontSizeMenuStr.Text = "Font Size";
            // 
            // FontSize8pxMenuStr
            // 
            this.FontSize8pxMenuStr.Name = "FontSize8pxMenuStr";
            this.FontSize8pxMenuStr.Size = new System.Drawing.Size(101, 22);
            this.FontSize8pxMenuStr.Text = "8 px";
            this.FontSize8pxMenuStr.Click += new System.EventHandler(this.FontSize8pxMenuStr_Click);
            // 
            // FontSize10pxMenuStr
            // 
            this.FontSize10pxMenuStr.Name = "FontSize10pxMenuStr";
            this.FontSize10pxMenuStr.Size = new System.Drawing.Size(101, 22);
            this.FontSize10pxMenuStr.Text = "10 px";
            this.FontSize10pxMenuStr.Click += new System.EventHandler(this.FontSize10pxMenuStr_Click);
            // 
            // FontSize12pxMenuStr
            // 
            this.FontSize12pxMenuStr.Name = "FontSize12pxMenuStr";
            this.FontSize12pxMenuStr.Size = new System.Drawing.Size(101, 22);
            this.FontSize12pxMenuStr.Text = "12 px";
            this.FontSize12pxMenuStr.Click += new System.EventHandler(this.FontSize12pxMenuStr_Click);
            // 
            // FontSize14pxMenuStr
            // 
            this.FontSize14pxMenuStr.Name = "FontSize14pxMenuStr";
            this.FontSize14pxMenuStr.Size = new System.Drawing.Size(101, 22);
            this.FontSize14pxMenuStr.Text = "14 px";
            this.FontSize14pxMenuStr.Click += new System.EventHandler(this.FontSize14pxMenuStr_Click);
            // 
            // FontSize16pxMenuStr
            // 
            this.FontSize16pxMenuStr.Name = "FontSize16pxMenuStr";
            this.FontSize16pxMenuStr.Size = new System.Drawing.Size(101, 22);
            this.FontSize16pxMenuStr.Text = "16 px";
            this.FontSize16pxMenuStr.Click += new System.EventHandler(this.FontSize16pxMenuStr_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1146, 653);
            this.Controls.Add(this.SpltCnt);
            this.Controls.Add(this.StsBar);
            this.Controls.Add(this.MenuStr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStr;
            this.Name = "MainForm";
            this.Text = "SharePoint 2013 Util";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SpltCnt.Panel1.ResumeLayout(false);
            this.SpltCnt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpltCnt)).EndInit();
            this.SpltCnt.ResumeLayout(false);
            this.StsBar.ResumeLayout(false);
            this.StsBar.PerformLayout();
            this.MenuStr.ResumeLayout(false);
            this.MenuStr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer SpltCnt;
        private System.Windows.Forms.TreeView TreeNav;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.StatusStrip StsBar;
        private System.Windows.Forms.MenuStrip MenuStr;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FontSizeMenuStr;
        private System.Windows.Forms.ToolStripMenuItem FontSize8pxMenuStr;
        private System.Windows.Forms.ToolStripMenuItem FontSize10pxMenuStr;
        private System.Windows.Forms.ToolStripMenuItem FontSize12pxMenuStr;
        private System.Windows.Forms.ToolStripMenuItem FontSize14pxMenuStr;
        private System.Windows.Forms.ToolStripMenuItem FontSize16pxMenuStr;
        private System.Windows.Forms.ImageList ImgList;
        private System.Windows.Forms.ToolStripProgressBar ToolStripProgress;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}