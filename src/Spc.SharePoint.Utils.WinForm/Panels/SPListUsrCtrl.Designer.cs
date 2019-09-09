﻿namespace Spc.SharePoint.Utils.WinForm.Panels
{
    partial class SPListUsrCtrl
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
            this.SplitterTextBox = new System.Windows.Forms.SplitContainer();
            this.TxtSPListUrl = new System.Windows.Forms.TextBox();
            this.SplitterControls = new System.Windows.Forms.SplitContainer();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.SplitterGrids = new System.Windows.Forms.SplitContainer();
            this.GrpColFilter = new System.Windows.Forms.GroupBox();
            this.ChkBoxListFilters = new System.Windows.Forms.CheckedListBox();
            this.CmsShowHide = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmiShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiShowNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ListTabs = new System.Windows.Forms.TabControl();
            this.TabSchema = new System.Windows.Forms.TabPage();
            this.RchTxtSchema = new System.Windows.Forms.RichTextBox();
            this.TabData = new System.Windows.Forms.TabPage();
            this.GridData = new Zuby.ADGV.AdvancedDataGridView();
            this.TabProperties = new System.Windows.Forms.TabPage();
            this.SplitContProperties = new System.Windows.Forms.SplitContainer();
            this.BtnQueryProp = new System.Windows.Forms.Button();
            this.DdListProperties = new System.Windows.Forms.ComboBox();
            this.RchTxtProperties = new System.Windows.Forms.RichTextBox();
            this.TblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.PicBoxTitle = new System.Windows.Forms.PictureBox();
            this.LblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitterTextBox)).BeginInit();
            this.SplitterTextBox.Panel1.SuspendLayout();
            this.SplitterTextBox.Panel2.SuspendLayout();
            this.SplitterTextBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitterControls)).BeginInit();
            this.SplitterControls.Panel1.SuspendLayout();
            this.SplitterControls.Panel2.SuspendLayout();
            this.SplitterControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitterGrids)).BeginInit();
            this.SplitterGrids.Panel1.SuspendLayout();
            this.SplitterGrids.Panel2.SuspendLayout();
            this.SplitterGrids.SuspendLayout();
            this.GrpColFilter.SuspendLayout();
            this.CmsShowHide.SuspendLayout();
            this.ListTabs.SuspendLayout();
            this.TabSchema.SuspendLayout();
            this.TabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).BeginInit();
            this.TabProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContProperties)).BeginInit();
            this.SplitContProperties.Panel1.SuspendLayout();
            this.SplitContProperties.Panel2.SuspendLayout();
            this.SplitContProperties.SuspendLayout();
            this.TblHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitterTextBox
            // 
            this.SplitterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitterTextBox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitterTextBox.IsSplitterFixed = true;
            this.SplitterTextBox.Location = new System.Drawing.Point(0, 25);
            this.SplitterTextBox.Name = "SplitterTextBox";
            this.SplitterTextBox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitterTextBox.Panel1
            // 
            this.SplitterTextBox.Panel1.Controls.Add(this.TxtSPListUrl);
            this.SplitterTextBox.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.SplitterTextBox.Panel1MinSize = 24;
            // 
            // SplitterTextBox.Panel2
            // 
            this.SplitterTextBox.Panel2.Controls.Add(this.SplitterControls);
            this.SplitterTextBox.Panel2MinSize = 24;
            this.SplitterTextBox.Size = new System.Drawing.Size(678, 584);
            this.SplitterTextBox.SplitterDistance = 25;
            this.SplitterTextBox.TabIndex = 5;
            // 
            // TxtSPListUrl
            // 
            this.TxtSPListUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtSPListUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSPListUrl.Location = new System.Drawing.Point(3, 0);
            this.TxtSPListUrl.MinimumSize = new System.Drawing.Size(4, 24);
            this.TxtSPListUrl.Name = "TxtSPListUrl";
            this.TxtSPListUrl.Size = new System.Drawing.Size(672, 22);
            this.TxtSPListUrl.TabIndex = 0;
            this.TxtSPListUrl.TextChanged += new System.EventHandler(this.TxtSPListUrl_TextChanged);
            this.TxtSPListUrl.Enter += new System.EventHandler(this.TxtSPListUrl_Enter);
            this.TxtSPListUrl.Leave += new System.EventHandler(this.TxtSPListUrl_Leave);
            // 
            // SplitterControls
            // 
            this.SplitterControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitterControls.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitterControls.IsSplitterFixed = true;
            this.SplitterControls.Location = new System.Drawing.Point(0, 0);
            this.SplitterControls.Name = "SplitterControls";
            this.SplitterControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitterControls.Panel1
            // 
            this.SplitterControls.Panel1.Controls.Add(this.BtnQuery);
            this.SplitterControls.Panel1.Controls.Add(this.BtnRefresh);
            // 
            // SplitterControls.Panel2
            // 
            this.SplitterControls.Panel2.Controls.Add(this.SplitterGrids);
            this.SplitterControls.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.SplitterControls.Size = new System.Drawing.Size(678, 555);
            this.SplitterControls.SplitterDistance = 28;
            this.SplitterControls.SplitterWidth = 1;
            this.SplitterControls.TabIndex = 1;
            this.SplitterControls.SizeChanged += new System.EventHandler(this.SplitterControls_SizeChanged);
            this.SplitterControls.Resize += new System.EventHandler(this.SplitterControls_Resize);
            // 
            // BtnQuery
            // 
            this.BtnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnQuery.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnQuery.ForeColor = System.Drawing.Color.Black;
            this.BtnQuery.Location = new System.Drawing.Point(4, 2);
            this.BtnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(104, 23);
            this.BtnQuery.TabIndex = 2;
            this.BtnQuery.Text = "Query";
            this.BtnQuery.UseVisualStyleBackColor = false;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.ForeColor = System.Drawing.Color.Black;
            this.BtnRefresh.Location = new System.Drawing.Point(113, 2);
            this.BtnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(104, 23);
            this.BtnRefresh.TabIndex = 1;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            // 
            // SplitterGrids
            // 
            this.SplitterGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitterGrids.Location = new System.Drawing.Point(3, 3);
            this.SplitterGrids.Name = "SplitterGrids";
            this.SplitterGrids.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitterGrids.Panel1
            // 
            this.SplitterGrids.Panel1.Controls.Add(this.GrpColFilter);
            this.SplitterGrids.Panel1MinSize = 0;
            // 
            // SplitterGrids.Panel2
            // 
            this.SplitterGrids.Panel2.Controls.Add(this.ListTabs);
            this.SplitterGrids.Size = new System.Drawing.Size(672, 520);
            this.SplitterGrids.SplitterDistance = 79;
            this.SplitterGrids.TabIndex = 3;
            // 
            // GrpColFilter
            // 
            this.GrpColFilter.Controls.Add(this.ChkBoxListFilters);
            this.GrpColFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpColFilter.Location = new System.Drawing.Point(0, 0);
            this.GrpColFilter.Name = "GrpColFilter";
            this.GrpColFilter.Size = new System.Drawing.Size(672, 79);
            this.GrpColFilter.TabIndex = 0;
            this.GrpColFilter.TabStop = false;
            this.GrpColFilter.Text = " Show/Hide Columns ";
            // 
            // ChkBoxListFilters
            // 
            this.ChkBoxListFilters.ContextMenuStrip = this.CmsShowHide;
            this.ChkBoxListFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChkBoxListFilters.FormattingEnabled = true;
            this.ChkBoxListFilters.HorizontalScrollbar = true;
            this.ChkBoxListFilters.Location = new System.Drawing.Point(3, 16);
            this.ChkBoxListFilters.MultiColumn = true;
            this.ChkBoxListFilters.Name = "ChkBoxListFilters";
            this.ChkBoxListFilters.Size = new System.Drawing.Size(666, 60);
            this.ChkBoxListFilters.TabIndex = 0;
            // 
            // CmsShowHide
            // 
            this.CmsShowHide.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiShowAll,
            this.TsmiShowNone});
            this.CmsShowHide.Name = "CmsShowHide";
            this.CmsShowHide.Size = new System.Drawing.Size(136, 48);
            // 
            // TsmiShowAll
            // 
            this.TsmiShowAll.Name = "TsmiShowAll";
            this.TsmiShowAll.Size = new System.Drawing.Size(135, 22);
            this.TsmiShowAll.Text = "Show All";
            this.TsmiShowAll.Click += new System.EventHandler(this.TsmiShowAll_Click);
            // 
            // TsmiShowNone
            // 
            this.TsmiShowNone.Name = "TsmiShowNone";
            this.TsmiShowNone.Size = new System.Drawing.Size(135, 22);
            this.TsmiShowNone.Text = "Show None";
            this.TsmiShowNone.Click += new System.EventHandler(this.TsmiShowNone_Click);
            // 
            // ListTabs
            // 
            this.ListTabs.Controls.Add(this.TabSchema);
            this.ListTabs.Controls.Add(this.TabData);
            this.ListTabs.Controls.Add(this.TabProperties);
            this.ListTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListTabs.Location = new System.Drawing.Point(0, 0);
            this.ListTabs.Name = "ListTabs";
            this.ListTabs.SelectedIndex = 0;
            this.ListTabs.Size = new System.Drawing.Size(672, 437);
            this.ListTabs.TabIndex = 2;
            this.ListTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.ListTabs_Selected);
            // 
            // TabSchema
            // 
            this.TabSchema.Controls.Add(this.RchTxtSchema);
            this.TabSchema.Location = new System.Drawing.Point(4, 22);
            this.TabSchema.Name = "TabSchema";
            this.TabSchema.Padding = new System.Windows.Forms.Padding(3);
            this.TabSchema.Size = new System.Drawing.Size(664, 411);
            this.TabSchema.TabIndex = 0;
            this.TabSchema.Text = "Schema";
            this.TabSchema.UseVisualStyleBackColor = true;
            // 
            // RchTxtSchema
            // 
            this.RchTxtSchema.BackColor = System.Drawing.SystemColors.Control;
            this.RchTxtSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RchTxtSchema.Location = new System.Drawing.Point(3, 3);
            this.RchTxtSchema.Name = "RchTxtSchema";
            this.RchTxtSchema.Size = new System.Drawing.Size(658, 405);
            this.RchTxtSchema.TabIndex = 0;
            this.RchTxtSchema.Text = "";
            // 
            // TabData
            // 
            this.TabData.Controls.Add(this.GridData);
            this.TabData.Location = new System.Drawing.Point(4, 22);
            this.TabData.Name = "TabData";
            this.TabData.Padding = new System.Windows.Forms.Padding(3);
            this.TabData.Size = new System.Drawing.Size(664, 411);
            this.TabData.TabIndex = 1;
            this.TabData.Text = "Data";
            this.TabData.UseVisualStyleBackColor = true;
            // 
            // GridData
            // 
            this.GridData.AllowUserToAddRows = false;
            this.GridData.AllowUserToDeleteRows = false;
            this.GridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridData.FilterAndSortEnabled = true;
            this.GridData.Location = new System.Drawing.Point(3, 3);
            this.GridData.Name = "GridData";
            this.GridData.ReadOnly = true;
            this.GridData.Size = new System.Drawing.Size(658, 405);
            this.GridData.TabIndex = 0;
            // 
            // TabProperties
            // 
            this.TabProperties.Controls.Add(this.SplitContProperties);
            this.TabProperties.Location = new System.Drawing.Point(4, 22);
            this.TabProperties.Name = "TabProperties";
            this.TabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.TabProperties.Size = new System.Drawing.Size(664, 437);
            this.TabProperties.TabIndex = 2;
            this.TabProperties.Text = "Properties";
            this.TabProperties.UseVisualStyleBackColor = true;
            // 
            // SplitContProperties
            // 
            this.SplitContProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContProperties.IsSplitterFixed = true;
            this.SplitContProperties.Location = new System.Drawing.Point(3, 3);
            this.SplitContProperties.Name = "SplitContProperties";
            this.SplitContProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContProperties.Panel1
            // 
            this.SplitContProperties.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.SplitContProperties.Panel1.Controls.Add(this.BtnQueryProp);
            this.SplitContProperties.Panel1.Controls.Add(this.DdListProperties);
            this.SplitContProperties.Panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.SplitContProperties.Panel1MinSize = 18;
            // 
            // SplitContProperties.Panel2
            // 
            this.SplitContProperties.Panel2.Controls.Add(this.RchTxtProperties);
            this.SplitContProperties.Size = new System.Drawing.Size(658, 431);
            this.SplitContProperties.SplitterDistance = 22;
            this.SplitContProperties.SplitterWidth = 1;
            this.SplitContProperties.TabIndex = 0;
            this.SplitContProperties.SizeChanged += new System.EventHandler(this.SplitContProperties_SizeChanged);
            this.SplitContProperties.Layout += new System.Windows.Forms.LayoutEventHandler(this.SplitContProperties_Layout);
            // 
            // BtnQueryProp
            // 
            this.BtnQueryProp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(251)))));
            this.BtnQueryProp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnQueryProp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(153)))), ((int)(((byte)(210)))));
            this.BtnQueryProp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnQueryProp.ForeColor = System.Drawing.Color.Black;
            this.BtnQueryProp.Location = new System.Drawing.Point(384, 1);
            this.BtnQueryProp.Margin = new System.Windows.Forms.Padding(0);
            this.BtnQueryProp.Name = "BtnQueryProp";
            this.BtnQueryProp.Size = new System.Drawing.Size(104, 23);
            this.BtnQueryProp.TabIndex = 3;
            this.BtnQueryProp.Text = "Query";
            this.BtnQueryProp.UseVisualStyleBackColor = false;
            this.BtnQueryProp.Click += new System.EventHandler(this.BtnQueryProp_Click);
            // 
            // DdListProperties
            // 
            this.DdListProperties.FormattingEnabled = true;
            this.DdListProperties.Location = new System.Drawing.Point(0, 2);
            this.DdListProperties.Name = "DdListProperties";
            this.DdListProperties.Size = new System.Drawing.Size(378, 21);
            this.DdListProperties.TabIndex = 0;
            // 
            // RchTxtProperties
            // 
            this.RchTxtProperties.BackColor = System.Drawing.SystemColors.Control;
            this.RchTxtProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RchTxtProperties.Location = new System.Drawing.Point(0, 0);
            this.RchTxtProperties.Name = "RchTxtProperties";
            this.RchTxtProperties.Size = new System.Drawing.Size(658, 408);
            this.RchTxtProperties.TabIndex = 0;
            this.RchTxtProperties.Text = "";
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
            this.TblHeader.Size = new System.Drawing.Size(678, 25);
            this.TblHeader.TabIndex = 4;
            // 
            // PicBoxTitle
            // 
            this.PicBoxTitle.BackgroundImage = global::Spc.SharePoint.Utils.WinForm.Properties.Resources.SharePoint16x16;
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
            this.LblHeader.Size = new System.Drawing.Size(645, 25);
            this.LblHeader.TabIndex = 1;
            this.LblHeader.Text = "SharePoint List";
            this.LblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SPListUsrCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SplitterTextBox);
            this.Controls.Add(this.TblHeader);
            this.Name = "SPListUsrCtrl";
            this.Size = new System.Drawing.Size(678, 609);
            this.SplitterTextBox.Panel1.ResumeLayout(false);
            this.SplitterTextBox.Panel1.PerformLayout();
            this.SplitterTextBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitterTextBox)).EndInit();
            this.SplitterTextBox.ResumeLayout(false);
            this.SplitterControls.Panel1.ResumeLayout(false);
            this.SplitterControls.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitterControls)).EndInit();
            this.SplitterControls.ResumeLayout(false);
            this.SplitterGrids.Panel1.ResumeLayout(false);
            this.SplitterGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitterGrids)).EndInit();
            this.SplitterGrids.ResumeLayout(false);
            this.GrpColFilter.ResumeLayout(false);
            this.CmsShowHide.ResumeLayout(false);
            this.ListTabs.ResumeLayout(false);
            this.TabSchema.ResumeLayout(false);
            this.TabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).EndInit();
            this.TabProperties.ResumeLayout(false);
            this.SplitContProperties.Panel1.ResumeLayout(false);
            this.SplitContProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContProperties)).EndInit();
            this.SplitContProperties.ResumeLayout(false);
            this.TblHeader.ResumeLayout(false);
            this.TblHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SplitterControls;
        private System.Windows.Forms.TableLayoutPanel TblHeader;
        private System.Windows.Forms.PictureBox PicBoxTitle;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.SplitContainer SplitterTextBox;
        private System.Windows.Forms.TextBox TxtSPListUrl;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.TabControl ListTabs;
        private System.Windows.Forms.TabPage TabSchema;
        private System.Windows.Forms.TabPage TabData;
        private System.Windows.Forms.RichTextBox RchTxtSchema;
        private Zuby.ADGV.AdvancedDataGridView GridData;
        private System.Windows.Forms.TabPage TabProperties;
        private System.Windows.Forms.SplitContainer SplitContProperties;
        private System.Windows.Forms.ComboBox DdListProperties;
        private System.Windows.Forms.RichTextBox RchTxtProperties;
        private System.Windows.Forms.Button BtnQueryProp;
        private System.Windows.Forms.SplitContainer SplitterGrids;
        private System.Windows.Forms.GroupBox GrpColFilter;
        private System.Windows.Forms.CheckedListBox ChkBoxListFilters;
        private System.Windows.Forms.ContextMenuStrip CmsShowHide;
        private System.Windows.Forms.ToolStripMenuItem TsmiShowAll;
        private System.Windows.Forms.ToolStripMenuItem TsmiShowNone;
    }
}
