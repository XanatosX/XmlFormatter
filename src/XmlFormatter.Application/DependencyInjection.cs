using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatter.Application;

/// <summary>
/// Dependency Injection class for application Library
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add the application dependencies to the application
    /// </summary>
    /// <param name="collection">The collection to add tp</param>
    /// <returns>The edited collection</returns>
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        return collection;
    }
}