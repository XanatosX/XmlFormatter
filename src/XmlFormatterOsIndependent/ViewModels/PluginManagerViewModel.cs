using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterOsIndependent.Models;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Window to list all the plugin data
    /// </summary>
    public partial class PluginManagerViewModel : ObservableObject
    {
        /// <summary>
        /// Is the information panel to the right visible
        /// </summary>
        [ObservableProperty]
        private bool panelVisible;

        /// <summary>
        /// Private plugin information which should be displayed
        /// </summary>
        [ObservableProperty]
        private PluginInformation pluginInformation;

        /// <summary>
        /// The groups for the plugins to be shown in the tree view
        /// </summary>
        public List<PluginTreeViewGroup> PluginGroups { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="viewContainer">The container for the parent window and the current window</param>
        /// <param name="managerFactory">The factory to create the plugin manager</param>
        public PluginManagerViewModel(IPluginManager pluginManager) //ViewContainer viewContainer, 
        {
            PanelVisible = false;
            PluginGroups = new List<PluginTreeViewGroup>();
            PluginTreeViewGroup formatterGroup = new PluginTreeViewGroup("Formatter");
            PluginTreeViewGroup updaterGroup = new PluginTreeViewGroup("Updater");
            List<PluginMetaData> formatters = pluginManager.ListPlugins<IFormatter>().ToList();
            foreach (PluginMetaData formatter in formatters)
            {
                formatterGroup.Add(new PluginTreeViewItem(formatter, Enums.PluginType.Formatter));
            }
            List<PluginMetaData> updaters = pluginManager.ListPlugins<IUpdateStrategy>().ToList();
            foreach (PluginMetaData updater in updaters)
            {
                updaterGroup.Add(new PluginTreeViewItem(updater, Enums.PluginType.Updater));
            }
            PluginGroups.Add(formatterGroup);
            PluginGroups.Add(updaterGroup);
        }

        [RelayCommand]
        public void OpenPlugin(PluginInformation pluginInformation)
        {
            PanelVisible = true;
            PluginInformation = pluginInformation;
        }
    }
}
