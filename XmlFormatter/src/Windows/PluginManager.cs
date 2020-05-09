using Octokit;
using PluginFramework.src.DataContainer;
using PluginFramework.src.Interfaces.Manager;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Settings;
using XmlFormatter.src.Settings.DataStructure;

namespace XmlFormatter.src.Windows
{
    public partial class PluginManager : Form
    {
        private readonly IPluginManager pluginManager;
        private readonly ISettingsManager settingsManager;
        private readonly string settingFile;
        private IPluginOverhead currentPlugin;
        private Panel currentSettingsPanel;

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

        private void AddPluginsOfType<T>(TreeNode node) where T : IPluginOverhead
        {
            List<PluginMetaData> pluginMetas = pluginManager.ListPlugins<T>();
            foreach (PluginMetaData metaData in pluginMetas)
            {
                TreeNode currentPlugin = new TreeNode(metaData.Information.Name)
                {
                    Tag = metaData
                };
                node.Nodes.Add(currentPlugin);
            }
        }

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

                    
                    if (currentPlugin.GetSettingsPage() == null)
                    {
                        return;
                    }

                    AddSettingsTab();
                    UserControl control = currentPlugin.GetSettingsPage();
                    control.Width = currentSettingsPanel.Width;
                    control.Height = currentSettingsPanel.Height;
                    currentSettingsPanel.Controls.Add(control);
                }
            }
        }

        private PluginSettings ConvertToPluginSettings(ISettingScope settingScope)
        {
            PluginSettings returnSettings = new PluginSettings();
            foreach (SettingPair settingPair in settingScope.GetSettings())
            {
                returnSettings.AddValue(settingPair.Name, settingPair.Value);
            }

            return returnSettings;
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            if (currentPlugin != null)
            {
                PluginSettings settings = currentPlugin.Settings;
                string scopeName = GetScopeName();
                ISettingScope scope = new SettingScope(scopeName);
                foreach(KeyValuePair<string, object> settingPair in settings.Settings)
                {
                    ISettingPair pair = new SettingPair(settingPair.Key);
                    pair.SetValue(settingPair.Value);
                    scope.AddSetting(pair);
                }

                settingsManager.AddScope(scope);
                settingsManager.Save(settingFile);
            }
        }

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
                
                Button button = new Button()
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

        private void RemoveSettingsTab()
        {
            if (TC_PluginData.TabPages.Count == 2)
            {
                TC_PluginData.TabPages.RemoveAt(1);
                currentSettingsPanel = null;
            }
        }

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