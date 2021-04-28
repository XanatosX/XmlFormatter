using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Models;

namespace XmlFormatterOsIndependent.ViewModels
{
    class PluginManagerViewModel : ViewModelBase
    {
        public bool PanelVisible
        {
            get => panelVisible;
            private set => this.RaiseAndSetIfChanged(ref panelVisible, value);
        }
        private bool panelVisible;

        public PluginInformation PluginInformation 
        { 
            get => pluginInformation; 
            private set => this.RaiseAndSetIfChanged(ref pluginInformation, value); 
        }
        private PluginInformation pluginInformation;

        public List<PluginTreeViewGroup> PluginGroups { get; }

        public ITriggerCommand OpenPluginCommand { get; }

        public PluginManagerViewModel(ViewContainer viewContainer, DefaultManagerFactory managerFactory)
            : base(viewContainer, managerFactory.GetSettingsManager(), managerFactory.GetPluginManager())
        {
            PanelVisible = false;
            OpenPluginCommand = new OpenPluginCommand(pluginManager);
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
            List<PluginMetaData> formatters = pluginManager.ListPlugins<IFormatter>();
            foreach (PluginMetaData formatter in formatters)
            {
                formatterGroup.Add(new PluginTreeViewItem(formatter, Enums.PluginType.Formatter));
            }
            List<PluginMetaData> updaters = pluginManager.ListPlugins<IUpdateStrategy>();
            foreach (PluginMetaData updater in updaters)
            {
                updaterGroup.Add(new PluginTreeViewItem(updater, Enums.PluginType.Updater));
            }
            PluginGroups.Add(formatterGroup);
            PluginGroups.Add(updaterGroup);
        }
    }
}
