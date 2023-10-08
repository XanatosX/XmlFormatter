using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatterOsIndependent.Views;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjection
{
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
}
