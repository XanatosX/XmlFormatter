using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace XmlFormatterOsIndependent.Views;
public partial class ApplicationSettingsView : UserControl
{
    public ApplicationSettingsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
