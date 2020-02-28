using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XmlFormatter.src.Interfaces.Hotfolder;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Interfaces.Updates;
using XmlFormatter.src.Manager;
using XmlFormatter.src.Settings.DataStructure;
using XmlFormatter.src.Settings.Hotfolder;

namespace XmlFormatter.src.Windows
{
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
        /// A dictionary with all the update strategies
        /// </summary>
        private readonly Dictionary<string, IUpdateStrategy> updateStrategies;

        /// <summary>
        /// Create a new settings window
        /// </summary>
        public Settings(ISettingsManager settingManager, string settingFile)
        {
            InitializeComponent();

            this.settingManager = settingManager;
            this.settingFile = settingFile;
            updateStrategies = new Dictionary<string, IUpdateStrategy>();

            setupToolTip(CB_MinimizeToTray);
            setupToolTip(CB_AskBeforeClose);
            setupToolTip(CB_CheckUpdatesOnStartup);
            setupToolTip(CB_Hotfolder);
            setupToolTip(L_UpdateStrategy);
            B_EditHotfolder.Enabled = false;
            B_RemoveHotfolder.Enabled = false;
            LV_Hotfolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            setupControls();
        }

        /// <summary>
        /// This method will set the matching tool tip for the control
        /// </summary>
        /// <param name="control">The control to set the tip to</param>
        private void setupToolTip(Control control)
        {
            ToolTip toolTip = new ToolTip();
            this.components.Add(toolTip);
            setupToolTip(toolTip, control);
        }

        /// <summary>
        /// This method will set the matching tool tip for the control
        /// </summary>
        /// <param name="toolTip">The tool tip provider to use</param>
        /// <param name="control">The control to set the tip to</param>
        private void setupToolTip(ToolTip toolTip, Control control)
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
        private void setupControls()
        {
            CB_MinimizeToTray.Checked = Properties.Settings.Default.MinimizeToTray;
            CB_AskBeforeClose.Checked = Properties.Settings.Default.AskBeforeClosing;
            CB_CheckUpdatesOnStartup.Checked = Properties.Settings.Default.SearchUpdateOnStartup;
            CB_Hotfolder.Checked = Properties.Settings.Default.HotfolderActive;
            GB_Hotfolder.Enabled = CB_Hotfolder.Checked;

            LV_Hotfolders.Items.Clear();
            HotfolderExtension hotfolderExtension = new HotfolderExtension(settingManager);
            foreach (IHotfolder hotfolder in hotfolderExtension.GetHotFoldersFromSettings())
            {
                ListViewItem item = hotfolderToListView(hotfolder);
                LV_Hotfolders.Items.Add(item);
            }

            Type type = typeof(IUpdateStrategy);
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            var possibleStrategies = types.Where(currentType => currentType.GetInterfaces().Contains(type));

            foreach (Type currentStrategy in possibleStrategies)
            {
                try
                {
                    IUpdateStrategy updateStrategy = (IUpdateStrategy)Activator.CreateInstance(currentStrategy);
                    updateStrategies.Add(updateStrategy.DisplayName, updateStrategy);
                    CB_UpdateStrategy.Items.Add(updateStrategy.DisplayName);
                    var test = currentStrategy.ToString();
                    var test2 = Properties.Settings.Default.UpdateStrategy;
                    if (currentStrategy.ToString() == Properties.Settings.Default.UpdateStrategy)
                    {
                        CB_UpdateStrategy.SelectedIndex = CB_UpdateStrategy.Items.Count - 1;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// The event for saving and closing the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the sendfer</param>
        private void B_SaveAndClose_Click(object sender, EventArgs e)
        {
            writeSettings();
            settingManager.Save(settingFile);
            B_Cancel.PerformClick();
        }

        /// <summary>
        /// Write the settings into the property container
        /// </summary>
        private void writeSettings()
        {
            Properties.Settings.Default.MinimizeToTray = CB_MinimizeToTray.Checked;
            Properties.Settings.Default.AskBeforeClosing = CB_AskBeforeClose.Checked;
            Properties.Settings.Default.SearchUpdateOnStartup = CB_CheckUpdatesOnStartup.Checked;
            Properties.Settings.Default.HotfolderActive = CB_Hotfolder.Checked;
            string strategyName = CB_UpdateStrategy.SelectedItem.ToString();
            if (updateStrategies.ContainsKey(strategyName))
            {
                IUpdateStrategy updateStrategy = updateStrategies[strategyName];
                Properties.Settings.Default.UpdateStrategy = updateStrategy.GetType().ToString();
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
                    ISettingScope hotfolderSubScope = createHotfolderScope(hotfolderName, (IHotfolder)item.Tag);
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

            writeSettings();
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
            setupControls();
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

                LV_Hotfolders.Items.Add(hotfolderToListView(hotfolder));
            }
        }
        /// <summary>
        /// Convert a hotfolder to a valid ListViewItem
        /// </summary>
        /// <param name="hotfolder">The hotfolder to convert</param>
        /// <returns>A valid ListViewItem</returns>
        private ListViewItem hotfolderToListView(IHotfolder hotfolder)
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

        /// <summary>
        /// Create hotfolder scope from name and hotfolder
        /// </summary>
        /// <param name="name">The name of the new hotfolder configuration</param>
        /// <param name="hotfolder">The hotfolder to convert</param>
        /// <returns>A valid scope for the setting manager</returns>
        private ISettingScope createHotfolderScope(string name, IHotfolder hotfolder)
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
                HotfolderEditor hotfolderEditor = new HotfolderEditor((IHotfolder)selectedItem.Tag);
                hotfolderEditor.ShowDialog();
                if (hotfolderEditor.Saved)
                {
                    int position = LV_Hotfolders.SelectedItems[0].Index;
                    LV_Hotfolders.Items.Remove(selectedItem);
                    ListViewItem itemToAdd = hotfolderToListView(hotfolderEditor.Hotfolder);
                    LV_Hotfolders.Items.Insert(position, itemToAdd);
                    LV_Hotfolders.Items[position].Selected = true;
                }
            }
        }
    }
}
