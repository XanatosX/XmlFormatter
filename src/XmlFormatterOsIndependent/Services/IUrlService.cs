namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service interface to validate and open links in the default browser
/// </summary>
public interface IUrlService
{
    /// <summary>
    /// Is the provided string a valid url
    /// </summary>
    /// <param name="url">The url which should be checked</param>
    /// <returns>True if the url is a valid one</returns>
    bool IsValidUrl(string url);

    /// <summary>
    /// Open the provided url in the default browser, this method will only open valid ones
    /// </summary>
    /// <param name="url">The url to open in the browser</param>
    void OpenUrl(string url);
}
