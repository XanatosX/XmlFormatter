using HyperText.Avalonia.Extensions;
using System;

namespace XmlFormatterOsIndependent.Services;
internal class HyperlinkAdapterUrlService : IUrlService
{
    public bool IsValidUrl(string url)
    {
        //@Note Copied from https://github.com/AvaloniaUtils/HyperText.Avalonia/blob/master/HyperText.Avalonia/Extensions/OpenUrl.cs#L9
        if (string.IsNullOrWhiteSpace(url)) return false;
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) return false;
        if (!Uri.TryCreate(url, UriKind.Absolute, out var tmp)) return false;
        return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
    }

    public void OpenUrl(string url)
    {
        //@Note Using HyperText Avalonia https://github.com/AvaloniaUtils/HyperText.Avalonia
        url.OpenUrl();
    }
}
