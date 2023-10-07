using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;
using XmlFormatter.Application;
using XmlFormatter.Infrastructure;
using System.Text.Json;
using XmlFormatterOsIndependent.Serializer;

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

        /// <summary>
        /// Method to create the service collection
        /// Including all the services, views and model views
        /// </summary>
        /// <returns>A useable service collection</returns>
        private IServiceCollection CreateServiceCollection()
        {
            var collection = new ServiceCollection().AddApplication()
                                                    .AddPresentation();

            var provider = collection.BuildServiceProvider();
            
            var options = new JsonSerializerOptions();
            options.Converters.Add(provider.GetRequiredService<PluginMetaDataSerializer>());
            return collection.AddInfrastructure(options);
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
