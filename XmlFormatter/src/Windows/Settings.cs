using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlFormatter.src.Windows
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            SetupToolTip(CB_MinimizeToTray);
            SetupToolTip(CB_AskBeforeClose);

            SetupControls();

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method will set the matching tool tip for the control
        /// </summary>
        /// <param name="control">The control to set the tip to</param>
        private void SetupToolTip(Control control)
        {
            ToolTip toolTip = new ToolTip();
            this.components.Add(toolTip);
            SetupToolTip(toolTip, control);
        }

        /// <summary>
        /// This method will set the matching tool tip for the control
        /// </summary>
        /// <param name="toolTip">The tool tip provider to use</param>
        /// <param name="control">The control to set the tip to</param>
        private void SetupToolTip(ToolTip toolTip, Control control)
        {
            string baseName = this.Name + "_" + control.Name;
            toolTip.ToolTipTitle = Properties.Resources.ResourceManager.GetString(baseName + "_Title");
            toolTip.SetToolTip(
                control,
                Properties.Resources.ResourceManager.GetString(baseName + "_Message")
            );
        }

        /// <summary>
        /// This method will setup the controls
        /// </summary>
        private void SetupControls()
        {
            CB_MinimizeToTray.Checked = Properties.Settings.Default.MinimizeToTray;
            CB_AskBeforeClose.Checked = Properties.Settings.Default.AskBeforeClosing;
        }

        /// <summary>
        /// The event for saving and closing the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the sendfer</param>
        private void B_SaveAndClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinimizeToTray = CB_MinimizeToTray.Checked;
            Properties.Settings.Default.AskBeforeClosing = CB_AskBeforeClose.Checked;

            Properties.Settings.Default.Save();
            B_Cancel.PerformClick();
        }

        /// <summary>
        /// The event to cancel the settings changes 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the sendfer</param>
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Close();
        }
    }
}
