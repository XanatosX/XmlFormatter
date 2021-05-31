using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.MVVM.ViewModels.Main;

namespace XmlFormatterOsIndependent.MVVM.Windows
{
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SetContent(object content)
        {
            if (DataContext is PopupWindowViewModel popupWindowView)
            {
                popupWindowView.SetContent(content);
                popupWindowView.SetWindow(this);
            }
        }
    }
}
