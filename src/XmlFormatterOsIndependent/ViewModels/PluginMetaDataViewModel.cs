using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;
using System;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// View model for a single plugin
/// </summary>
internal class PluginMetaDataViewModel : ObservableObject
{
    /// <summary>
    /// The plugin meta data
    /// </summary>
    public PluginMetaData MetaData { get; }

    /// <summary>
    /// The display name for the given entry
    /// </summary>
    public string DisplayName => MetaData.Information.Name;

    /// <summary>
    /// The type of the updater to use
    /// </summary>
    public Type UpdaterType => MetaData.Type;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="metaData">The meta data to use as a base data set</param>
    public PluginMetaDataViewModel(PluginMetaData metaData)
    {
        MetaData = metaData;
    }
}
