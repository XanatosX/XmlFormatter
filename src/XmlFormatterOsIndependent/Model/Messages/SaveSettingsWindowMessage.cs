using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to trigger a setting save
/// </summary>
internal class SaveSettingsWindowMessage : ValueChangedMessage<bool>
{
    public SaveSettingsWindowMessage(bool value) : base(value)
    {
    }
}
