namespace XmlFormatter.src.Windows
{
    partial class PluginManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManager));
            this.TV_Plugins = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TC_PluginData = new System.Windows.Forms.TabControl();
            this.TP_GeneralInformation = new System.Windows.Forms.TabPage();
            this.TB_Description = new System.Windows.Forms.TextBox();
            this.L_Description = new System.Windows.Forms.Label();
            this.L_Version = new System.Windows.Forms.Label();
            this.L_Author = new System.Windows.Forms.Label();
            this.L_Name = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.TC_PluginData.SuspendLayout();
            this.TP_GeneralInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // TV_Plugins
            // 
            this.TV_Plugins.Location = new System.Drawing.Point(18, 18);
            this.TV_Plugins.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TV_Plugins.Name = "TV_Plugins";
            this.TV_Plugins.Size = new System.Drawing.Size(214, 653);
            this.TV_Plugins.TabIndex = 0;
            this.TV_Plugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Plugins_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TC_PluginData);
            this.panel1.Location = new System.Drawing.Point(243, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 655);
            this.panel1.TabIndex = 1;
            // 
            // TC_PluginData
            // 
            this.TC_PluginData.Controls.Add(this.TP_GeneralInformation);
            this.TC_PluginData.Location = new System.Drawing.Point(4, 5);
            this.TC_PluginData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TC_PluginData.Name = "TC_PluginData";
            this.TC_PluginData.SelectedIndex = 0;
            this.TC_PluginData.Size = new System.Drawing.Size(930, 646);
            this.TC_PluginData.TabIndex = 0;
            // 
            // TP_GeneralInformation
            // 
            this.TP_GeneralInformation.Controls.Add(this.TB_Description);
            this.TP_GeneralInformation.Controls.Add(this.L_Description);
            this.TP_GeneralInformation.Controls.Add(this.L_Version);
            this.TP_GeneralInformation.Controls.Add(this.L_Author);
            this.TP_GeneralInformation.Controls.Add(this.L_Name);
            this.TP_GeneralInformation.Location = new System.Drawing.Point(4, 29);
            this.TP_GeneralInformation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_GeneralInformation.Name = "TP_GeneralInformation";
            this.TP_GeneralInformation.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TP_GeneralInformation.Size = new System.Drawing.Size(922, 613);
            this.TP_GeneralInformation.TabIndex = 0;
            this.TP_GeneralInformation.Text = "General Information";
            this.TP_GeneralInformation.UseVisualStyleBackColor = true;
            // 
            // TB_Description
            // 
            this.TB_Description.Location = new System.Drawing.Point(14, 108);
            this.TB_Description.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TB_Description.Multiline = true;
            this.TB_Description.Name = "TB_Description";
            this.TB_Description.Size = new System.Drawing.Size(894, 487);
            this.TB_Description.TabIndex = 4;
            // 
            // L_Description
            // 
            this.L_Description.AutoSize = true;
            this.L_Description.Location = new System.Drawing.Point(9, 83);
            this.L_Description.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Description.Name = "L_Description";
            this.L_Description.Size = new System.Drawing.Size(89, 20);
            this.L_Description.TabIndex = 3;
            this.L_Description.Text = "Description";
            // 
            // L_Version
            // 
            this.L_Version.AutoSize = true;
            this.L_Version.Location = new System.Drawing.Point(9, 58);
            this.L_Version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Version.Name = "L_Version";
            this.L_Version.Size = new System.Drawing.Size(67, 20);
            this.L_Version.TabIndex = 2;
            this.L_Version.Text = "Version:";
            // 
            // L_Author
            // 
            this.L_Author.AutoSize = true;
            this.L_Author.Location = new System.Drawing.Point(9, 9);
            this.L_Author.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Author.Name = "L_Author";
            this.L_Author.Size = new System.Drawing.Size(61, 20);
            this.L_Author.TabIndex = 1;
            this.L_Author.Text = "Author:";
            // 
            // L_Name
            // 
            this.L_Name.AutoSize = true;
            this.L_Name.Location = new System.Drawing.Point(9, 34);
            this.L_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.L_Name.Name = "L_Name";
            this.L_Name.Size = new System.Drawing.Size(55, 20);
            this.L_Name.TabIndex = 0;
            this.L_Name.Text = "Name:";
            // 
            // PluginManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TV_Plugins);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PluginManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PluginManager";
            this.Load += new System.EventHandler(this.PluginManager_Load);
            this.panel1.ResumeLayout(false);
            this.TC_PluginData.ResumeLayout(false);
            this.TP_GeneralInformation.ResumeLayout(false);
            this.TP_GeneralInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TV_Plugins;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl TC_PluginData;
        private System.Windows.Forms.TabPage TP_GeneralInformation;
        private System.Windows.Forms.TextBox TB_Description;
        private System.Windows.Forms.Label L_Description;
        private System.Windows.Forms.Label L_Version;
        private System.Windows.Forms.Label L_Author;
        private System.Windows.Forms.Label L_Name;
    }
}