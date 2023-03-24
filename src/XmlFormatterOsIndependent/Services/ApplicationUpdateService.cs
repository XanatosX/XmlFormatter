using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;
public class ApplicationUpdateService
{
    private readonly ISettingsManager settingsManager;
    private readonly IPluginManager pluginManager;
    private readonly IPathService pathService;

    public ApplicationUpdateService(ISettingsManager settingsManager, IPluginManager pluginManager, IPathService pathService)
    {
        this.settingsManager = settingsManager;
        this.pluginManager = pluginManager;
        this.pathService = pathService;
    }

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

    public IUpdateStrategy? GetUpdateStrategy()
    {
        return pluginManager.LoadPlugin<IUpdateStrategy>(GetUpdateStrategyMetaData());
    }

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

    public bool UpdateApplication(VersionCompare versionCompare)
    {
        settingsManager.Load(pathService.GetSettingsFile());
        var strategy = GetUpdateStrategy();
        return strategy?.Update(versionCompare, GetFilterForUpdates()) ?? false;
    }
}
