namespace XmlFormatter.Windows
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
            this.CB_AskBeforeClose = new System.Windows.Forms.CheckBox();
            this.CB_MinimizeToTray = new System.Windows.Forms.CheckBox();
            this.CB_CheckUpdatesOnStartup = new System.Windows.Forms.CheckBox();
            this.B_SaveAndClose = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.MI_SettingsMenu = new System.Windows.Forms.MenuStrip();
            this.exportSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TC_SettingTabs = new System.Windows.Forms.TabControl();
            this.TP_Application = new System.Windows.Forms.TabPage();
            this.GB_Update = new System.Windows.Forms.GroupBox();
            this.L_UpdateStrategy = new System.Windows.Forms.Label();
            this.CB_UpdateStrategy = new System.Windows.Forms.ComboBox();
            this.TP_Logging = new System.Windows.Forms.TabPage();
            this.B_OpenFolder = new System.Windows.Forms.Button();
            this.B_DeleteLog = new System.Windows.Forms.Button();
            this.LV_logFiles = new System.Windows.Forms.ListView();
            this.CH_logFiles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RTB_loggingText = new System.Windows.Forms.RichTextBox();
            this.CB_LoggingActive = new System.Windows.Forms.CheckBox();
            this.TP_Hotfolder = new System.Windows.Forms.TabPage();
            this.GB_Hotfolder = new System.Windows.Forms.GroupBox();
            this.B_RemoveHotfolder = new System.Windows.Forms.Button();
            this.B_EditHotfolder = new System.Windows.Forms.Button();
            this.B_AddHotfolder = new System.Windows.Forms.Button();
            this.LV_Hotfolders = new System.Windows.Forms.ListView();
            this.CH_Formatter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_Mode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_watchedFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_filter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OutputFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OutputFileScheme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_OnRename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_RemoveOld = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CB_Hotfolder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.MI_SettingsMenu.SuspendLayout();
            this.TC_SettingTabs.SuspendLayout();
            this.TP_Application.SuspendLayout();
            this.GB_Update.SuspendLayout();
            this.TP_Logging.SuspendLayout();
            this.TP_Hotfolder.SuspendLayout();
            this.GB_Hotfolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_AskBeforeClose);
            this.groupBox1.Controls.Add(this.CB_MinimizeToTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1050, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application settings";
            // 
            // CB_AskBeforeClose
            // 
            this.CB_AskBeforeClose.AutoSize = true;
            this.CB_AskBeforeClose.Location = new System.Drawing.Point(9, 65);
            this.CB_AskBeforeClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_AskBeforeClose.Name = "CB_AskBeforeClose";
            this.CB_AskBeforeClose.Size = new System.Drawing.Size(165, 24);
            this.CB_AskBeforeClose.TabIndex = 1;
            this.CB_AskBeforeClose.Text = "Ask before closing";
            this.CB_AskBeforeClose.UseVisualStyleBackColor = true;
            // 
            // CB_MinimizeToTray
            // 
            this.CB_MinimizeToTray.AutoSize = true;
            this.CB_MinimizeToTray.Location = new System.Drawing.Point(9, 29);
            this.CB_MinimizeToTray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_MinimizeToTray.Name = "CB_MinimizeToTray";
            this.CB_MinimizeToTray.Size = new System.Drawing.Size(144, 24);
            this.CB_MinimizeToTray.TabIndex = 0;
            this.CB_MinimizeToTray.Text = "Minimize to tray";
            this.CB_MinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // CB_CheckUpdatesOnStartup
            // 
            this.CB_CheckUpdatesOnStartup.AutoSize = true;
            this.CB_CheckUpdatesOnStartup.Location = new System.Drawing.Point(9, 22);
            this.CB_CheckUpdatesOnStartup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_CheckUpdatesOnStartup.Name = "CB_CheckUpdatesOnStartup";
            this.CB_CheckUpdatesOnStartup.Size = new System.Drawing.Size(241, 24);
            this.CB_CheckUpdatesOnStartup.TabIndex = 2;
            this.CB_CheckUpdatesOnStartup.Text = "Check for updates on startup";
            this.CB_CheckUpdatesOnStartup.UseVisualStyleBackColor = true;
            // 
            // B_SaveAndClose
            // 
            this.B_SaveAndClose.Location = new System.Drawing.Point(18, 422);
            this.B_SaveAndClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_SaveAndClose.Name = "B_SaveAndClose";
            this.B_SaveAndClose.Size = new System.Drawing.Size(156, 35);
            this.B_SaveAndClose.TabIndex = 1;
            this.B_SaveAndClose.Text = "Save and close";
            this.B_SaveAndClose.UseVisualStyleBackColor = true;
            this.B_SaveAndClose.Click += new System.EventHandler(this.B_SaveAndClose_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(956, 422);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(112, 35);
            this.B_Cancel.TabIndex = 2;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // MI_SettingsMenu
            // 
            this.MI_SettingsMenu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.MI_SettingsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MI_SettingsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSettingsToolStripMenuItem,
            this.importSettingsToolStripMenuItem});
            this.MI_SettingsMenu.Location = new System.Drawing.Point(0, 0);
            this.MI_SettingsMenu.Name = "MI_SettingsMenu";
            this.MI_SettingsMenu.Size = new System.Drawing.Size(1101, 36);
            this.MI_SettingsMenu.TabIndex = 3;
            this.MI_SettingsMenu.Text = "menuStrip1";
            // 
            // exportSettingsToolStripMenuItem
            // 
            this.exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            this.exportSettingsToolStripMenuItem.Size = new System.Drawing.Size(146, 30);
            this.exportSettingsToolStripMenuItem.Text = "Export settings";
            this.exportSettingsToolStripMenuItem.Click += new System.EventHandler(this.ExportSettingsToolStripMenuItem_Click);
            // 
            // importSettingsToolStripMenuItem
            // 
            this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
            this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size(150, 30);
            this.importSettingsToolStripMenuItem.Text = "Import settings";
            this.importSettingsToolStripMenuItem.Click += new System.EventHandler(this.ImportSettingsToolStripMenuItem_Click);
            // 
            // TC_SettingTabs
            // 
            this.TC_SettingTabs.Controls.Add(this.TP_Application);
            this.TC_SettingTabs.Controls.Add(this.TP_Logging);
            this.TC_SettingTabs.Controls.Add(this.TP_Hotfolder);
            this.TC_SettingTabs.Location = new System.Drawing.Point(0, 42);
            this.TC_SettingTabs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TC_SettingTabs.Name = "TC_SettingTabs";
            this.TC_SettingTabs.SelectedIndex = 0;
            this.TC_SettingTabs.Size = new System.Drawing.Size(1083, 377);
            this.TC_SettingTabs.TabIndex = 3;
            // 
            // TP_Application
            // 
            this.TP_Application.Controls.Add(this.GB_Update);
            this.TP_Application.Controls.Add(this.groupBox1);
            this.TP_Application.Location = new System.Drawing.Point(4, 29);
            this.TP_Application.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_Application.Name = "TP_Application";
            this.TP_Application.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_Application.Size = new System.Drawing.Size(1075, 344);
            this.TP_Application.TabIndex = 0;
            this.TP_Application.Text = "Application";
            this.TP_Application.UseVisualStyleBackColor = true;
            // 
            // GB_Update
            // 
            this.GB_Update.Controls.Add(this.CB_CheckUpdatesOnStartup);
            this.GB_Update.Controls.Add(this.L_UpdateStrategy);
            this.GB_Update.Controls.Add(this.CB_UpdateStrategy);
            this.GB_Update.Location = new System.Drawing.Point(12, 123);
            this.GB_Update.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Update.Name = "GB_Update";
            this.GB_Update.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Update.Size = new System.Drawing.Size(1050, 106);
            this.GB_Update.TabIndex = 1;
            this.GB_Update.TabStop = false;
            this.GB_Update.Text = "Updating";
            // 
            // L_UpdateStrategy
            // 
            this.L_UpdateStrategy.AutoSize = true;
            this.L_UpdateStrategy.Location = new System.Drawing.Point(4, 62);
            this.L_UpdateStrategy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_UpdateStrategy.Name = "L_UpdateStrategy";
            this.L_UpdateStrategy.Size = new System.Drawing.Size(123, 20);
            this.L_UpdateStrategy.TabIndex = 4;
            this.L_UpdateStrategy.Text = "Update strategy";
            // 
            // CB_UpdateStrategy
            // 
            this.CB_UpdateStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_UpdateStrategy.FormattingEnabled = true;
            this.CB_UpdateStrategy.Location = new System.Drawing.Point(136, 57);
            this.CB_UpdateStrategy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_UpdateStrategy.Name = "CB_UpdateStrategy";
            this.CB_UpdateStrategy.Size = new System.Drawing.Size(242, 28);
            this.CB_UpdateStrategy.TabIndex = 3;
            // 
            // TP_Logging
            // 
            this.TP_Logging.Controls.Add(this.B_OpenFolder);
            this.TP_Logging.Controls.Add(this.B_DeleteLog);
            this.TP_Logging.Controls.Add(this.LV_logFiles);
            this.TP_Logging.Controls.Add(this.RTB_loggingText);
            this.TP_Logging.Controls.Add(this.CB_LoggingActive);
            this.TP_Logging.Location = new System.Drawing.Point(4, 29);
            this.TP_Logging.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_Logging.Name = "TP_Logging";
            this.TP_Logging.Size = new System.Drawing.Size(1075, 344);
            this.TP_Logging.TabIndex = 2;
            this.TP_Logging.Text = "Logging";
            this.TP_Logging.UseVisualStyleBackColor = true;
            // 
            // B_OpenFolder
            // 
            this.B_OpenFolder.Location = new System.Drawing.Point(98, 294);
            this.B_OpenFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_OpenFolder.Name = "B_OpenFolder";
            this.B_OpenFolder.Size = new System.Drawing.Size(138, 35);
            this.B_OpenFolder.TabIndex = 6;
            this.B_OpenFolder.Text = "Open folder";
            this.B_OpenFolder.UseVisualStyleBackColor = true;
            this.B_OpenFolder.Click += new System.EventHandler(this.B_OpenFolder_Click);
            // 
            // B_DeleteLog
            // 
            this.B_DeleteLog.Location = new System.Drawing.Point(12, 294);
            this.B_DeleteLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_DeleteLog.Name = "B_DeleteLog";
            this.B_DeleteLog.Size = new System.Drawing.Size(76, 35);
            this.B_DeleteLog.TabIndex = 5;
            this.B_DeleteLog.Text = "Delete";
            this.B_DeleteLog.UseVisualStyleBackColor = true;
            this.B_DeleteLog.Click += new System.EventHandler(this.B_DeleteLog_Click);
            // 
            // LV_logFiles
            // 
            this.LV_logFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_logFiles});
            this.LV_logFiles.HideSelection = false;
            this.LV_logFiles.Location = new System.Drawing.Point(4, 40);
            this.LV_logFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LV_logFiles.Name = "LV_logFiles";
            this.LV_logFiles.Size = new System.Drawing.Size(222, 242);
            this.LV_logFiles.TabIndex = 4;
            this.LV_logFiles.UseCompatibleStateImageBehavior = false;
            this.LV_logFiles.View = System.Windows.Forms.View.Details;
            this.LV_logFiles.SelectedIndexChanged += new System.EventHandler(this.LV_logFiles_SelectedIndexChanged);
            // 
            // CH_logFiles
            // 
            this.CH_logFiles.Text = "Log files";
            // 
            // RTB_loggingText
            // 
            this.RTB_loggingText.Location = new System.Drawing.Point(237, 40);
            this.RTB_loggingText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RTB_loggingText.Name = "RTB_loggingText";
            this.RTB_loggingText.ReadOnly = true;
            this.RTB_loggingText.Size = new System.Drawing.Size(828, 242);
            this.RTB_loggingText.TabIndex = 3;
            this.RTB_loggingText.Text = "";
            // 
            // CB_LoggingActive
            // 
            this.CB_LoggingActive.AutoSize = true;
            this.CB_LoggingActive.Location = new System.Drawing.Point(12, 5);
            this.CB_LoggingActive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_LoggingActive.Name = "CB_LoggingActive";
            this.CB_LoggingActive.Size = new System.Drawing.Size(137, 24);
            this.CB_LoggingActive.TabIndex = 0;
            this.CB_LoggingActive.Text = "Logging active";
            this.CB_LoggingActive.UseVisualStyleBackColor = true;
            // 
            // TP_Hotfolder
            // 
            this.TP_Hotfolder.Controls.Add(this.GB_Hotfolder);
            this.TP_Hotfolder.Controls.Add(this.CB_Hotfolder);
            this.TP_Hotfolder.Location = new System.Drawing.Point(4, 29);
            this.TP_Hotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_Hotfolder.Name = "TP_Hotfolder";
            this.TP_Hotfolder.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_Hotfolder.Size = new System.Drawing.Size(1075, 344);
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
            this.GB_Hotfolder.Location = new System.Drawing.Point(12, 45);
            this.GB_Hotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Hotfolder.Name = "GB_Hotfolder";
            this.GB_Hotfolder.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GB_Hotfolder.Size = new System.Drawing.Size(1050, 283);
            this.GB_Hotfolder.TabIndex = 1;
            this.GB_Hotfolder.TabStop = false;
            this.GB_Hotfolder.Text = "Hotfolder";
            // 
            // B_RemoveHotfolder
            // 
            this.B_RemoveHotfolder.Location = new System.Drawing.Point(928, 240);
            this.B_RemoveHotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_RemoveHotfolder.Name = "B_RemoveHotfolder";
            this.B_RemoveHotfolder.Size = new System.Drawing.Size(112, 35);
            this.B_RemoveHotfolder.TabIndex = 3;
            this.B_RemoveHotfolder.Text = "Remove";
            this.B_RemoveHotfolder.UseVisualStyleBackColor = true;
            this.B_RemoveHotfolder.Click += new System.EventHandler(this.B_RemoveHotfolder_Click);
            // 
            // B_EditHotfolder
            // 
            this.B_EditHotfolder.Location = new System.Drawing.Point(130, 240);
            this.B_EditHotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_EditHotfolder.Name = "B_EditHotfolder";
            this.B_EditHotfolder.Size = new System.Drawing.Size(112, 35);
            this.B_EditHotfolder.TabIndex = 2;
            this.B_EditHotfolder.Text = "Edit";
            this.B_EditHotfolder.UseVisualStyleBackColor = true;
            this.B_EditHotfolder.Click += new System.EventHandler(this.B_EditHotfolder_Click);
            // 
            // B_AddHotfolder
            // 
            this.B_AddHotfolder.Location = new System.Drawing.Point(9, 240);
            this.B_AddHotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_AddHotfolder.Name = "B_AddHotfolder";
            this.B_AddHotfolder.Size = new System.Drawing.Size(112, 35);
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
            this.LV_Hotfolders.Location = new System.Drawing.Point(9, 29);
            this.LV_Hotfolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LV_Hotfolders.Name = "LV_Hotfolders";
            this.LV_Hotfolders.Size = new System.Drawing.Size(1030, 199);
            this.LV_Hotfolders.TabIndex = 0;
            this.LV_Hotfolders.UseCompatibleStateImageBehavior = false;
            this.LV_Hotfolders.View = System.Windows.Forms.View.Details;
            this.LV_Hotfolders.SelectedIndexChanged += new System.EventHandler(this.LV_Hotfolders_SelectedIndexChanged);
            // 
            // CH_Formatter
            // 
            this.CH_Formatter.Text = "Formatter";
            // 
            // CH_Mode
            // 
            this.CH_Mode.Text = "Mode";
            // 
            // CH_watchedFolder
            // 
            this.CH_watchedFolder.Text = "Watched folder";
            // 
            // CH_filter
            // 
            this.CH_filter.Text = "File filter";
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
            this.CB_Hotfolder.Location = new System.Drawing.Point(12, 9);
            this.CB_Hotfolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CB_Hotfolder.Name = "CB_Hotfolder";
            this.CB_Hotfolder.Size = new System.Drawing.Size(146, 24);
            this.CB_Hotfolder.TabIndex = 0;
            this.CB_Hotfolder.Text = "Hotfolder active";
            this.CB_Hotfolder.UseVisualStyleBackColor = true;
            this.CB_Hotfolder.Click += new System.EventHandler(this.CB_Hotfolder_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1101, 469);
            this.Controls.Add(this.TC_SettingTabs);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_SaveAndClose);
            this.Controls.Add(this.MI_SettingsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MI_SettingsMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.GB_Update.ResumeLayout(false);
            this.GB_Update.PerformLayout();
            this.TP_Logging.ResumeLayout(false);
            this.TP_Logging.PerformLayout();
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
        private System.Windows.Forms.GroupBox GB_Update;
        private System.Windows.Forms.Label L_UpdateStrategy;
        private System.Windows.Forms.ComboBox CB_UpdateStrategy;
        private System.Windows.Forms.TabPage TP_Logging;
        private System.Windows.Forms.RichTextBox RTB_loggingText;
        private System.Windows.Forms.CheckBox CB_LoggingActive;
        private System.Windows.Forms.ListView LV_logFiles;
        private System.Windows.Forms.ColumnHeader CH_logFiles;
        private System.Windows.Forms.Button B_OpenFolder;
        private System.Windows.Forms.Button B_DeleteLog;
    }
}