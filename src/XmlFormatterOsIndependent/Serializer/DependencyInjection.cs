using Microsoft.Extensions.DependencyInjection;

namespace XmlFormatterOsIndependent.Serializer;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    /// Add all the json type converters
    /// </summary>
    /// <param name="collection">The service collection to edit</param>
    /// <returns>The edited service collection</returns>
    public static IServiceCollection AddConverters(this IServiceCollection collection)
    {
        return collection.AddSingleton<PluginMetaDataSerializer>();
    }
}
