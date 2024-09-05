using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Reactive;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal an window that it should be closing
/// </summary>
internal class ChangeWindowState : RequestMessage<Unit>
{
    /// <summary>
    /// The id of the window to change the state of
    /// </summary>
    public int WindowId {get;}
    
    /// <summary>
    /// The new window state to use
    /// </summary>
    public WindowState NewState {get;}

    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="windowId">The window id to edit</param>
    /// <param name="newState">The new state to set on the window</param>
    public ChangeWindowState(int windowId, WindowState newState)
    {
        this.WindowId = windowId;
        this.NewState = newState;
    }
}
