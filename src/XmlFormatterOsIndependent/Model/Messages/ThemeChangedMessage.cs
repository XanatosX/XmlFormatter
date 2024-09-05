using Avalonia.Styling;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to signal that the theme hast been changed
/// </summary>
public class ThemeChangedMessage : ValueChangedMessage<ThemeVariant>
{
    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="newThemeVariant">The new theme variant to use</param>
    public ThemeChangedMessage(ThemeVariant newThemeVariant) : base(newThemeVariant)
    {
    }
}
