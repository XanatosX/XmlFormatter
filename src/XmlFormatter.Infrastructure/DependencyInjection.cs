using Microsoft.Extensions.DependencyInjection;
using XmlFormatter.Application;
using XmlFormatter.Infrastructure.Repositories;

namespace XmlFormatter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        return collection.AddTransient(typeof(ISettingsRepository<>), typeof(JsonSettingRepository<>));
    }
}