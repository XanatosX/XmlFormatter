namespace XmlFormatter.src.Windows
{
    partial class VersionInformation
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Octokit");
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TP_General = new System.Windows.Forms.TabPage();
            this.L_VersionNumber = new System.Windows.Forms.Label();
            this.L_MaintainerName = new System.Windows.Forms.Label();
            this.L_Project = new System.Windows.Forms.Label();
            this.LL_Project = new System.Windows.Forms.LinkLabel();
            this.L_License = new System.Windows.Forms.Label();
            this.L_Maintainer = new System.Windows.Forms.Label();
            this.L_Version = new System.Windows.Forms.Label();
            this.TP_ThirdParty = new System.Windows.Forms.TabPage();
            this.LV_ThirdPartyLibraries = new System.Windows.Forms.ListView();
            this.CH_ThirdParty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LL_MIT = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.TP_General.SuspendLayout();
            this.TP_ThirdParty.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TP_General);
            this.tabControl1.Controls.Add(this.TP_ThirdParty);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(405, 108);
            this.tabControl1.TabIndex = 1;
            // 
            // TP_General
            // 
            this.TP_General.Controls.Add(this.LL_MIT);
            this.TP_General.Controls.Add(this.L_VersionNumber);
            this.TP_General.Controls.Add(this.L_MaintainerName);
            this.TP_General.Controls.Add(this.L_Project);
            this.TP_General.Controls.Add(this.LL_Project);
            this.TP_General.Controls.Add(this.L_License);
            this.TP_General.Controls.Add(this.L_Maintainer);
            this.TP_General.Controls.Add(this.L_Version);
            this.TP_General.Location = new System.Drawing.Point(4, 22);
            this.TP_General.Name = "TP_General";
            this.TP_General.Padding = new System.Windows.Forms.Padding(3);
            this.TP_General.Size = new System.Drawing.Size(397, 82);
            this.TP_General.TabIndex = 0;
            this.TP_General.Text = "General";
            this.TP_General.UseVisualStyleBackColor = true;
            // 
            // L_VersionNumber
            // 
            this.L_VersionNumber.AutoSize = true;
            this.L_VersionNumber.Location = new System.Drawing.Point(71, 10);
            this.L_VersionNumber.Name = "L_VersionNumber";
            this.L_VersionNumber.Size = new System.Drawing.Size(31, 13);
            this.L_VersionNumber.TabIndex = 7;
            this.L_VersionNumber.Text = "0.0.0";
            // 
            // L_MaintainerName
            // 
            this.L_MaintainerName.AutoSize = true;
            this.L_MaintainerName.Location = new System.Drawing.Point(71, 26);
            this.L_MaintainerName.Name = "L_MaintainerName";
            this.L_MaintainerName.Size = new System.Drawing.Size(53, 13);
            this.L_MaintainerName.TabIndex = 6;
            this.L_MaintainerName.Text = "XanatosX";
            // 
            // L_Project
            // 
            this.L_Project.AutoSize = true;
            this.L_Project.Location = new System.Drawing.Point(6, 58);
            this.L_Project.Name = "L_Project";
            this.L_Project.Size = new System.Drawing.Size(43, 13);
            this.L_Project.TabIndex = 4;
            this.L_Project.Text = "Project:";
            // 
            // LL_Project
            // 
            this.LL_Project.AutoSize = true;
            this.LL_Project.Location = new System.Drawing.Point(71, 58);
            this.LL_Project.Name = "LL_Project";
            this.LL_Project.Size = new System.Drawing.Size(212, 13);
            this.LL_Project.TabIndex = 3;
            this.LL_Project.TabStop = true;
            this.LL_Project.Tag = "https://github.com/XanatosX/XmlFormatter";
            this.LL_Project.Text = "https://github.com/XanatosX/XmlFormatter";
            this.LL_Project.Click += new System.EventHandler(this.OpenLinkLabel_Click);
            // 
            // L_License
            // 
            this.L_License.AutoSize = true;
            this.L_License.Location = new System.Drawing.Point(6, 42);
            this.L_License.Name = "L_License";
            this.L_License.Size = new System.Drawing.Size(47, 13);
            this.L_License.TabIndex = 2;
            this.L_License.Text = "License:";
            // 
            // L_Maintainer
            // 
            this.L_Maintainer.AutoSize = true;
            this.L_Maintainer.Location = new System.Drawing.Point(6, 26);
            this.L_Maintainer.Name = "L_Maintainer";
            this.L_Maintainer.Size = new System.Drawing.Size(59, 13);
            this.L_Maintainer.TabIndex = 1;
            this.L_Maintainer.Text = "Maintainer:";
            // 
            // L_Version
            // 
            this.L_Version.AutoSize = true;
            this.L_Version.Location = new System.Drawing.Point(6, 10);
            this.L_Version.Name = "L_Version";
            this.L_Version.Size = new System.Drawing.Size(45, 13);
            this.L_Version.TabIndex = 0;
            this.L_Version.Text = "Version:";
            // 
            // TP_ThirdParty
            // 
            this.TP_ThirdParty.Controls.Add(this.LV_ThirdPartyLibraries);
            this.TP_ThirdParty.Location = new System.Drawing.Point(4, 22);
            this.TP_ThirdParty.Name = "TP_ThirdParty";
            this.TP_ThirdParty.Padding = new System.Windows.Forms.Padding(3);
            this.TP_ThirdParty.Size = new System.Drawing.Size(397, 82);
            this.TP_ThirdParty.TabIndex = 1;
            this.TP_ThirdParty.Text = "Third Party";
            this.TP_ThirdParty.UseVisualStyleBackColor = true;
            // 
            // LV_ThirdPartyLibraries
            // 
            this.LV_ThirdPartyLibraries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_ThirdParty});
            this.LV_ThirdPartyLibraries.FullRowSelect = true;
            this.LV_ThirdPartyLibraries.HideSelection = false;
            listViewItem2.Tag = "https://github.com/octokit/octokit.net";
            this.LV_ThirdPartyLibraries.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.LV_ThirdPartyLibraries.Location = new System.Drawing.Point(6, 6);
            this.LV_ThirdPartyLibraries.Name = "LV_ThirdPartyLibraries";
            this.LV_ThirdPartyLibraries.Size = new System.Drawing.Size(385, 70);
            this.LV_ThirdPartyLibraries.TabIndex = 0;
            this.LV_ThirdPartyLibraries.UseCompatibleStateImageBehavior = false;
            this.LV_ThirdPartyLibraries.View = System.Windows.Forms.View.Details;
            this.LV_ThirdPartyLibraries.SelectedIndexChanged += new System.EventHandler(this.LV_ThirdPartyLibraries_SelectedIndexChanged);
            // 
            // CH_ThirdParty
            // 
            this.CH_ThirdParty.Text = "Third Party Libraries";
            // 
            // LL_MIT
            // 
            this.LL_MIT.AutoSize = true;
            this.LL_MIT.Location = new System.Drawing.Point(71, 42);
            this.LL_MIT.Name = "LL_MIT";
            this.LL_MIT.Size = new System.Drawing.Size(26, 13);
            this.LL_MIT.TabIndex = 8;
            this.LL_MIT.TabStop = true;
            this.LL_MIT.Tag = "https://github.com/XanatosX/XmlFormatter/blob/master/LICENSE";
            this.LL_MIT.Text = "MIT";
            this.LL_MIT.Click += new System.EventHandler(this.OpenLinkLabel_Click);
            // 
            // VersionInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 126);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "VersionInformation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VersionInformation";
            this.Load += new System.EventHandler(this.VersionInformation_Load);
            this.tabControl1.ResumeLayout(false);
            this.TP_General.ResumeLayout(false);
            this.TP_General.PerformLayout();
            this.TP_ThirdParty.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TP_General;
        private System.Windows.Forms.TabPage TP_ThirdParty;
        private System.Windows.Forms.ListView LV_ThirdPartyLibraries;
        private System.Windows.Forms.ColumnHeader CH_ThirdParty;
        private System.Windows.Forms.Label L_Project;
        private System.Windows.Forms.LinkLabel LL_Project;
        private System.Windows.Forms.Label L_License;
        private System.Windows.Forms.Label L_Maintainer;
        private System.Windows.Forms.Label L_Version;
        private System.Windows.Forms.Label L_VersionNumber;
        private System.Windows.Forms.Label L_MaintainerName;
        private System.Windows.Forms.LinkLabel LL_MIT;
    }
}