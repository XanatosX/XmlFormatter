using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Model.Messages;

public partial class AppViewModel : ObservableObject
{
        /// <summary>
    /// The theme variant to use
    /// </summary>
    [ObservableProperty]
    private ThemeVariant themeVariant;

    public AppViewModel()
    {
        themeVariant = ThemeVariant.Light;

        WeakReferenceMessenger.Default.Register<GetCurrentThemeMessage>(this, (_, e) => {
            if (e.HasReceivedResponse)
            {
                return;
            }
            e.Reply(ThemeVariant);
        });

        WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, e) => {
            ThemeVariant = e.Value;
        });
    }

    ~AppViewModel()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}