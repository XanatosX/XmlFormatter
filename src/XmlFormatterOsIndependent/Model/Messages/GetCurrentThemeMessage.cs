using Avalonia.Styling;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages
{
    public class GetCurrentThemeMessage : RequestMessage<ThemeVariant>
    {
        public GetCurrentThemeMessage()
        {
            
        }
    }
}