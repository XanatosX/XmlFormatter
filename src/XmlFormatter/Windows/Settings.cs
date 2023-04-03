using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using XmlFormatter.DataContainer;
using XmlFormatter.Update;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Setting.Hotfolder;
using XmlFormatterModel.Update;
using XMLFormatterModel.Hotfolder;

namespace XmlFormatter.Windows
{
    /// <summary>
    /// Settings window
    /// </summary>
    public partial class Settings : Form
    {
        /// <summary>
        /// The setting manager to user
        /// </summary>
        private readonly ISettingsManager settingManager;

        /// <summary>
        /// The setting file to load
        /// </summary>
        private readonly string settingFile;

        /// <summary>
        /// The plugin manager to use
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new settings window
        /// </summary>
        public Settings(ISettingsManager settingManager, string settingFile, IPluginManager pluginManager)
        {
            InitializeComponent();

            this.settingManager = settingManager;
            this.settingFile = settingFile;
            this.pluginManager = pluginManager;

            SetupToolTip(CB_MinimizeToTray);
            SetupToolTip(CB_AskBeforeClose);
            SetupToolTip(CB_CheckUpdatesOnStartup);
            SetupToolTip(CB_Hotfolder);
            SetupToolTip(L_UpdateStrategy);
            SetupToolTip(CB_LoggingActive);
            B_EditHotfolder.Enabled = false;
            B_RemoveHotfolder.Enabled = false;
            B_RemoveHotfolder.Enabled = false;

            LV_Hotfolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            settingManager.Load(settingFile);

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
            CB_LoggingActive.Checked = Properties.Settings.Default.LoggingEnabled;

            LV_Hotfolders.Items.Clear();
            HotfolderExtension hotfolderExtension = new HotfolderExtension(settingManager, pluginManager);
            foreach (IHotfolder hotfolder in hotfolderExtension.GetHotFoldersFromSettings())
            {
                ListViewItem item = HotfolderToListView(hotfolder);
                LV_Hotfolders.Items.Add(item);
            }

            List<PluginMetaData> updatePlugins = pluginManager.ListPlugins<IUpdateStrategy>().ToList();

            foreach (PluginMetaData metaData in updatePlugins)
            {
                try
                {
                    CB_UpdateStrategy.Items.Add(new ComboboxPluginItem(metaData));
                    if (metaData.Type.ToString() == Properties.Settings.Default.UpdateStrategy)
                    {
                        CB_UpdateStrategy.SelectedIndex = CB_UpdateStrategy.Items.Count - 1;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            FillLogFolderView();
            LV_logFiles.Columns[0].Width = LV_logFiles.Width;
            if (CB_UpdateStrategy.SelectedIndex == -1 && CB_UpdateStrategy.Items.Count > 0)
            {
                CB_UpdateStrategy.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Fill in the list with all the log files
        /// </summary>
        private void FillLogFolderView()
        {
            LV_logFiles.Items.Clear();
            foreach (string file in Directory.GetFiles(Properties.Settings.Default.LoggingFolder))
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".log")
                {
                    continue;
                }
                ListViewItem listViewItem = new ListViewItem(fileInfo.Name)
                {
                    Tag = file
                };
                LV_logFiles.Items.Add(listViewItem);
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
            Properties.Settings.Default.LoggingEnabled = CB_LoggingActive.Checked;

            if (CB_UpdateStrategy.SelectedItem is ComboboxPluginItem updateItem)
            {
                Properties.Settings.Default.UpdateStrategy = updateItem.Type.ToString();
            }

            ISettingScope hotfolderScope = settingManager.GetScope("Hotfolder");
            if (hotfolderScope == null)
            {
                hotfolderScope = new SettingScope("Hotfolder");
                settingManager.AddScope(hotfolderScope);
            }
            hotfolderScope.ClearSubScopes();
            foreach (ListViewItem item in LV_Hotfolders.Items)
            {
                if (item.Tag is IHotfolder)
                {
                    string hotfolderName = "Hotfolder_" + hotfolderScope.GetSubScopes().Count + 1;
                    ISettingScope hotfolderSubScope = CreateHotfolderScope(hotfolderName, (IHotfolder)item.Tag);
                    hotfolderScope.AddSubScope(hotfolderSubScope);
                }
            }
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
        private void ExportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            fileName += "_";
            fileName += Application.ProductName;
            fileName += "Settings";
            fileName += "_V";
            fileName += Properties.Settings.Default.ApplicationVersion;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML files(*.xml)| *.xml",
                FileName = fileName
            };


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
        private void ImportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML files(*.xml)| *.xml"
            };
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
            IVersionManagerFactory factory = new VersionManagerFactory();
            IVersionManager versionManager = factory.GetVersionManager();

            string lowestVersion = Properties.Settings.Default.LowestSupportedVersion;

            Version settingVersion = versionManager.ConvertStringToVersion(storedVersion);
            Version lowVersion = versionManager.ConvertStringToVersion(lowestVersion);
            TaskAwaiter<Version> awaiter = versionManager.GetLocalVersionAsync().GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Version highVersion = awaiter.GetResult();
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
            });
        }

