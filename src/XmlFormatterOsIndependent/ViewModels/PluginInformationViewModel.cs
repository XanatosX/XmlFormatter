using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Model to display plugin information
/// </summary>
internal partial class PluginInformationViewModel : ObservableObject
{
    /// <summary>
    /// THe name of the plugin
    /// </summary>
    [ObservableProperty]
    private string name;

    /// <summary>
    /// The author of the plugin
    /// </summary>
    [ObservableProperty]
    private string author;

    /// <summary>
    /// The version of the plugin
    /// </summary>
    [ObservableProperty]
    private string version;

    /// <summary>
    /// The description of the plugin
    /// </summary>
    [ObservableProperty]
    private string description;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginInformation">The plugin information to use</param>
    public PluginInformationViewModel(PluginInformation pluginInformation)
    {
        Name = pluginInformation.Name;
        Author = pluginInformation.Author;
        string major = ConvertToVersion(pluginInformation.Version.Major);
        string minor = ConvertToVersion(pluginInformation.Version.Minor);
        string build = ConvertToVersion(pluginInformation.Version.Build);
        Version = $"{major}.{minor}.{build}";
        Description = pluginInformation.Description;
    }

    /// <summary>
    /// Method to convert a given number to a correct version number
    /// </summary>
    /// <param name="number">The number to convert</param>
    /// <returns>A version number between 0 and max int as string</returns>
    private string ConvertToVersion(int number)
    {
        return number < 0 ? "0" : number.ToString();
    }
}
