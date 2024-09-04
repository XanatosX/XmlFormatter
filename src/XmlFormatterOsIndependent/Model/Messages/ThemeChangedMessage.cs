using Avalonia.Styling;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages
{
    public class ThemeChangedMessage : ValueChangedMessage<ThemeVariant>
    {
        public ThemeChangedMessage(ThemeVariant value) : base(value)
        {
        }
    }
}