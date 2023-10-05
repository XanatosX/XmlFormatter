using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message if a new setting version was imported
/// </summary>
internal class SettingsImportedMessage : ValueChangedMessage<ApplicationSettings>
{
    internal SettingsImportedMessage(ApplicationSettings value) : base(value)
    {
    }
}