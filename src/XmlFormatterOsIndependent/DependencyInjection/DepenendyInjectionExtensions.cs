using Microsoft.Extensions.DependencyInjection;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.Update;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.DependencyInjection;
internal static class DepenendyInjectionExtensions
{
    public static IServiceCollection AddPluginFramwork(this IServiceCollection collection)
    {
        return collection.AddSingleton<IPluginManager, DefaultManager>()
                         .AddSingleton<IPluginLoadStrategy, PluginFolder>();
    }

    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<ISettingsManager, SettingsManager>()
                         .AddSingleton<IVersionManager, VersionManager>()
                         .AddSingleton<IVersionConvertStrategy, DefaultStringConvertStrategy>()
                         .AddSingleton<IVersionRecieverStrategy, LocalVersionRecieverStrategy>()
                         .AddSingleton<IVersionRecieverStrategy, GitHubVersionRecieverStrategy>()
                         .AddSingleton<IPathService, PathService>()
                         .AddSingleton<IIOInteractionService, DefaultInteractionService>()
                         .AddSingleton<IWindowApplicationService, WindowApplicationService>()
                         .AddSingleton<IDependecyInjectionResolverService, DependecyInjectionResolverService>(provider => new DependecyInjectionResolverService(provider));
    }

    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindow>()
                         .AddTransient<PluginManagerWindow>()
                         .AddTransient<AboutWindow>()
                         .AddTransient<SettingsWindow>();
    }
    public static IServiceCollection AddViewModels(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindowViewModel>();
    }
}
