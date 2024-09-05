using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to trigger closing settings window,
/// If the value is true the window should save the content otherwise just close it
/// </summary>
internal class SettingsWindowClosingMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="shouldSaveSettings">Should all the settings been saved?</param>
    public SettingsWindowClosingMessage(bool shouldSaveSettings) : base(shouldSaveSettings)
    {
    }
}
