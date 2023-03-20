using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using XmlFormatterOsIndependent.DependencyInjection;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection().AddServices()
                                          .AddViews()
                                          .AddViewModels();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var provider = CreateServiceCollection().BuildServiceProvider();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = provider.GetRequiredService<MainWindow>();
                mainWindow.DataContext = provider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
