using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Reactive;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal an window that it should be closing
/// </summary>
internal class CloseWindowMessage : RequestMessage<Unit>
{
    public int WindowId {get;}

    public CloseWindowMessage(int windowId)
    {
        this.WindowId = windowId;
    }
}
