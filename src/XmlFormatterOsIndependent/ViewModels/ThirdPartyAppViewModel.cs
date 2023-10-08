using CommunityToolkit.Mvvm.ComponentModel;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// View model for the an single third party app
/// </summary>
internal partial class ThirdPartyAppViewModel : ObservableObject
{
    /// <summary>
    /// The name of the app
    /// </summary>
    [ObservableProperty]
    private string name;

    /// <summary>
    /// The version of the app
    /// </summary>
    [ObservableProperty]
    private string version;

    /// <summary>
    /// The license type of the version
    /// </summary>
    [ObservableProperty]
    private string license;

    /// <summary>
    /// The license url of the app
    /// </summary>
    [ObservableProperty]
    private string? licenseUrl;

    /// <summary>
    /// The url to the third party app
    /// </summary>
    [ObservableProperty]
    private string url;

    /// <summary>
    /// Create a new instance of the view model
    /// </summary>
    /// <param name="appData">The app data to use</param>
    public ThirdPartyAppViewModel(ThirdPartyApp appData)
    {
        Name = appData.Name;
        Version = appData.Version;
        License = appData.License;
        LicenseUrl = appData.LicenseUrl;
        Url = appData.Url;
    }
}