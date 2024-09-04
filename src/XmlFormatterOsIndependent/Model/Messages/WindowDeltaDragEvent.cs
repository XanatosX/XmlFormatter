using Avalonia;
using CommunityToolkit.Mvvm.Messaging.Messages;

public class WindowDeltaDragEvent : ValueChangedMessage<PixelPoint>
{
    public int windowId {get;}

    public WindowDeltaDragEvent(PixelPoint value, int windowId) : base(value)
    {
        this.windowId = windowId;
    }
}