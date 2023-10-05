using CommunityToolkit.Mvvm.Messaging.Messages;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.Model.Messages;

internal class SettingsImportedMessage : ValueChangedMessage<ApplicationSettings>
{
    internal SettingsImportedMessage(ApplicationSettings value) : base(value)
    {
    }
}