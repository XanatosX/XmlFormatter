using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using XmlFormatter.Application;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Infrastructure.Repositories;
using XmlFormatter.Infrastructure.Services.UpdateFeature;
using XmlFormatter.Infrastructure.Services.UpdaterFeature.Strategy;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection, JsonSerializerOptions? options)
    {
        return collection.AddSingleton(_ => options ?? new())
                         .AddTransient(typeof(ISettingsRepository<>), typeof(JsonSettingRepository<>)).AddSingleton<IVersionManager, VersionManager>(provider =>
                         {
                             var dataSet = provider.GetServices<IVersionReceiverStrategy>();
                             IVersionReceiverStrategy? localVersion = dataSet.FirstOrDefault(data => data.Scope == ScopeEnum.Local);
                             IVersionReceiverStrategy? remoteVersion = dataSet.FirstOrDefault(data => data.Scope == ScopeEnum.Remote);
                             return new VersionManager(provider.GetRequiredService<IVersionConvertStrategy>(), localVersion, remoteVersion);
                         })
                         .AddSingleton<IVersionConvertStrategy, DefaultStringConvertStrategy>()
                         .AddSingleton<IVersionReceiverStrategy, GitHubVersionReceiverStrategy>();
    }
}