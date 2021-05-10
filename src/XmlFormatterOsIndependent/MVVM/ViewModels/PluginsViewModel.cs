using PluginFramework.Interfaces.Manager;
using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.MVVM.Models;
using XmlFormatterModel.Update;
using PluginFramework.Interfaces.PluginTypes;
using XmlFormatterOsIndependent.Enums;
using PluginFramework.DataContainer;
using XmlFormatterOsIndependent.Commands;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class PluginsViewModel : ReactiveObject
    {
        /// <summary>
        /// The groups for the plugins to be shown in the tree view
        /// </summary>
        public List<PluginTreeViewGroup> PluginGroups { get; }

        public ITriggerCommand OpenPluginCommand { get; }

        public PluginInformation CurrentPluginData
        {
            get => currentPluginData;
            private set
            {
                this.RaiseAndSetIfChanged(ref currentPluginData, value);
            }
        }
        private PluginInformation currentPluginData;


        private DefaultManagerFactory factory;

        public PluginsViewModel()
        {
            factory = new DefaultManagerFactory();
            IPluginManager pluginManager = factory.GetPluginManager();
            PluginTreeViewGroup formatter = new PluginTreeViewGroup("Formatter");
            PluginTreeViewGroup updater = new PluginTreeViewGroup("Updater");
            formatter.Add(pluginManager.ListPlugins<IFormatter>().Select(data => new PluginTreeViewItem(data, PluginType.Formatter)).ToList());
            updater.Add(pluginManager.ListPlugins<IUpdateStrategy>().Select(data => new PluginTreeViewItem(data, PluginType.Updater)).ToList());
            PluginGroups = new List<PluginTreeViewGroup>()
            {
                formatter,
                updater
            };
            OpenPluginCommand = new RelayCommand(
                (parameter) => parameter is PluginInformation,
                (parameter) =>
                {
                    if (parameter is PluginInformation information)
                    {
                        CurrentPluginData = information;
                    }
                }
            );
            

        }
    }
}
