using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Reactive;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal the application that it should be closing
/// </summary>
internal class CloseApplicationMessage : RequestMessage<Unit>
{
}
