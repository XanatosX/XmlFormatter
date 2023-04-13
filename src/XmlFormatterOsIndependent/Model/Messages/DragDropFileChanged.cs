using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to tell the application that a file was dropped on the app and should be updated
/// </summary>
internal class DragDropFileChanged : ValueChangedMessage<string?>
{
    public DragDropFileChanged(string? value) : base(value)
    {
    }
}
