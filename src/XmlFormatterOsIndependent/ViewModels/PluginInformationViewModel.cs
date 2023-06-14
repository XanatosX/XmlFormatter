using CommunityToolkit.Mvvm.ComponentModel;
using HyperText.Avalonia.Extensions;
using PluginFramework.DataContainer;
using System;
using XmlFormatterOsIndependent.Services;

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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProjectUrlPresent))]
    private string projectUrl;
    public bool ProjectUrlPresent => IsValidUrl(ProjectUrl);

    /// <summary>
    /// The author of the plugin
    /// </summary>
    [ObservableProperty]
    private string author;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AuthorLinkPresent))]
    public string authorUrl;

    public bool AuthorLinkPresent => IsValidUrl(AuthorUrl);

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
    private readonly IUrlService urlService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginInformation">The plugin information to use</param>
    public PluginInformationViewModel(PluginInformation pluginInformation, IUrlService urlService)
    {
        this.urlService = urlService;

        Name = pluginInformation.Name;
        Author = pluginInformation.Author;
        string major = ConvertToVersion(pluginInformation.Version.Major);
        string minor = ConvertToVersion(pluginInformation.Version.Minor);
        string build = ConvertToVersion(pluginInformation.Version.Build);
        Version = $"{major}.{minor}.{build}";
        Description = pluginInformation.MarkdownDescription;
        AuthorUrl = pluginInformation.AuthorUrl;
        ProjectUrl = pluginInformation.ProjectUrl;
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

    /// <summary>
    /// Check if a given url is valid
    /// </summary>
    /// <param name="url">The url to check</param>
    /// <returns>True if the url is valid</returns>
    private bool IsValidUrl(string url)
    {
        return urlService.IsValidUrl(url);
    }
}