        /// <summary>
        /// Event that the hotfolders where activated or disabled
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event arguments</param>
        private void CB_Hotfolder_Click(object sender, EventArgs e)
        {
            GB_Hotfolder.Enabled = CB_Hotfolder.Checked;
        }

        /// <summary>
        /// Someone did select or deselect an item in the list view
        /// Will change the enable state of the edit and remove button
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event arguments</param>
        private void LV_Hotfolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = LV_Hotfolders.SelectedItems.Count > 0;
            B_EditHotfolder.Enabled = enabled;
            B_RemoveHotfolder.Enabled = enabled;
        }

        /// <summary>
        /// Create a new hotfolder configuration
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event arguments</param>
        private void B_AddHotfolder_Click(object sender, EventArgs e)
        {
            HotfolderEditor hotfolderEditor = new HotfolderEditor(pluginManager);
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

                LV_Hotfolders.Items.Add(HotfolderToListView(hotfolder));
            }
        }
        /// <summary>
        /// Convert a hotfolder to a valid ListViewItem
        /// </summary>
        /// <param name="hotfolder">The hotfolder to convert</param>
        /// <returns>A valid ListViewItem</returns>
        private ListViewItem HotfolderToListView(IHotfolder hotfolder)
        {
            ListViewItem listViewItem = new ListViewItem("Plugin not found!");
            if (hotfolder.FormatterToUse != null)
            {
                listViewItem = new ListViewItem(hotfolder.FormatterToUse.Information.Name)
                {
                    Tag = hotfolder
                };
            }

            listViewItem.SubItems.Add(hotfolder.Mode.ToString());
            listViewItem.SubItems.Add(hotfolder.WatchedFolder);
            listViewItem.SubItems.Add(hotfolder.Filter);
            listViewItem.SubItems.Add(hotfolder.OutputFolder);
            listViewItem.SubItems.Add(hotfolder.OutputFileScheme);
            listViewItem.SubItems.Add(hotfolder.OnRename.ToString());
            listViewItem.SubItems.Add(hotfolder.RemoveOld.ToString());
            return listViewItem;
        }

        /// <summary>
        /// Create hotfolder scope from name and hotfolder
        /// </summary>
        /// <param name="name">The name of the new hotfolder configuration</param>
        /// <param name="hotfolder">The hotfolder to convert</param>
        /// <returns>A valid scope for the setting manager</returns>
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

        /// <summary>
        /// Event to remove a hotfolder configuration
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event arguments</param>
        private void B_RemoveHotfolder_Click(object sender, EventArgs e)
        {
            ListViewItem itemToRemove = LV_Hotfolders.SelectedItems[0];
            LV_Hotfolders.Items.Remove(itemToRemove);
        }

        /// <summary>
        /// Event to change a hotfolder configuration
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event arguments</param>
        private void B_EditHotfolder_Click(object sender, EventArgs e)
        {
            if (LV_Hotfolders.SelectedItems[0] == null)
            {
                return;
            }
            ListViewItem selectedItem = LV_Hotfolders.SelectedItems[0];
            if (selectedItem.Tag is IHotfolder)
            {
                HotfolderEditor hotfolderEditor = new HotfolderEditor((IHotfolder)selectedItem.Tag, pluginManager);
                hotfolderEditor.ShowDialog();
                if (hotfolderEditor.Saved)
                {
                    int position = LV_Hotfolders.SelectedItems[0].Index;
                    LV_Hotfolders.Items.Remove(selectedItem);
                    ListViewItem itemToAdd = HotfolderToListView(hotfolderEditor.Hotfolder);
                    LV_Hotfolders.Items.Insert(position, itemToAdd);
                    LV_Hotfolders.Items[position].Selected = true;
                }
            }
        }

        /// <summary>
        /// Selected log file changed
        /// </summary>
        /// <param name="sender">The sender ov the event</param>
        /// <param name="e">The arguments of the event</param>
        private void LV_logFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_logFiles.SelectedItems.Count == 0)
            {
                B_RemoveHotfolder.Enabled = false;
                return;
            }
            string file = LV_logFiles.SelectedItems[0].Tag.ToString();
            LoadLogFile(file);
        }

        /// <summary>
        /// Load the given log file and put it into the rich box
        /// </summary>
        /// <param name="file">The file to load</param>
        private void LoadLogFile(string file)
        {
            RTB_loggingText.Text = String.Empty;
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Write);
            using (StreamReader reader = new StreamReader(stream))
            {
                RTB_loggingText.Text = reader.ReadToEnd();
            }
            B_RemoveHotfolder.Enabled = true;
        }

        /// <summary>
        /// Delete the selected log file
        /// </summary>
        /// <param name="sender">The sender ov the event</param>
        /// <param name="e">The arguments of the event</param>
        private void B_DeleteLog_Click(object sender, EventArgs e)
        {
            if (LV_logFiles.SelectedItems.Count == 0)
            {
                return;
            }
            try
            {
                File.Delete(LV_logFiles.SelectedItems[0].Tag.ToString());
                FillLogFolderView();
            }
            catch (Exception ex)
            {
                string message = "Can't delete logfile " + LV_logFiles.SelectedItems[0].Tag.ToString();
                message += "\n\r" + ex.Message;
                MessageBox.Show(message, "Delete log error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open the log folder
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the event</param>
        private void B_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.LoggingFolder);
        }
    }
}
