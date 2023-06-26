using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;
using System;

namespace XmlFormatterOsIndependent.ViewModels;
internal class PluginMetaDataViewModel : ObservableObject
{
    public PluginMetaData MetaData { get; }

    public string DisplayName => MetaData.Information.Name;

    public Type UpdaterType => MetaData.Type;

    public PluginMetaDataViewModel(PluginMetaData metaData)
    {
        MetaData = metaData;
    }
}
