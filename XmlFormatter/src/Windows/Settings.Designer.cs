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
            this.groupBox1.SuspendLayout();
            this.MI_SettingsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_CheckUpdatesOnStartup);
            this.groupBox1.Controls.Add(this.CB_AskBeforeClose);
            this.groupBox1.Controls.Add(this.CB_MinimizeToTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 91);
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
            this.B_SaveAndClose.Location = new System.Drawing.Point(12, 124);
            this.B_SaveAndClose.Name = "B_SaveAndClose";
            this.B_SaveAndClose.Size = new System.Drawing.Size(104, 23);
            this.B_SaveAndClose.TabIndex = 1;
            this.B_SaveAndClose.Text = "Save and close";
            this.B_SaveAndClose.UseVisualStyleBackColor = true;
            this.B_SaveAndClose.Click += new System.EventHandler(this.B_SaveAndClose_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(176, 124);
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
            this.MI_SettingsMenu.Size = new System.Drawing.Size(263, 24);
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 159);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_SaveAndClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MI_SettingsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MI_SettingsMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MI_SettingsMenu.ResumeLayout(false);
            this.MI_SettingsMenu.PerformLayout();
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
    }
}