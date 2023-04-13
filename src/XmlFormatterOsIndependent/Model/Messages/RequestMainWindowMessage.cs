using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to request the current main window of the application
/// </summary>
internal class RequestMainWindowMessage : AsyncRequestMessage<Window?>
{
}
