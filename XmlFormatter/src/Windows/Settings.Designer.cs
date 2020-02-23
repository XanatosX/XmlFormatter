namespace XmlFormatter.src.Windows
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_CheckUpdatesOnStartup = new System.Windows.Forms.CheckBox();
            this.CB_AskBeforeClose = new System.Windows.Forms.CheckBox();
            this.CB_MinimizeToTray = new System.Windows.Forms.CheckBox();
            this.B_SaveAndClose = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.MI_SettingsMenu = new System.Windows.Forms.MenuStrip();
            this.exportSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TC_SettingTabs = new System.Windows.Forms.TabControl();
            this.TP_Application = new System.Windows.Forms.TabPage();
            this.TP_Hotfolder = new System.Windows.Forms.TabPage();
            this.GB_Hotfolder = new System.Windows.Forms.GroupBox();
            this.B_RemoveHotfolder = new System.Windows.Forms.Button();
            this.B_EditHotfolder = new System.Windows.Forms.Button();
            this.B_AddHotfolder = new System.Windows.Forms.Button();
            this.LV_Hotfolders = new System.Windows.Forms.ListView();
            this.CH_watchedFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_filter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_Formatter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_Mode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OutputFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OutputFileScheme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OnRename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_RemoveOld = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CB_Hotfolder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.MI_SettingsMenu.SuspendLayout();
            this.TC_SettingTabs.SuspendLayout();
            this.TP_Application.SuspendLayout();
            this.TP_Hotfolder.SuspendLayout();
            this.GB_Hotfolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_CheckUpdatesOnStartup);
            this.groupBox1.Controls.Add(this.CB_AskBeforeClose);
            this.groupBox1.Controls.Add(this.CB_MinimizeToTray);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application settings";
            // 
            // CB_CheckUpdatesOnStartup
            // 
            this.CB_CheckUpdatesOnStartup.AutoSize = true;
            this.CB_CheckUpdatesOnStartup.Location = new System.Drawing.Point(6, 65);
            this.CB_CheckUpdatesOnStartup.Name = "CB_CheckUpdatesOnStartup";
            this.CB_CheckUpdatesOnStartup.Size = new System.Drawing.Size(163, 17);
            this.CB_CheckUpdatesOnStartup.TabIndex = 2;
            this.CB_CheckUpdatesOnStartup.Text = "Check for updates on startup";
            this.CB_CheckUpdatesOnStartup.UseVisualStyleBackColor = true;
            // 
            // CB_AskBeforeClose
            // 
            this.CB_AskBeforeClose.AutoSize = true;
            this.CB_AskBeforeClose.Location = new System.Drawing.Point(6, 42);
            this.CB_AskBeforeClose.Name = "CB_AskBeforeClose";
            this.CB_AskBeforeClose.Size = new System.Drawing.Size(113, 17);
            this.CB_AskBeforeClose.TabIndex = 1;
            this.CB_AskBeforeClose.Text = "Ask before closing";
            this.CB_AskBeforeClose.UseVisualStyleBackColor = true;
            // 
            // CB_MinimizeToTray
            // 
            this.CB_MinimizeToTray.AutoSize = true;
            this.CB_MinimizeToTray.Location = new System.Drawing.Point(6, 19);
            this.CB_MinimizeToTray.Name = "CB_MinimizeToTray";
            this.CB_MinimizeToTray.Size = new System.Drawing.Size(98, 17);
            this.CB_MinimizeToTray.TabIndex = 0;
            this.CB_MinimizeToTray.Text = "Minimize to tray";
            this.CB_MinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // B_SaveAndClose
            // 
            this.B_SaveAndClose.Location = new System.Drawing.Point(12, 274);
            this.B_SaveAndClose.Name = "B_SaveAndClose";
            this.B_SaveAndClose.Size = new System.Drawing.Size(104, 23);
            this.B_SaveAndClose.TabIndex = 1;
            this.B_SaveAndClose.Text = "Save and close";
            this.B_SaveAndClose.UseVisualStyleBackColor = true;
            this.B_SaveAndClose.Click += new System.EventHandler(this.B_SaveAndClose_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(637, 274);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 2;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // MI_SettingsMenu
            // 
            this.MI_SettingsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSettingsToolStripMenuItem,
            this.importSettingsToolStripMenuItem});
            this.MI_SettingsMenu.Location = new System.Drawing.Point(0, 0);
            this.MI_SettingsMenu.Name = "MI_SettingsMenu";
            this.MI_SettingsMenu.Size = new System.Drawing.Size(734, 24);
            this.MI_SettingsMenu.TabIndex = 3;
            this.MI_SettingsMenu.Text = "menuStrip1";
            // 
            // exportSettingsToolStripMenuItem
            // 
            this.exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            this.exportSettingsToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.exportSettingsToolStripMenuItem.Text = "Export settings";
            this.exportSettingsToolStripMenuItem.Click += new System.EventHandler(this.exportSettingsToolStripMenuItem_Click);
            // 
            // importSettingsToolStripMenuItem
            // 
            this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
            this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.importSettingsToolStripMenuItem.Text = "Import settings";
            this.importSettingsToolStripMenuItem.Click += new System.EventHandler(this.importSettingsToolStripMenuItem_Click);
            // 
            // TC_SettingTabs
            // 
            this.TC_SettingTabs.Controls.Add(this.TP_Application);
            this.TC_SettingTabs.Controls.Add(this.TP_Hotfolder);
            this.TC_SettingTabs.Location = new System.Drawing.Point(0, 27);
            this.TC_SettingTabs.Name = "TC_SettingTabs";
            this.TC_SettingTabs.SelectedIndex = 0;
            this.TC_SettingTabs.Size = new System.Drawing.Size(722, 245);
            this.TC_SettingTabs.TabIndex = 3;
            // 
            // TP_Application
            // 
            this.TP_Application.Controls.Add(this.groupBox1);
            this.TP_Application.Location = new System.Drawing.Point(4, 22);
            this.TP_Application.Name = "TP_Application";
            this.TP_Application.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Application.Size = new System.Drawing.Size(714, 219);
            this.TP_Application.TabIndex = 0;
            this.TP_Application.Text = "Application";
            this.TP_Application.UseVisualStyleBackColor = true;
            // 
            // TP_Hotfolder
            // 
            this.TP_Hotfolder.Controls.Add(this.GB_Hotfolder);
            this.TP_Hotfolder.Controls.Add(this.CB_Hotfolder);
            this.TP_Hotfolder.Location = new System.Drawing.Point(4, 22);
            this.TP_Hotfolder.Name = "TP_Hotfolder";
            this.TP_Hotfolder.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Hotfolder.Size = new System.Drawing.Size(714, 219);
            this.TP_Hotfolder.TabIndex = 1;
            this.TP_Hotfolder.Text = "Hotfolder";
            this.TP_Hotfolder.UseVisualStyleBackColor = true;
            // 
            // GB_Hotfolder
            // 
            this.GB_Hotfolder.Controls.Add(this.B_RemoveHotfolder);
            this.GB_Hotfolder.Controls.Add(this.B_EditHotfolder);
            this.GB_Hotfolder.Controls.Add(this.B_AddHotfolder);
            this.GB_Hotfolder.Controls.Add(this.LV_Hotfolders);
            this.GB_Hotfolder.Location = new System.Drawing.Point(8, 29);
            this.GB_Hotfolder.Name = "GB_Hotfolder";
            this.GB_Hotfolder.Size = new System.Drawing.Size(700, 184);
            this.GB_Hotfolder.TabIndex = 1;
            this.GB_Hotfolder.TabStop = false;
            this.GB_Hotfolder.Text = "Hotfolder";
            // 
            // B_RemoveHotfolder
            // 
            this.B_RemoveHotfolder.Location = new System.Drawing.Point(619, 156);
            this.B_RemoveHotfolder.Name = "B_RemoveHotfolder";
            this.B_RemoveHotfolder.Size = new System.Drawing.Size(75, 23);
            this.B_RemoveHotfolder.TabIndex = 3;
            this.B_RemoveHotfolder.Text = "Remove";
            this.B_RemoveHotfolder.UseVisualStyleBackColor = true;
            this.B_RemoveHotfolder.Click += new System.EventHandler(this.B_RemoveHotfolder_Click);
            // 
            // B_EditHotfolder
            // 
            this.B_EditHotfolder.Location = new System.Drawing.Point(87, 156);
            this.B_EditHotfolder.Name = "B_EditHotfolder";
            this.B_EditHotfolder.Size = new System.Drawing.Size(75, 23);
            this.B_EditHotfolder.TabIndex = 2;
            this.B_EditHotfolder.Text = "Edit";
            this.B_EditHotfolder.UseVisualStyleBackColor = true;
            this.B_EditHotfolder.Click += new System.EventHandler(this.B_EditHotfolder_Click);
            // 
            // B_AddHotfolder
            // 
            this.B_AddHotfolder.Location = new System.Drawing.Point(6, 156);
            this.B_AddHotfolder.Name = "B_AddHotfolder";
            this.B_AddHotfolder.Size = new System.Drawing.Size(75, 23);
            this.B_AddHotfolder.TabIndex = 1;
            this.B_AddHotfolder.Text = "Add";
            this.B_AddHotfolder.UseVisualStyleBackColor = true;
            this.B_AddHotfolder.Click += new System.EventHandler(this.B_AddHotfolder_Click);
            // 
            // LV_Hotfolders
            // 
            this.LV_Hotfolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_Formatter,
            this.CH_Mode,
            this.CH_watchedFolder,
            this.CH_filter,
            this.CH_OutputFolder,
            this.CH_OutputFileScheme,
            this.CH_OnRename,
            this.CH_RemoveOld});
            this.LV_Hotfolders.FullRowSelect = true;
            this.LV_Hotfolders.GridLines = true;
            this.LV_Hotfolders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_Hotfolders.HideSelection = false;
            this.LV_Hotfolders.Location = new System.Drawing.Point(6, 19);
            this.LV_Hotfolders.Name = "LV_Hotfolders";
            this.LV_Hotfolders.Size = new System.Drawing.Size(688, 131);
            this.LV_Hotfolders.TabIndex = 0;
            this.LV_Hotfolders.UseCompatibleStateImageBehavior = false;
            this.LV_Hotfolders.View = System.Windows.Forms.View.Details;
            this.LV_Hotfolders.SelectedIndexChanged += new System.EventHandler(this.LV_Hotfolders_SelectedIndexChanged);
            // 
            // CH_watchedFolder
            // 
            this.CH_watchedFolder.Text = "Watched folder";
            // 
            // CH_filter
            // 
            this.CH_filter.Text = "File filter";
            // 
            // CH_Formatter
            // 
            this.CH_Formatter.Text = "Formatter";
            // 
            // CH_Mode
            // 
            this.CH_Mode.Text = "Mode";
            // 
            // CH_OutputFolder
            // 
            this.CH_OutputFolder.Text = "Output folder";
            // 
            // CH_OutputFileScheme
            // 
            this.CH_OutputFileScheme.Text = "Output file scheme";
            // 
            // CH_OnRename
            // 
            this.CH_OnRename.Text = "Rename";
            // 
            // CH_RemoveOld
            // 
            this.CH_RemoveOld.Text = "Remove old";
            // 
            // CB_Hotfolder
            // 
            this.CB_Hotfolder.AutoSize = true;
            this.CB_Hotfolder.Location = new System.Drawing.Point(8, 6);
            this.CB_Hotfolder.Name = "CB_Hotfolder";
            this.CB_Hotfolder.Size = new System.Drawing.Size(101, 17);
            this.CB_Hotfolder.TabIndex = 0;
            this.CB_Hotfolder.Text = "Hotfolder active";
            this.CB_Hotfolder.UseVisualStyleBackColor = true;
            this.CB_Hotfolder.Click += new System.EventHandler(this.CB_Hotfolder_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 305);
            this.Controls.Add(this.TC_SettingTabs);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_SaveAndClose);
            this.Controls.Add(this.MI_SettingsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MI_SettingsMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MI_SettingsMenu.ResumeLayout(false);
            this.MI_SettingsMenu.PerformLayout();
            this.TC_SettingTabs.ResumeLayout(false);
            this.TP_Application.ResumeLayout(false);
            this.TP_Hotfolder.ResumeLayout(false);
            this.TP_Hotfolder.PerformLayout();
            this.GB_Hotfolder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CB_MinimizeToTray;
        private System.Windows.Forms.CheckBox CB_AskBeforeClose;
        private System.Windows.Forms.Button B_SaveAndClose;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.CheckBox CB_CheckUpdatesOnStartup;
        private System.Windows.Forms.MenuStrip MI_SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem exportSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl TC_SettingTabs;
        private System.Windows.Forms.TabPage TP_Application;
        private System.Windows.Forms.TabPage TP_Hotfolder;
        private System.Windows.Forms.GroupBox GB_Hotfolder;
        private System.Windows.Forms.CheckBox CB_Hotfolder;
        private System.Windows.Forms.ListView LV_Hotfolders;
        private System.Windows.Forms.ColumnHeader CH_watchedFolder;
        private System.Windows.Forms.ColumnHeader CH_filter;
        private System.Windows.Forms.ColumnHeader CH_Formatter;
        private System.Windows.Forms.ColumnHeader CH_OutputFolder;
        private System.Windows.Forms.ColumnHeader CH_OutputFileScheme;
        private System.Windows.Forms.ColumnHeader CH_OnRename;
        private System.Windows.Forms.ColumnHeader CH_RemoveOld;
        private System.Windows.Forms.Button B_RemoveHotfolder;
        private System.Windows.Forms.Button B_EditHotfolder;
        private System.Windows.Forms.Button B_AddHotfolder;
        private System.Windows.Forms.ColumnHeader CH_Mode;
    }
}