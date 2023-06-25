using Avalonia.Data.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.ViewModels;
internal class PluginMetaDataViewModel : ObservableObject
{
    private readonly PluginMetaData metaData;

    public string DisplayName => metaData.Information.Name;

    public Type UpdaterType => metaData.Type;

    public PluginMetaDataViewModel(PluginMetaData metaData)
    {
        this.metaData = metaData;
    }
}
