using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public partial class SettingsWindow : CustomWindowBarWindow
    {
        public SettingsWindow() : this(null)
        {
        }

        public SettingsWindow(SettingsWindowViewModel? settingsWindowView)
        {
            InitializeComponent();
            DataContext = settingsWindowView;
        }

    }
}
