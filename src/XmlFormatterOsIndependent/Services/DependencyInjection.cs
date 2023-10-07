using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;
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
        return collection.AddSingleton<IVersionManager, VersionManager>(provider =>
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
                         .AddSingleton<IUrlService, HyperlinkAdapterUrlService>()
                         .AddSingleton<IThemeService, ThemeService>()
                         .AddSingleton<ApplicationUpdateService>();
    }
}
