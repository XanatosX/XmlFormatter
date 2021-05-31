using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Windows.Input;
using XmlFormatterOsIndependent.Commands;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Bars
{
    public class TitleBarViewModel
    {
        public ICommand CloseWindow { get; }

        public ICommand MinimizeWindow { get; }

        public TitleBarViewModel()
        {
            CloseWindow = new RelayCommand(
                (parameter) =>
                {
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        desktop.MainWindow.Close();
                    }
                });

            MinimizeWindow = new RelayCommand(
                paramter =>
                {
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        desktop.MainWindow.WindowState = WindowState.Minimized;
                    }
                });
        }
    }
}
