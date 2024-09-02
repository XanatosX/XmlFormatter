using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class AppViewModel : ObservableObject
{
        /// <summary>
    /// The theme variant to use
    /// </summary>
    [ObservableProperty]
    private ThemeVariant themeVariant;

    public AppViewModel()
    {
        themeVariant = ThemeVariant.Dark;
    }
}