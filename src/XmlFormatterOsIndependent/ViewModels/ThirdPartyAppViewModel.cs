using CommunityToolkit.Mvvm.ComponentModel;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.ViewModels;

internal partial class ThirdPartyAppViewModel : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string version;

    [ObservableProperty]
    private string license;

    [ObservableProperty]
    private string? licenseUrl;

    
    [ObservableProperty]
    private string url;

    public ThirdPartyAppViewModel(ThirdPartyApp appData)
    {
        Name = appData.Name;
        Version = appData.Version;
        License = appData.License;
        LicenseUrl = appData.LicenseUrl;
        Url = appData.Url;
    }
}