using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;
internal class IsDragDropFileValidMessage : RequestMessage<bool>
{
    public string? FileName { get; init; }
}
