using System;
using System.Diagnostics;
using XmlFormatter.Application.Services;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Adapter class which uses the HyperText Avalonia nuget to open links
/// </summary>
internal class HyperlinkAdapterUrlService : IUrlService
{
    /// <inheritdoc>/>
    public bool IsValidUrl(string? url)
    {
        //@Note Copied from https://github.com/AvaloniaUtils/HyperText.Avalonia/blob/master/HyperText.Avalonia/Extensions/OpenUrl.cs#L9
        if (string.IsNullOrWhiteSpace(url)) return false;
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) return false;
        if (!Uri.TryCreate(url, UriKind.Absolute, out var tmp)) return false;
        return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
    }

    /// <inheritdoc>/>
    public void OpenUrl(string? url)
    {
        if (url is null)
        {
            return;
        }
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url
            };
            _ = Process.Start(processStartInfo);
        }
        catch (Exception)
        {
        }
        return;
    }
}
