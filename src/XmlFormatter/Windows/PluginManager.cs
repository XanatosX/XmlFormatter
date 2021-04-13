using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XmlFormatterModel.Setting;

namespace XmlFormatter.Windows
{
    /// <summary>
    /// Window to manage the plugins
    /// </summary>
    public partial class PluginManager : Form
    {
        /// <summary>
        /// Instance of the plugin manager to use
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Instance of the settings manager to use
        /// </summary>
        private readonly ISettingsManager settingsManager;

        /// <summary>
        /// Path where the settings file is stored
        /// </summary>
        private readonly string settingFile;

        /// <summary>
        /// Current plugin which was selected
        /// </summary>
        private IPluginOverhead currentPlugin;

        /// <summary>
        /// Current panel to put the plugin settings into
        /// </summary>
        private Panel currentSettingsPanel;

        /// <summary>
        /// Create a new instance of the plugin manager window
        /// </summary>
        /// <param name="pluginManager">The plugin manager to use</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="settingsFileName">The file to save the settings in</param>
        public PluginManager(IPluginManager pluginManager, ISettingsManager settingsManager, string settingsFileName)
        {
            InitializeComponent();
            this.pluginManager = pluginManager;
            this.settingsManager = settingsManager;
            this.settingFile = settingsFileName;

            currentSettingsPanel = null;

            TC_PluginData.Enabled = false;
            L_Name.Tag = L_Name.Text;
            L_Version.Tag = L_Version.Text;
            L_Author.Tag = L_Author.Text;
            TB_Description.ReadOnly = true;
        }

        /// <summary>
        /// Load event of the plugin window
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void PluginManager_Load(object sender, EventArgs e)
        {
            TreeNode root = new TreeNode("Plugins");
            TreeNode formatter = new TreeNode("Formatter");
            AddPluginsOfType<IFormatter>(formatter);
            root.Nodes.Add(formatter);

            TreeNode updater = new TreeNode("Updater");
            AddPluginsOfType<IUpdateStrategy>(updater);
            root.Nodes.Add(updater);

            TV_Plugins.Nodes.Add(root);
        }

        /// <summary>
        /// Add new plugins to the tree view of a given type
        /// </summary>
        /// <typeparam name="T">The plugin type</typeparam>
        /// <param name="node">The root node to create the plugins in</param>
        private void AddPluginsOfType<T>(TreeNode node) where T : IPluginOverhead
        {
            List<PluginMetaData> pluginMetas = pluginManager.ListPlugins<T>();
            foreach (PluginMetaData metaData in pluginMetas)
            {
                TreeNode selectedPlugin = new TreeNode(metaData.Information.Name)
                {
                    Tag = metaData
                };
                node.Nodes.Add(selectedPlugin);
            }
        }

        /// <summary>
        /// After item was selected in the tree view
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the evend</param>
        private void TV_Plugins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentPlugin = null;
            RemoveSettingsTab();
            if (sender is TreeView treeView)
            {
                if (treeView.SelectedNode is TreeNode node && node.Tag is PluginMetaData metaData)
                {
                    L_Name.Text = L_Name.Tag.ToString() + " " + metaData.Information.Name;
                    L_Author.Text = L_Author.Tag.ToString() + " " + metaData.Information.Author;
                    L_Version.Text = L_Version.Tag.ToString() + " " + metaData.Information.Version;
                    TB_Description.Text = metaData.Information.Description;
                    TC_PluginData.Enabled = true;

                    currentPlugin = pluginManager.LoadPlugin<IPluginOverhead>(metaData.Id);
                    ISettingScope settings = settingsManager.GetScope(GetScopeName());
                    if (settings != null)
                    {
                        currentPlugin.ChangeSettings(ConvertToPluginSettings(settings));
                    }


                    //if (currentPlugin.GetSettingsPage() == null)
                    //{
                    //return;
                    //}

                    //AddSettingsTab();
                    //UserControl control = currentPlugin.GetSettingsPage();
                    //control.Width = currentSettingsPanel.Width;
                    //control.Height = currentSettingsPanel.Height;
                    //currentSettingsPanel.Controls.Add(control);
                }
            }
        }

        /// <summary>
        /// Convert ISettingScope to plugin settings
        /// </summary>
        /// <param name="settingScope">The settings scope to convert</param>
        /// <returns>The plugin settings ready to use</returns>
        private PluginSettings ConvertToPluginSettings(ISettingScope settingScope)
        {
            PluginSettings returnSettings = new PluginSettings();
            foreach (SettingPair settingPair in settingScope.GetSettings())
            {
                returnSettings.AddValue(settingPair.Name, settingPair.Value);
            }

            return returnSettings;
        }

        /// <summary>
        /// Save the plugin settings
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Arguments of the event</param>
        private void B_Save_Click(object sender, EventArgs e)
        {
            if (currentPlugin != null)
            {
                PluginSettings settings = currentPlugin.Settings;
                string scopeName = GetScopeName();
                ISettingScope scope = new SettingScope(scopeName);
                foreach (KeyValuePair<string, object> settingPair in settings.Settings)
                {
                    ISettingPair pair = new SettingPair(settingPair.Key);
                    pair.SetValue(settingPair.Value);
                    scope.AddSetting(pair);
                }

                settingsManager.AddScope(scope);
                settingsManager.Save(settingFile);
            }
        }

        /// <summary>
        /// Add the settings tab to the tab control
        /// </summary>
        private void AddSettingsTab()
        {
            if (TC_PluginData.TabPages.Count < 2)
            {
                TabPage settings = new TabPage("Settings");
                TC_PluginData.TabPages.Add(settings);

                currentSettingsPanel = new Panel
                {
                    Width = settings.Width,
                    Height = settings.Height - 31
                };

                Button button = new Button
                {
                    Text = "Save settings",
                    Location = new Point(5, settings.Height - 27),
                    Height = 23,
                    Width = 87
                };
                button.Click += B_Save_Click;

                settings.Controls.Add(button);
                settings.Controls.Add(currentSettingsPanel);
            }
        }

        /// <summary>
        /// Remove the settings tab from the tab control
        /// </summary>
        private void RemoveSettingsTab()
        {
            if (TC_PluginData.TabPages.Count == 2)
            {
                TC_PluginData.TabPages.RemoveAt(1);
                currentSettingsPanel = null;
            }
        }

        /// <summary>
        /// Get the name of the scope to save the settings to
        /// </summary>
        /// <returns>Name of the scope to use</returns>
        private string GetScopeName()
        {
            string returnString = string.Empty;
            if (currentPlugin != null)
            {
                returnString = "plugin_";
                returnString += currentPlugin.Information.Author;
                returnString += "_";
                returnString += currentPlugin.Information.Name;
                returnString = returnString.Replace(" ", "");
            }

            return returnString;
        }
    }
}