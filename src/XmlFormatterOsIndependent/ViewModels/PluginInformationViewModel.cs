using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;

namespace XmlFormatterOsIndependent.ViewModels;
internal partial class PluginInformationViewModel : ObservableObject
{
    [ObservableProperty]
    private string author;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string version;

    [ObservableProperty]
    private string description;

    public PluginInformationViewModel(PluginInformation pluginInformation)
    {
        Author = pluginInformation.Author;
        Name = pluginInformation.Name;
        Version = pluginInformation.Version.ToString();
        Description = pluginInformation.Description;
    }
}
