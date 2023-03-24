using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;
using XmlFormatterOsIndependent.DependencyInjection;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            WeakReferenceMessenger.Default.Register<GetOsPlatformMessage>(this, (_, e) =>
            {
                if (e.HasReceivedResponse)
                {
                    return;
                }

                if (OperatingSystem.IsWindows())
                {
                    e.Reply(OperationSystemEnum.Windows);
                    return;
                }
                if (OperatingSystem.IsLinux())
                {
                    e.Reply(OperationSystemEnum.Linux);
                    return;
                }
                if (OperatingSystem.IsMacOS())
                {
                    e.Reply(OperationSystemEnum.MacOS);
                    return;
                }
                e.Reply(OperationSystemEnum.Unknown);

            });
        }

        private IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection().AddPluginFramwork()
                                          .AddServices()
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
