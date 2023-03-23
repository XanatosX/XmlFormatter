using System.Collections.Generic;

namespace XmlFormatterOsIndependent.Services;
public interface IDependecyInjectionResolverService
{
    T? GetService<T>() where T : class;
    IEnumerable<T> GetServices<T>() where T : class;
}