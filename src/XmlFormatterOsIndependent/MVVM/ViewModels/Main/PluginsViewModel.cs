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

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Main
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

        private bool dataIsAvailable;

        public bool DataIsAvailable
        {
            get => dataIsAvailable;
            set => this.RaiseAndSetIfChanged(ref dataIsAvailable, value);
        }

        public PluginsViewModel()
        {
            IPluginManager pluginManager = DefaultManagerFactory.GetPluginManager();
            PluginTreeViewGroup formatter = new PluginTreeViewGroup("Formatter");
            PluginTreeViewGroup updater = new PluginTreeViewGroup("Updater");
            formatter.Add(pluginManager.ListPlugins<IFormatter>().Select(data => new PluginTreeViewItem(data, PluginType.Formatter)).ToList());
            updater.Add(pluginManager.ListPlugins<IUpdateStrategy>().Select(data => new PluginTreeViewItem(data, PluginType.Updater)).ToList());
            DataIsAvailable = false;
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
                        DataIsAvailable = true;
                    }
                }
            );


        }
    }
}
