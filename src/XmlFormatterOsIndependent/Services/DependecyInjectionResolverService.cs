using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Services;
public class DependecyInjectionResolverService : IDependecyInjectionResolverService
{
    private readonly IServiceProvider provider;

    public DependecyInjectionResolverService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public T? GetService<T>() where T : class
    {
        return provider.GetService<T>();
    }

    public IEnumerable<T> GetServices<T>() where T : class
    {
        return provider.GetServices<T>();
    }
}
