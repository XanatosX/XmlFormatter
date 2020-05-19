namespace XmlFormatter.src.Windows
{
    partial class HotfolderEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotfolderEditor));
            this.GB_General = new System.Windows.Forms.GroupBox();
            this.CB_Mode = new System.Windows.Forms.ComboBox();
            this.L_Mode = new System.Windows.Forms.Label();
            this.L_Provider = new System.Windows.Forms.Label();
            this.CB_Formatter = new System.Windows.Forms.ComboBox();
            this.GB_Folders = new System.Windows.Forms.GroupBox();
            this.TB_OutputFileScheme = new System.Windows.Forms.TextBox();
            this.L_OutputScheme = new System.Windows.Forms.Label();
            this.L_Filter = new System.Windows.Forms.Label();
            this.TB_Filter = new System.Windows.Forms.TextBox();
            this.B_OutputFolder = new System.Windows.Forms.Button();
            this.TB_OutputFolder = new System.Windows.Forms.TextBox();
            this.L_OutputFolder = new System.Windows.Forms.Label();
            this.B_OpenWatchFolder = new System.Windows.Forms.Button();
            this.TB_WatchedFolder = new System.Windows.Forms.TextBox();
            this.L_WatchedFolder = new System.Windows.Forms.Label();
            this.GB_Triggers = new System.Windows.Forms.GroupBox();
            this.CB_RemoveOld = new System.Windows.Forms.CheckBox();
            this.CB_OnRename = new System.Windows.Forms.CheckBox();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.GB_General.SuspendLayout();
            this.GB_Folders.SuspendLayout();
            this.GB_Triggers.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_General
            // 
            this.GB_General.Controls.Add(this.CB_Mode);
            this.GB_General.Controls.Add(this.L_Mode);
            this.GB_General.Controls.Add(this.L_Provider);
            this.GB_General.Controls.Add(this.CB_Formatter);
            this.GB_General.Location = new System.Drawing.Point(18, 18);
            this.GB_General.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_General.Name = "GB_General";
            this.GB_General.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_General.Size = new System.Drawing.Size(519, 177);
            this.GB_General.TabIndex = 0;
            this.GB_General.TabStop = false;
            this.GB_General.Text = "General";
            // 
            // CB_Mode
            // 
            this.CB_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Mode.FormattingEnabled = true;
            this.CB_Mode.Location = new System.Drawing.Point(9, 115);
            this.CB_Mode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_Mode.Name = "CB_Mode";
            this.CB_Mode.Size = new System.Drawing.Size(499, 28);
            this.CB_Mode.TabIndex = 3;
            // 
            // L_Mode
            // 
            this.L_Mode.AutoSize = true;
            this.L_Mode.Location = new System.Drawing.Point(4, 91);
            this.L_Mode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Mode.Name = "L_Mode";
            this.L_Mode.Size = new System.Drawing.Size(49, 20);
            this.L_Mode.TabIndex = 2;
            this.L_Mode.Text = "Mode";
            // 
            // L_Provider
            // 
            this.L_Provider.AutoSize = true;
            this.L_Provider.Location = new System.Drawing.Point(4, 25);
            this.L_Provider.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Provider.Name = "L_Provider";
            this.L_Provider.Size = new System.Drawing.Size(66, 20);
            this.L_Provider.TabIndex = 1;
            this.L_Provider.Text = "Provider";
            // 
            // CB_Formatter
            // 
            this.CB_Formatter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Formatter.FormattingEnabled = true;
            this.CB_Formatter.Location = new System.Drawing.Point(9, 54);
            this.CB_Formatter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_Formatter.Name = "CB_Formatter";
            this.CB_Formatter.Size = new System.Drawing.Size(499, 28);
            this.CB_Formatter.TabIndex = 0;
            // 
            // GB_Folders
            // 
            this.GB_Folders.Controls.Add(this.TB_OutputFileScheme);
            this.GB_Folders.Controls.Add(this.L_OutputScheme);
            this.GB_Folders.Controls.Add(this.L_Filter);
            this.GB_Folders.Controls.Add(this.TB_Filter);
            this.GB_Folders.Controls.Add(this.B_OutputFolder);
            this.GB_Folders.Controls.Add(this.TB_OutputFolder);
            this.GB_Folders.Controls.Add(this.L_OutputFolder);
            this.GB_Folders.Controls.Add(this.B_OpenWatchFolder);
            this.GB_Folders.Controls.Add(this.TB_WatchedFolder);
            this.GB_Folders.Controls.Add(this.L_WatchedFolder);
            this.GB_Folders.Location = new System.Drawing.Point(18, 205);
            this.GB_Folders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Folders.Name = "GB_Folders";
            this.GB_Folders.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Folders.Size = new System.Drawing.Size(519, 278);
            this.GB_Folders.TabIndex = 1;
            this.GB_Folders.TabStop = false;
            this.GB_Folders.Text = "Folders";
            // 
            // TB_OutputFileScheme
            // 
            this.TB_OutputFileScheme.Location = new System.Drawing.Point(9, 229);
            this.TB_OutputFileScheme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TB_OutputFileScheme.Name = "TB_OutputFileScheme";
            this.TB_OutputFileScheme.Size = new System.Drawing.Size(454, 26);
            this.TB_OutputFileScheme.TabIndex = 11;
            // 
            // L_OutputScheme
            // 
            this.L_OutputScheme.AutoSize = true;
            this.L_OutputScheme.Location = new System.Drawing.Point(4, 205);
            this.L_OutputScheme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_OutputScheme.Name = "L_OutputScheme";
            this.L_OutputScheme.Size = new System.Drawing.Size(142, 20);
            this.L_OutputScheme.TabIndex = 10;
            this.L_OutputScheme.Text = "Output file scheme";
            // 
            // L_Filter
            // 
            this.L_Filter.AutoSize = true;
            this.L_Filter.Location = new System.Drawing.Point(4, 85);
            this.L_Filter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Filter.Name = "L_Filter";
            this.L_Filter.Size = new System.Drawing.Size(44, 20);
            this.L_Filter.TabIndex = 9;
            this.L_Filter.Text = "Filter";
            // 
            // TB_Filter
            // 
            this.TB_Filter.Location = new System.Drawing.Point(9, 109);
            this.TB_Filter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TB_Filter.Name = "TB_Filter";
            this.TB_Filter.Size = new System.Drawing.Size(454, 26);
            this.TB_Filter.TabIndex = 8;
            // 
            // B_OutputFolder
            // 
            this.B_OutputFolder.Location = new System.Drawing.Point(474, 169);
            this.B_OutputFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_OutputFolder.Name = "B_OutputFolder";
            this.B_OutputFolder.Size = new System.Drawing.Size(36, 31);
            this.B_OutputFolder.TabIndex = 5;
            this.B_OutputFolder.Text = "...";
            this.B_OutputFolder.UseVisualStyleBackColor = true;
            this.B_OutputFolder.Click += new System.EventHandler(this.B_OutputFolder_Click);
            // 
            // TB_OutputFolder
            // 
            this.TB_OutputFolder.Location = new System.Drawing.Point(9, 169);
            this.TB_OutputFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TB_OutputFolder.Name = "TB_OutputFolder";
            this.TB_OutputFolder.Size = new System.Drawing.Size(454, 26);
            this.TB_OutputFolder.TabIndex = 4;
            // 
            // L_OutputFolder
            // 
            this.L_OutputFolder.AutoSize = true;
            this.L_OutputFolder.Location = new System.Drawing.Point(4, 145);
            this.L_OutputFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_OutputFolder.Name = "L_OutputFolder";
            this.L_OutputFolder.Size = new System.Drawing.Size(102, 20);
            this.L_OutputFolder.TabIndex = 3;
            this.L_OutputFolder.Text = "Output folder";
            // 
            // B_OpenWatchFolder
            // 
            this.B_OpenWatchFolder.Location = new System.Drawing.Point(474, 49);
            this.B_OpenWatchFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_OpenWatchFolder.Name = "B_OpenWatchFolder";
            this.B_OpenWatchFolder.Size = new System.Drawing.Size(36, 31);
            this.B_OpenWatchFolder.TabIndex = 2;
            this.B_OpenWatchFolder.Text = "...";
            this.B_OpenWatchFolder.UseVisualStyleBackColor = true;
            this.B_OpenWatchFolder.Click += new System.EventHandler(this.B_OpenWatchFolder_Click);
            // 
            // TB_WatchedFolder
            // 
            this.TB_WatchedFolder.Location = new System.Drawing.Point(9, 49);
            this.TB_WatchedFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TB_WatchedFolder.Name = "TB_WatchedFolder";
            this.TB_WatchedFolder.Size = new System.Drawing.Size(454, 26);
            this.TB_WatchedFolder.TabIndex = 1;
            // 
            // L_WatchedFolder
            // 
            this.L_WatchedFolder.AutoSize = true;
            this.L_WatchedFolder.Location = new System.Drawing.Point(4, 25);
            this.L_WatchedFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_WatchedFolder.Name = "L_WatchedFolder";
            this.L_WatchedFolder.Size = new System.Drawing.Size(118, 20);
            this.L_WatchedFolder.TabIndex = 0;
            this.L_WatchedFolder.Text = "Folder to watch";
            // 
            // GB_Triggers
            // 
            this.GB_Triggers.Controls.Add(this.CB_RemoveOld);
            this.GB_Triggers.Controls.Add(this.CB_OnRename);
            this.GB_Triggers.Location = new System.Drawing.Point(18, 492);
            this.GB_Triggers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Triggers.Name = "GB_Triggers";
            this.GB_Triggers.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Triggers.Size = new System.Drawing.Size(519, 71);
            this.GB_Triggers.TabIndex = 2;
            this.GB_Triggers.TabStop = false;
            this.GB_Triggers.Text = "Trigger";
            // 
            // CB_RemoveOld
            // 
            this.CB_RemoveOld.AutoSize = true;
            this.CB_RemoveOld.Location = new System.Drawing.Point(348, 29);
            this.CB_RemoveOld.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_RemoveOld.Name = "CB_RemoveOld";
            this.CB_RemoveOld.Size = new System.Drawing.Size(157, 24);
            this.CB_RemoveOld.TabIndex = 1;
            this.CB_RemoveOld.Text = "Remove input file";
            this.CB_RemoveOld.UseVisualStyleBackColor = true;
            // 
            // CB_OnRename
            // 
            this.CB_OnRename.AutoSize = true;
            this.CB_OnRename.Location = new System.Drawing.Point(9, 29);
            this.CB_OnRename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_OnRename.Name = "CB_OnRename";
            this.CB_OnRename.Size = new System.Drawing.Size(105, 24);
            this.CB_OnRename.TabIndex = 0;
            this.CB_OnRename.Text = "Renamed";
            this.CB_OnRename.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(18, 572);
            this.B_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(112, 35);
            this.B_Save.TabIndex = 3;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(424, 572);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(112, 35);
            this.B_Cancel.TabIndex = 4;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // HotfolderEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(555, 618);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.GB_Triggers);
            this.Controls.Add(this.GB_Folders);
            this.Controls.Add(this.GB_General);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HotfolderEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hotfolder Editor";
            this.Load += new System.EventHandler(this.HotfolderEditor_Load);
            this.GB_General.ResumeLayout(false);
            this.GB_General.PerformLayout();
            this.GB_Folders.ResumeLayout(false);
            this.GB_Folders.PerformLayout();
            this.GB_Triggers.ResumeLayout(false);
            this.GB_Triggers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_General;
        private System.Windows.Forms.ComboBox CB_Formatter;
        private System.Windows.Forms.Label L_Provider;
        private System.Windows.Forms.ComboBox CB_Mode;
        private System.Windows.Forms.Label L_Mode;
        private System.Windows.Forms.GroupBox GB_Folders;
        private System.Windows.Forms.Button B_OpenWatchFolder;
        private System.Windows.Forms.TextBox TB_WatchedFolder;
        private System.Windows.Forms.Label L_WatchedFolder;
        private System.Windows.Forms.Button B_OutputFolder;
        private System.Windows.Forms.TextBox TB_OutputFolder;
        private System.Windows.Forms.Label L_OutputFolder;
        private System.Windows.Forms.TextBox TB_OutputFileScheme;
        private System.Windows.Forms.Label L_OutputScheme;
        private System.Windows.Forms.Label L_Filter;
        private System.Windows.Forms.TextBox TB_Filter;
        private System.Windows.Forms.GroupBox GB_Triggers;
        private System.Windows.Forms.CheckBox CB_RemoveOld;
        private System.Windows.Forms.CheckBox CB_OnRename;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
    }
}