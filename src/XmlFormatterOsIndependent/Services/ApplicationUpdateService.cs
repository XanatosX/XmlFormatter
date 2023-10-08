using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using System;
using XmlFormatter.Application;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service used to provide methods for updating the application
/// </summary>
internal class ApplicationUpdateService
{
    /// <summary>
    /// The settings repository used to request application settings
    /// </summary>
    private readonly ISettingsRepository<ApplicationSettings> settingsRepository;


    /// <summary>
    /// The plugin manager used to request the update strategy
    /// </summary>
    private readonly IPluginManager pluginManager;

    /// <summary>
    /// Create a new instance of the application update service
    /// </summary>
    /// <param name="settingsManager">The settings manager used for getting the application settings</param>
    /// <param name="pluginManager">The plugin manager used for working with the update plugins</param>
    /// <param name="pathService">The path service used to check the settings file</param>
    public ApplicationUpdateService(ISettingsRepository<ApplicationSettings> settingsRepository,
                                    IPluginManager pluginManager)
    {
        this.settingsRepository = settingsRepository;
        this.pluginManager = pluginManager;
    }

    /// <summary>
    /// Get the meta data for the update strategy which should be used
    /// </summary>
    /// <returns>The meta data or null if nothing was found</returns>
    public PluginMetaData? GetUpdateStrategyMetaData()
    {
        return settingsRepository.CreateOrLoad()?.Updater ?? null;
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
        var strategy = GetUpdateStrategy();
        return strategy?.Update(versionCompare, GetFilterForUpdates()) ?? false;
    }
}
