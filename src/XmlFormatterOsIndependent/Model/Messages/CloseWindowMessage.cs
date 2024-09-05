using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Reactive;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal an window that it should be closing
/// </summary>
internal class CloseWindowMessage : RequestMessage<Unit>
{
    /// <summary>
    /// The id of the window to change the state of
    /// </summary>
    public int WindowId {get;}

    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="windowId">The window id to close</param>
    public CloseWindowMessage(int windowId)
    {
        this.WindowId = windowId;
    }
}
