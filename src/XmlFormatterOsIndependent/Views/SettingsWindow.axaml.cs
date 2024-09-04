using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow() : this(null)
        {
        }

        public SettingsWindow(SettingsWindowViewModel? settingsWindowView)
        {
            this.InitializeComponent();
            DataContext = settingsWindowView;

            
            WeakReferenceMessenger.Default.Register<WindowDeltaDragEvent>(this, (_, e) =>
            {
                if (DataContext is IWindowWithId windowWithId)
                {
                    if (e.windowId == windowWithId.WindowId)
                    {
                        Position = new PixelPoint(Position.X + e.Value.X, Position.Y + e.Value.Y);
                    }
                }
            });
        }

    }
}
