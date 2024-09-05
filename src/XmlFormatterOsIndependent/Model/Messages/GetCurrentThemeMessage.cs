using Avalonia.Styling;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to get the current theme message
/// </summary>
public class GetCurrentThemeMessage : RequestMessage<ThemeVariant>
{
    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    public GetCurrentThemeMessage()
    {
        
    }
}
