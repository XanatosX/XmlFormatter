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
using System.Xml.Linq;
using System.Xml.XPath;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Manager;

namespace XmlFormatter.src.Windows
{
    public partial class Settings : Form
    {
        private readonly ISettingsManager settingManager;
        private readonly string settingFile;

        /// <summary>
        /// Create a new settings window
        /// </summary>
        public Settings(ISettingsManager settingManager, string settingFile)
        {
            InitializeComponent();

            this.settingManager = settingManager;
            this.settingFile = settingFile;

            SetupToolTip(CB_MinimizeToTray);
            SetupToolTip(CB_AskBeforeClose);
            SetupToolTip(CB_CheckUpdatesOnStartup);

            SetupControls();

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
            CB_CheckUpdatesOnStartup.Checked = Properties.Settings.Default.SearchUpdateOnStartup;
        }

        /// <summary>
        /// The event for saving and closing the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the sendfer</param>
        private void B_SaveAndClose_Click(object sender, EventArgs e)
        {
            WriteSettings();
            settingManager.Save(settingFile);
            B_Cancel.PerformClick();
        }

        /// <summary>
        /// Write the settings into the property container
        /// </summary>
        private void WriteSettings()
        {
            Properties.Settings.Default.MinimizeToTray = CB_MinimizeToTray.Checked;
            Properties.Settings.Default.AskBeforeClosing = CB_AskBeforeClose.Checked;
            Properties.Settings.Default.SearchUpdateOnStartup = CB_CheckUpdatesOnStartup.Checked;
        }

        /// <summary>
        /// The event to cancel the settings changes 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the sendfer</param>
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This method will export the settings of the application so you can use it on another computer
        /// </summary>
        /// <param name="sender">The sender wo called the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files(*.xml)| *.xml";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            saveFileDialog.FileName += "_";
            saveFileDialog.FileName += Application.ProductName;
            saveFileDialog.FileName += "Settings";
            saveFileDialog.FileName += "_V";
            saveFileDialog.FileName += Properties.Settings.Default.ApplicationVersion;

            DialogResult result = saveFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            WriteSettings();
            settingManager.Save(saveFileDialog.FileName);
        }

        /// <summary>
        /// This method will allow you to import the exported settings back into the application
        /// </summary>
        /// <param name="sender">The sender wo called the event</param>
        /// <param name="e">The arguments provided by the sender</param>
        private void importSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files(*.xml)| *.xml";
            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            settingManager.Load(openFileDialog.FileName);
            ISettingScope scope = settingManager.GetScope("Default");
            if (scope == null)
            {
                return;
            }

            string storedVersion = scope.GetSetting("ApplicationVersion").GetValue<string>();
            VersionManager versionManager = new VersionManager();

            string lowestVersion = Properties.Settings.Default.LowestSupportedVersion;

            Version settingVersion = versionManager.ConvertInnerFormatToProperVersion(storedVersion);
            Version lowVersion = versionManager.ConvertInnerFormatToProperVersion(lowestVersion);
            Version highVersion = versionManager.GetApplicationVersion();

            if (settingVersion < lowVersion || settingVersion > highVersion)
            {
                string message = "Import failed because setting file is not supported in this version";
                message += "\r\n\r\nLowest supported version: " + lowVersion;
                message += "\r\nApplication version: " + highVersion;
                message += "\r\nSetting version: " + settingVersion;
                MessageBox.Show(
                    message,
                    "Not supported import",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                 );

                settingManager.Load(settingFile);
                return;
            }

            settingManager.Save(settingFile);
            SetupControls();
        }
    }
}
