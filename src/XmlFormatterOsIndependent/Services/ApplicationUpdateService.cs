using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service used to provide methods for updating the application
/// </summary>
public class ApplicationUpdateService
{
    /// <summary>
    /// The settings manager used to request application settings
    /// </summary>
    private readonly ISettingsManager settingsManager;

    /// <summary>
    /// The plugin manager used to request the update strategy
    /// </summary>
    private readonly IPluginManager pluginManager;

    /// <summary>
    /// The path service used to request the settings file
    /// </summary>
    private readonly IPathService pathService;

    /// <summary>
    /// Create a new instance of the application update service
    /// </summary>
    /// <param name="settingsManager">The settings manager used for getting the application settings</param>
    /// <param name="pluginManager">The plugin manager used for working with the update plugins</param>
    /// <param name="pathService">The path service used to check the settings file</param>
    public ApplicationUpdateService(ISettingsManager settingsManager, IPluginManager pluginManager, IPathService pathService)
    {
        this.settingsManager = settingsManager;
        this.pluginManager = pluginManager;
        this.pathService = pathService;
    }

    /// <summary>
    /// Get the meta data for the update strategy which should be used
    /// </summary>
    /// <returns>The meta data or null if nothing was found</returns>
    public PluginMetaData? GetUpdateStrategyMetaData()
    {
        string? pluginName = settingsManager.GetScope("Default")?.GetSetting("UpdateStrategy")?.GetValue<string>();
        if (pluginName is null)
        {
            return null;
        }
        var plugin = pluginManager.ListPlugins<IUpdateStrategy>().FirstOrDefault(plugin => plugin.Type.ToString() == pluginName);
        if (plugin is null)
        {
            return null;
        }
        return plugin;
    }

    /// <summary>
    /// The the current update strategy
    /// </summary>
    /// <returns>Returns the update strategy or null if nothing was found</returns>
    public IUpdateStrategy? GetUpdateStrategy()
    {
        return pluginManager.LoadPlugin<IUpdateStrategy>(GetUpdateStrategyMetaData());
    }

    /// <summary>
    /// The the predicate filter for the update strategy
    /// </summary>
    /// <returns>A usable predicate to define if a release asset is valid</returns>
    public Predicate<IReleaseAsset> GetFilterForUpdates()
    {
        var operationSystem = WeakReferenceMessenger.Default.Send(new GetOsPlatformMessage());
        if (operationSystem is null || operationSystem == OperationSystemEnum.Unknown)
        {
            return _ => false;
        }
        return operationSystem.Response switch
        {
            OperationSystemEnum.Windows => asset => asset.Name.Contains(Properties.Properties.Asset_WindowsFilter),
            OperationSystemEnum.Linux => asset => asset.Name.Contains(Properties.Properties.Asset_LinuxFilter),
            OperationSystemEnum.MacOS => asset => asset.Name.Contains(Properties.Properties.Asset_MacOsFilter),
            _ => _ => false
        };
    }

    /// <summary>
    /// Update the application if a new version is available
    /// </summary>
    /// <param name="versionCompare">The current information about the local on remote version</param>
    /// <returns>True if the update was successful</returns>
    public bool UpdateApplication(VersionCompare versionCompare)
    {
        settingsManager.Load(pathService.GetSettingsFile());
        var strategy = GetUpdateStrategy();
        return strategy?.Update(versionCompare, GetFilterForUpdates()) ?? false;
    }
}
