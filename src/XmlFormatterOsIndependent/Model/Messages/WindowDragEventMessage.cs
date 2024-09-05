using Avalonia;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Event message to signal that the window has been dragged
/// </summary>
public class WindowDragEventMessage : ValueChangedMessage<PixelPoint>
{
    /// <summary>
    /// The id of the window to change the state of
    /// </summary>
    public int WindowId {get;}

    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="deltaUpdate">The delta update to the last position</param>
    /// <param name="windowId">The id of the window to move</param>
    public WindowDragEventMessage(PixelPoint deltaUpdate, int windowId) : base(deltaUpdate)
    {
        this.WindowId = windowId;
    }
}