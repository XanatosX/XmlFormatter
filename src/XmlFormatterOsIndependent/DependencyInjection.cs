using Microsoft.Extensions.DependencyInjection;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.IO;
using XmlFormatter.Infrastructure.Configuration;
using XmlFormatterOsIndependent.Serializer;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    /// Ad the presentation layer for this application
    /// </summary>
    /// <param name="collection">The service collection to edit</param>
    /// <returns>The edited service collection</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection collection)
    {
        collection.AddLogging();
        return collection.AddViews()
                         .AddViewModels()
                         .AddConverters()
                         .AddConfigurations()
                         .AddServices()
                         .AddPluginFramework();
    }

    /// <summary>
    /// Add configuration for some services
    /// </summary>
    /// <param name="collection">The service collection to edit</param>
    /// <returns>The edited service collection</returns>
    private static IServiceCollection AddConfigurations(this IServiceCollection collection)
    {
        return collection.AddSingleton(_ =>
        {
            string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return new SettingsOptions(Path.Combine(settingsPath, "XmlFormatter"), "settings.json");
        });
    }

    /// <summary>
    /// Add all the plugins required from the framework
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    private static IServiceCollection AddPluginFramework(this IServiceCollection collection)
    {
        return collection.AddSingleton<IPluginManager, DefaultManager>()
                         .AddSingleton<IPluginLoadStrategy, PluginFolder>();
    }
}
