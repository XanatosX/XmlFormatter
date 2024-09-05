using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    internal partial class AboutWindow : CustomWindowBarWindow
    {
        public AboutWindow() : this(null)
        {
        }

        public AboutWindow(AboutWindowViewModel? aboutWindowView)
        {
            this.InitializeComponent();
            DataContext = aboutWindowView;
        }
    }
}
