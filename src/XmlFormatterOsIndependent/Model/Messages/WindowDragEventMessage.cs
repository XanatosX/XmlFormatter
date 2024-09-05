using Avalonia;
using CommunityToolkit.Mvvm.Messaging.Messages;

public class WindowDragEventMessage : ValueChangedMessage<PixelPoint>
{
    public int WindowId {get;}

    public WindowDragEventMessage(PixelPoint value, int windowId) : base(value)
    {
        this.WindowId = windowId;
    }
}