using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        return collection;
    }
}