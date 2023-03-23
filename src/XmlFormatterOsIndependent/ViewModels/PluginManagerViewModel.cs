using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Models;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Window to list all the plugin data
    /// </summary>
    public class PluginManagerViewModel : ViewModelBase
    {
        /// <summary>
        /// Is the information panel to the right visible
        /// </summary>
        public bool PanelVisible
        {
            get => panelVisible;
            private set => this.RaiseAndSetIfChanged(ref panelVisible, value);
        }
        private bool panelVisible;

        /// <summary>
        /// The current plugin information to display
        /// </summary>
        public PluginInformation PluginInformation
        {
            get => pluginInformation;
            private set => this.RaiseAndSetIfChanged(ref pluginInformation, value);
        }

        /// <summary>
        /// Private plugin information which should be displayed
        /// </summary>
        private PluginInformation pluginInformation;

        /// <summary>
        /// The groups for the plugins to be shown in the tree view
        /// </summary>
        public List<PluginTreeViewGroup> PluginGroups { get; }

        /// <summary>
        /// Command to use to open the plugin and show the information
        /// </summary>
        public ITriggerCommand OpenPluginCommand { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="viewContainer">The container for the parent window and the current window</param>
        /// <param name="managerFactory">The factory to create the plugin manager</param>
        public PluginManagerViewModel(ISettingsManager settingsManager, IPluginManager pluginManager) //ViewContainer viewContainer, 
            : base(settingsManager, pluginManager)
        {
            PanelVisible = false;
            OpenPluginCommand = new GetPluginInformationCommand(pluginManager);
            OpenPluginCommand.ContinueWith += (sender, data) =>
            {
                if (data is PluginInformationArg arg)
                {
                    PanelVisible = true;
                    PluginInformation = arg.Information;
                }
            };
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
    }
}
