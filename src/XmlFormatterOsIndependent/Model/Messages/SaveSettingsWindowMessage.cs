using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;
internal class SaveSettingsWindowMessage : ValueChangedMessage<bool>
{
    public SaveSettingsWindowMessage(bool value) : base(value)
    {
    }
}
