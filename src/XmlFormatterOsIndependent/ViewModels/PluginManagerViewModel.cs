﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using System.Collections.Generic;
using System.Linq;
using XmlFormatter.Application.Services;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;
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
        /// The view to display the current plugin information
        /// </summary>
        [ObservableProperty]
        public ObservableObject? visibleView;
        private readonly IUrlService urlService;

        /// <summary>
        /// The groups for the plugins to be shown in the tree view
        /// </summary>
        public List<PluginTreeViewGroup> PluginGroups { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="viewContainer">The container for the parent window and the current window</param>
        /// <param name="managerFactory">The factory to create the plugin manager</param>
        public PluginManagerViewModel(IPluginManager pluginManager, IUrlService urlService) //ViewContainer viewContainer, 
        {
            this.urlService = urlService;

            PanelVisible = false;
            PluginGroups = new List<PluginTreeViewGroup>();
            PluginTreeViewGroup formatterGroup = new PluginTreeViewGroup("Formatter");
            PluginTreeViewGroup updaterGroup = new PluginTreeViewGroup("Updater");
            List<PluginMetaData> formatters = pluginManager.ListPlugins<IFormatter>().ToList();
            List<PluginMetaData> updaters = pluginManager.ListPlugins<IUpdateStrategy>().ToList();

            foreach (PluginMetaData formatter in formatters)
            {
                formatterGroup.Add(new PluginTreeViewItem(formatter, Enums.PluginType.Formatter));
            }
            foreach (PluginMetaData updater in updaters)
            {
                updaterGroup.Add(new PluginTreeViewItem(updater, Enums.PluginType.Updater));
            }

            PluginGroups.Add(formatterGroup);
            PluginGroups.Add(updaterGroup);
        }

        /// <summary>
        /// Method to open a given plugin
        /// </summary>
        /// <param name="pluginInformation">The plugin information to open</param>
        [RelayCommand]
        public void OpenPlugin(PluginInformation pluginInformation)
        {
            PanelVisible = true;
            VisibleView = new PluginInformationViewModel(pluginInformation, urlService);
        }
    }
}
