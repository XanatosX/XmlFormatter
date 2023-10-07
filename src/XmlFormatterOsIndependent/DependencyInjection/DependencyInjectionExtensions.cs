using Microsoft.Extensions.DependencyInjection;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.IO;
using System.Linq;
using XmlFormatter.Infrastructure.Configuration;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;
using XMLFormatterModel.Setting.InputOutput;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.Update;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.DependencyInjection;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection collection)
    {
        return collection.AddSingleton(_ => {
            string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return new SettingsOptions(Path.Combine(settingsPath, "XmlFormatter"), "settings.json");
        });
    }

    /// <summary>
    /// Add all the plugins required from the framework
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    public static IServiceCollection AddPluginFramework(this IServiceCollection collection)
    {
        return collection.AddSingleton<IPluginManager, DefaultManager>()
                         .AddSingleton<IPluginLoadStrategy, PluginFolder>();
    }

    /// <summary>
    /// Add all the service to the dependency collection
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<ISettingsManager, SettingsManager>()
                         .AddSingleton<IVersionManager, VersionManager>(provider =>
                         {
                             var dataSet = provider.GetServices<IVersionReceiverStrategy>();
                             IVersionReceiverStrategy? localVersion = dataSet.FirstOrDefault(data => data is LocalVersionReceiverStrategy);
                             IVersionReceiverStrategy? remoteVersion = dataSet.FirstOrDefault(data => data is GitHubVersionReceiverStrategy); ;
                             return new VersionManager(provider.GetRequiredService<IVersionConvertStrategy>(), localVersion, remoteVersion);
                         })
                         .AddSingleton<IVersionConvertStrategy, DefaultStringConvertStrategy>()
                         .AddSingleton<IVersionReceiverStrategy, LocalVersionReceiverStrategy>()
                         .AddSingleton<IVersionReceiverStrategy, GitHubVersionReceiverStrategy>()
                         .AddSingleton<IPathService, PathService>()
                         .AddSingleton<IIOInteractionService, DefaultInteractionService>()
                         .AddSingleton<IWindowApplicationService, WindowApplicationService>()
                         .AddSingleton<IDependencyInjectionResolverService, DependencyInjectionResolverService>(provider => new DependencyInjectionResolverService(provider))
                         .AddSingleton<IPersistentFactory, XmlProviderFactory>()
                         .AddSingleton<ISettingLoadProvider, XmlLoaderProvider>()
                         .AddSingleton<ISettingSaveProvider, XmlSaverProvider>()
                         .AddSingleton<IUrlService, HyperlinkAdapterUrlService>()
                         .AddSingleton<IThemeService, ThemeService>()
                         .AddSingleton<ApplicationUpdateService>();
    }

    /// <summary>
    /// Add the views to the dependency collection
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindow>()
                         .AddTransient<PluginManagerWindow>()
                         .AddTransient<AboutWindow>()
                         .AddTransient<SettingsWindow>()
                         .AddTransient<ApplicationSettingsView>()
                         .AddTransient<ApplicationSettingsBackupView>();
    }

    /// <summary>
    /// Add the view models to the dependency collection
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    public static IServiceCollection AddViewModels(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindowViewModel>()
                         .AddTransient<PluginManagerViewModel>()
                         .AddTransient<AboutWindowViewModel>()
                         .AddTransient<SettingsWindowViewModel>()
                         .AddTransient<ApplicationSettingsViewModel>()
                         .AddTransient<ApplicationSettingsBackupViewModel>();
    }
}
