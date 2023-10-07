using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using XmlFormatter.Application;
using XmlFormatter.Infrastructure.Repositories;

namespace XmlFormatter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection, JsonSerializerOptions? options)
    {
        return collection.AddSingleton(_ => options ?? new())
                         .AddTransient(typeof(ISettingsRepository<>), typeof(JsonSettingRepository<>));
    }
}