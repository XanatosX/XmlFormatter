using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace XmlFormatterOsIndependent.Views;
public partial class ThirdPartyAppView : UserControl
{
    public ThirdPartyAppView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
