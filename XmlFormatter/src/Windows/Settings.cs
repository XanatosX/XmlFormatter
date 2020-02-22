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
using XmlFormatter.src.Interfaces.Hotfolder;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Manager;
using XmlFormatter.src.Settings.DataStructure;
using XmlFormatter.src.Settings.Hotfolder;

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
            B_EditHotfolder.Enabled = false;
            B_RemoveHotfolder.Enabled = false;
            LV_Hotfolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

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
            CB_Hotfolder.Checked = Properties.Settings.Default.HotfolderActive;
            GB_Hotfolder.Enabled = CB_Hotfolder.Checked;

            HotfolderExtension hotfolderExtension = new HotfolderExtension(settingManager);
            foreach (IHotfolder hotfolder in hotfolderExtension.GetHotFoldersFromSettings())
            {
                ListViewItem item = HotfolderToListView(hotfolder);
                LV_Hotfolders.Items.Add(item);
            }
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
            Properties.Settings.Default.HotfolderActive = CB_Hotfolder.Checked;
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
            if (highVersion < lowVersion)
            {
                highVersion = lowVersion;
            }

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

        private void CB_Hotfolder_Click(object sender, EventArgs e)
        {
            GB_Hotfolder.Enabled = CB_Hotfolder.Checked;
        }

        private void LV_Hotfolders_ItemActivate(object sender, EventArgs e)
        {
            B_EditHotfolder.Enabled = true;
            B_RemoveHotfolder.Enabled = true;
        }

        private void B_AddHotfolder_Click(object sender, EventArgs e)
        {
            HotfolderEditor hotfolderEditor = new HotfolderEditor(null);
            hotfolderEditor.ShowDialog();
            if (hotfolderEditor.Saved)
            {
                ISettingScope hotfolderScope = settingManager.GetScope("Hotfolder");
                if (hotfolderScope == null)
                {
                    hotfolderScope = new SettingScope("Hotfolder");
                    settingManager.AddScope(hotfolderScope);
                }
                IHotfolder hotfolder = hotfolderEditor.Hotfolder;


                ISettingScope hotfolderConfig = CreateHotfolderScope(
                    "Hotfolder_" + hotfolderScope.GetSubScopes().Count + 1,
                    hotfolder
                );
                hotfolderScope.AddSubScope(hotfolderConfig);

                LV_Hotfolders.Items.Add(HotfolderToListView(hotfolder));
            }
        }

        private ListViewItem HotfolderToListView(IHotfolder hotfolder)
        {
            ListViewItem listViewItem = new ListViewItem(hotfolder.FormatterToUse.ToString());
            listViewItem.Tag = hotfolder;
            listViewItem.SubItems.Add(hotfolder.Mode.ToString());
            listViewItem.SubItems.Add(hotfolder.WatchedFolder);
            listViewItem.SubItems.Add(hotfolder.Filter);
            listViewItem.SubItems.Add(hotfolder.OutputFolder);
            listViewItem.SubItems.Add(hotfolder.OutputFileScheme);
            listViewItem.SubItems.Add(hotfolder.OnRename.ToString());
            listViewItem.SubItems.Add(hotfolder.RemoveOld.ToString());
            return listViewItem;
        }

        private ISettingScope CreateHotfolderScope(string name, IHotfolder hotfolder)
        {
            ISettingScope hotfolderConfig = new SettingScope(name);

            SettingPair typeSetting = new SettingPair("Type");
            typeSetting.SetValue(hotfolder.FormatterToUse.ToString());

            SettingPair modeSetting = new SettingPair("Mode");
            modeSetting.SetValue(hotfolder.Mode.ToString());

            SettingPair watchedFolder = new SettingPair("WatchedFolder");
            watchedFolder.SetValue(hotfolder.WatchedFolder);

            SettingPair filter = new SettingPair("Filter");
            filter.SetValue(hotfolder.Filter);

            SettingPair outputFolder = new SettingPair("OutputFolder");
            outputFolder.SetValue(hotfolder.OutputFolder);

            SettingPair scheme = new SettingPair("Scheme");
            scheme.SetValue(hotfolder.OutputFileScheme);

            SettingPair rename = new SettingPair("Rename");
            rename.SetValue(hotfolder.OnRename);

            SettingPair remove = new SettingPair("Remove");
            remove.SetValue(hotfolder.RemoveOld);

            hotfolderConfig.AddSetting(typeSetting);
            hotfolderConfig.AddSetting(modeSetting);
            hotfolderConfig.AddSetting(watchedFolder);
            hotfolderConfig.AddSetting(filter);
            hotfolderConfig.AddSetting(outputFolder);
            hotfolderConfig.AddSetting(scheme);
            hotfolderConfig.AddSetting(rename);
            hotfolderConfig.AddSetting(remove);

            return hotfolderConfig;
        }

        private void LV_Hotfolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = LV_Hotfolders.SelectedItems.Count > 0;
            B_EditHotfolder.Enabled = enabled;
            B_RemoveHotfolder.Enabled = enabled;
        }

        private void B_RemoveHotfolder_Click(object sender, EventArgs e)
        {
            ListViewItem itemToRemove = LV_Hotfolders.SelectedItems[0];
            LV_Hotfolders.Items.Remove(itemToRemove);

            ISettingScope hotfolderScope = settingManager.GetScope("Hotfolder");
            if (hotfolderScope == null)
            {
                hotfolderScope = new SettingScope("Hotfolder");
                settingManager.AddScope(hotfolderScope);
            }
            //hotfolderScope.
        }
    }
}
