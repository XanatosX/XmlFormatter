using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Reactive;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal an window that it should be closing
/// </summary>
internal class ChangeWindowState : RequestMessage<Unit>
{
    public int WindowId {get;}
    
    public  WindowState NewState {get;}

    public ChangeWindowState(int windowId, WindowState newState)
    {
        this.WindowId = windowId;
        this.NewState = newState;
    }
}
