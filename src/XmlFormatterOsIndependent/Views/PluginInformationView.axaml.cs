using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace XmlFormatterOsIndependent.Views;
public partial class PluginInformationView : UserControl
{
    public PluginInformationView()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
