using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to get the current operation system
/// </summary>
internal class GetOsPlatformMessage : RequestMessage<OperationSystemEnum>
{
}
