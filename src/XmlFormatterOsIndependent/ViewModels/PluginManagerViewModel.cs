using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using System.Collections.Generic;
using System.Linq;
using XmlFormatter.Application.Services;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Window to list all the plugin data
    /// </summary>
    internal partial class PluginManagerViewModel : ObservableObject, IWindowBar
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
        private readonly IThemeService themeService;


        /// <summary>
        /// The groups for the plugins to be shown in the tree view
        /// </summary>
        public List<PluginTreeViewGroup> PluginGroups { get; }

        [ObservableProperty]
        private IWindowBar windowBar;

        
        [ObservableProperty]
        private Color themeColor;

        public int WindowId => WindowBar is IWindowWithId bar ? bar.WindowId : -1;


        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="viewContainer">The container for the parent window and the current window</param>
        /// <param name="managerFactory">The factory to create the plugin manager</param>
        public PluginManagerViewModel(IPluginManager pluginManager,
                                      IUrlService urlService,
                                      IWindowApplicationService windowApplicationService,
                                      IThemeService themeService) //ViewContainer viewContainer, 
        {
            this.windowBar = windowApplicationService.GetWindowBar(Properties.Properties.Default_Window_Icon, Properties.Resources.PluginManagerWindow_Title);
            this.urlService = urlService;
            this.themeService = themeService;
            
            var themeResponse = WeakReferenceMessenger.Default.Send<GetCurrentThemeMessage>();
            var theme = themeResponse.Response;
            SetThemeColor(theme);

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

            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, data) =>
            {
                SetThemeColor(data.Value);
            });
        }

        private void SetThemeColor(ThemeVariant theme)
        {
            ThemeColor = themeService.GetColorForTheme(theme);
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
