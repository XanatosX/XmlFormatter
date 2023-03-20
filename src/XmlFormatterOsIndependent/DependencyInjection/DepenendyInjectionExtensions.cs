using Microsoft.Extensions.DependencyInjection;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<IPluginManager, DefaultManager>()
                         .AddSingleton<ISettingsManager, SettingsManager>()
                         .AddSingleton<IVersionManager, VersionManager>()
                         .AddSingleton<IVersionConvertStrategy, DefaultStringConvertStrategy>()
                         .AddSingleton<IVersionRecieverStrategy, LocalVersionRecieverStrategy>()
                         .AddSingleton<IVersionRecieverStrategy, GitHubVersionRecieverStrategy>()
                         .AddSingleton<IPathService, PathService>()
                         .AddSingleton<IIOInteractionService, DefaultInteractionService>()
                         .AddSingleton<IWindowApplicationService, WindowApplicationService>();

    }

    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindow>();
    }
    public static IServiceCollection AddViewModels(this IServiceCollection collection)
    {
        return collection.AddTransient<MainWindowViewModel>();
    }
}
