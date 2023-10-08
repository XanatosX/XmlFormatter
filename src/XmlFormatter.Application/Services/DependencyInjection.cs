using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatter.Application.Services;

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
        return collection;
    }
}
