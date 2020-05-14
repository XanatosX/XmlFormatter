namespace XmlFormatter.src.Windows
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
            this.B_Select = new System.Windows.Forms.Button();
            this.TB_SelectedXml = new System.Windows.Forms.TextBox();
            this.B_Save = new System.Windows.Forms.Button();
            this.L_SelectedPath = new System.Windows.Forms.Label();
            this.CB_Mode = new System.Windows.Forms.ComboBox();
            this.MI_MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_HideToTray = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_CheckForUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_ReportIssue = new System.Windows.Forms.ToolStripMenuItem();
            this.L_Status = new System.Windows.Forms.Label();
            this.NI_Notification = new System.Windows.Forms.NotifyIcon(this.components);
            this.CB_Formatter = new System.Windows.Forms.ComboBox();
            this.MI_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Select
            // 
            this.B_Select.Location = new System.Drawing.Point(15, 67);
            this.B_Select.Name = "B_Select";
            this.B_Select.Size = new System.Drawing.Size(96, 23);
            this.B_Select.TabIndex = 0;
            this.B_Select.Text = "B_Select";
            this.B_Select.UseVisualStyleBackColor = true;
            this.B_Select.Click += new System.EventHandler(this.B_Select_Click);
            // 
            // TB_SelectedXml
            // 
            this.TB_SelectedXml.Location = new System.Drawing.Point(15, 41);
            this.TB_SelectedXml.Name = "TB_SelectedXml";
            this.TB_SelectedXml.Size = new System.Drawing.Size(773, 20);
            this.TB_SelectedXml.TabIndex = 1;
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(713, 66);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 2;
            this.B_Save.Text = "Save formatted";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // L_SelectedPath
            // 
            this.L_SelectedPath.AutoSize = true;
            this.L_SelectedPath.Location = new System.Drawing.Point(12, 24);
            this.L_SelectedPath.Name = "L_SelectedPath";
            this.L_SelectedPath.Size = new System.Drawing.Size(83, 13);
            this.L_SelectedPath.TabIndex = 3;
            this.L_SelectedPath.Text = "L_SelectedPath";
            // 
            // CB_Mode
            // 
            this.CB_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Mode.FormattingEnabled = true;
            this.CB_Mode.Items.AddRange(new object[] {
            "Formatted",
            "Flat"});
            this.CB_Mode.Location = new System.Drawing.Point(586, 67);
            this.CB_Mode.Name = "CB_Mode";
            this.CB_Mode.Size = new System.Drawing.Size(121, 21);
            this.CB_Mode.TabIndex = 4;
            // 
            // MI_MainMenu
            // 
            this.MI_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.MI_Help,
            this.MI_ReportIssue});
            this.MI_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MI_MainMenu.Name = "MI_MainMenu";
            this.MI_MainMenu.Size = new System.Drawing.Size(800, 24);
            this.MI_MainMenu.TabIndex = 5;
            this.MI_MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_HideToTray,
            this.MI_Settings});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MI_HideToTray
            // 
            this.MI_HideToTray.Name = "MI_HideToTray";
            this.MI_HideToTray.Size = new System.Drawing.Size(136, 22);
            this.MI_HideToTray.Text = "Hide to tray";
            this.MI_HideToTray.Click += new System.EventHandler(this.MI_HideToTray_Click);
            // 
            // MI_Settings
            // 
            this.MI_Settings.Name = "MI_Settings";
            this.MI_Settings.Size = new System.Drawing.Size(136, 22);
            this.MI_Settings.Text = "Settings";
            this.MI_Settings.Click += new System.EventHandler(this.MI_Settings_Click);
            // 
            // MI_Help
            // 
            this.MI_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_CheckForUpdate,
            this.MI_About});
            this.MI_Help.Name = "MI_Help";
            this.MI_Help.Size = new System.Drawing.Size(44, 20);
            this.MI_Help.Text = "Help";
            // 
            // MI_CheckForUpdate
            // 
            this.MI_CheckForUpdate.Name = "MI_CheckForUpdate";
            this.MI_CheckForUpdate.Size = new System.Drawing.Size(165, 22);
            this.MI_CheckForUpdate.Text = "Check for update";
            this.MI_CheckForUpdate.Click += new System.EventHandler(this.MI_CheckForUpdate_Click);
            // 
            // MI_About
            // 
            this.MI_About.Name = "MI_About";
            this.MI_About.Size = new System.Drawing.Size(165, 22);
            this.MI_About.Text = "About";
            this.MI_About.Click += new System.EventHandler(this.MI_About_Click);
            // 
            // MI_ReportIssue
            // 
            this.MI_ReportIssue.Name = "MI_ReportIssue";
            this.MI_ReportIssue.Size = new System.Drawing.Size(83, 20);
            this.MI_ReportIssue.Text = "Report Issue";
            this.MI_ReportIssue.Click += new System.EventHandler(this.MI_ReportIssue_Click);
            // 
            // L_Status
            // 
            this.L_Status.AutoSize = true;
            this.L_Status.Location = new System.Drawing.Point(117, 72);
            this.L_Status.Name = "L_Status";
            this.L_Status.Size = new System.Drawing.Size(49, 13);
            this.L_Status.TabIndex = 7;
            this.L_Status.Text = "L_Status";
            // 
            // NI_Notification
            // 
            this.NI_Notification.Icon = ((System.Drawing.Icon)(resources.GetObject("NI_Notification.Icon")));
            this.NI_Notification.Text = "notifyIcon1";
            this.NI_Notification.Click += new System.EventHandler(this.NI_Notification_Click);
            // 
            // CB_Formatter
            // 
            this.CB_Formatter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Formatter.FormattingEnabled = true;
            this.CB_Formatter.Location = new System.Drawing.Point(436, 68);
            this.CB_Formatter.Name = "CB_Formatter";
            this.CB_Formatter.Size = new System.Drawing.Size(144, 21);
            this.CB_Formatter.TabIndex = 8;
            this.CB_Formatter.SelectedIndexChanged += new System.EventHandler(this.CB_Formatter_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 99);
            this.Controls.Add(this.CB_Formatter);
            this.Controls.Add(this.L_Status);
            this.Controls.Add(this.CB_Mode);
            this.Controls.Add(this.L_SelectedPath);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.TB_SelectedXml);
            this.Controls.Add(this.B_Select);
            this.Controls.Add(this.MI_MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MI_MainMenu;
            this.Name = "MainForm";
            this.Text = "XML Formatter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MI_MainMenu.ResumeLayout(false);
            this.MI_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Select;
        private System.Windows.Forms.TextBox TB_SelectedXml;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Label L_SelectedPath;
        private System.Windows.Forms.ComboBox CB_Mode;
        private System.Windows.Forms.MenuStrip MI_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MI_Help;
        private System.Windows.Forms.ToolStripMenuItem MI_CheckForUpdate;
        private System.Windows.Forms.ToolStripMenuItem MI_About;
        private System.Windows.Forms.Label L_Status;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon NI_Notification;
        private System.Windows.Forms.ToolStripMenuItem MI_HideToTray;
        private System.Windows.Forms.ToolStripMenuItem MI_Settings;
        private System.Windows.Forms.ToolStripMenuItem MI_ReportIssue;
        private System.Windows.Forms.ComboBox CB_Formatter;
    }
}

