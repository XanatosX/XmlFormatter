using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjection
{
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
                         .AddTransient<ApplicationSettingsBackupViewModel>()
                         .AddTransient<ThirdPartyAppViewModel>();

    }
}
