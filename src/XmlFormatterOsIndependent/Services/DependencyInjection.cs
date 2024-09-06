using Microsoft.Extensions.DependencyInjection;
using XmlFormatter.Application.Services;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjection
{

    /// <summary>
    /// Add all the service to the dependency collection
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>An extended collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<IVersionReceiverStrategy, LocalVersionReceiverStrategy>()
                         .AddSingleton<IWindowApplicationService, WindowApplicationService>()
                         .AddSingleton<IDependencyInjectionResolverService, DependencyInjectionResolverService>(provider => new DependencyInjectionResolverService(provider))
                         .AddSingleton<IUrlService, HyperlinkAdapterUrlService>()
                         .AddSingleton<IThemeService, ThemeService>()
                         .AddSingleton<ApplicationUpdateService>()
                         .AddSingleton<ResourceLoaderService>();
    }
}
