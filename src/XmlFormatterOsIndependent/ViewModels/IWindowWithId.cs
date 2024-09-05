namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Interface for a window with an id
/// </summary>
public interface IWindowWithId
{
    /// <summary>
    /// The unique id of this window
    /// </summary>
    int WindowId {get; }
}