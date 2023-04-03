using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to check if a file is valid which was providet via drag and drop
/// </summary>
internal class IsDragDropFileValidMessage : RequestMessage<bool>
{
    public string? FileName { get; init; }
}
