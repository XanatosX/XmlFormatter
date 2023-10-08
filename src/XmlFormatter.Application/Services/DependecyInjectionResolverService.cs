using Microsoft.Extensions.DependencyInjection;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatter.Application.Services;

/// <summary>
/// Service to resolve dependencies
/// </summary>
public class DependencyInjectionResolverService : IDependencyInjectionResolverService
{
    /// <summary>
    /// THe internal service provider 
    /// </summary>
    private readonly IServiceProvider provider;

    /// <summary>
    /// Create a new instance of the service
    /// </summary>
    /// <param name="provider">The internal provider used for the requests</param>
    public DependencyInjectionResolverService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    /// <inheritdoc/>
    public T? GetService<T>() where T : class
    {
        return provider.GetService<T>();
    }

    /// <inheritdoc/>
    public IEnumerable<T> GetServices<T>() where T : class
    {
        return provider.GetServices<T>();
    }
}
