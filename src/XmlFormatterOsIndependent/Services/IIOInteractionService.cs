namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service to provider methods for interactions with the io of the system
/// </summary>
public interface IIOInteractionService
{
    /// <summary>
    /// Open a website in the default browser
    /// </summary>
    /// <param name="url">The url to open</param>
    /// <returns>True if the action was successful</returns>
    public bool OpenWebsite(string url);
}
