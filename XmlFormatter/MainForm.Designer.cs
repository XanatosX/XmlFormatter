namespace XmlFormatter
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
            this.B_Select = new System.Windows.Forms.Button();
            this.TB_SelectedXml = new System.Windows.Forms.TextBox();
            this.B_Save = new System.Windows.Forms.Button();
            this.L_SelectedPath = new System.Windows.Forms.Label();
            this.CB_Mode = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MI_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Select
            // 
            this.B_Select.Location = new System.Drawing.Point(15, 67);
            this.B_Select.Name = "B_Select";
            this.B_Select.Size = new System.Drawing.Size(75, 23);
            this.B_Select.TabIndex = 0;
            this.B_Select.Text = "Select XML";
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
            this.L_SelectedPath.Size = new System.Drawing.Size(114, 13);
            this.L_SelectedPath.TabIndex = 3;
            this.L_SelectedPath.Text = "Selected XML file path";
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MI_Help
            // 
            this.MI_Help.Name = "MI_Help";
            this.MI_Help.Size = new System.Drawing.Size(44, 20);
            this.MI_Help.Text = "Help";
            this.MI_Help.Click += new System.EventHandler(this.MI_Help_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 99);
            this.Controls.Add(this.CB_Mode);
            this.Controls.Add(this.L_SelectedPath);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.TB_SelectedXml);
            this.Controls.Add(this.B_Select);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "XML Formatter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Select;
        private System.Windows.Forms.TextBox TB_SelectedXml;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Label L_SelectedPath;
        private System.Windows.Forms.ComboBox CB_Mode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MI_Help;
    }
}

