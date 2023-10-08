using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatterOsIndependent.Serializer;

/// <summary>
/// The plugin meta data serializer
/// </summary>
internal sealed class PluginMetaDataSerializer : JsonConverter<PluginMetaData?>
{
    /// <summary>
    /// Plugin manager to load the meta data based on class type
    /// </summary>
    private readonly IPluginManager pluginManager;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginManager">Plugin manager to load meta data based on class type</param>
    public PluginMetaDataSerializer(IPluginManager pluginManager)
    {
        this.pluginManager = pluginManager;
    }

    /// <inheritdoc/>
    public override PluginMetaData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        var typeString = reader.GetString();
        var plugins = pluginManager.ListPlugins<IFormatter>().Where(plugin => plugin.Type.ToString() == typeString).ToList();
        plugins.AddRange(pluginManager.ListPlugins<IUpdateStrategy>().Where(plugin => plugin.Type.ToString() == typeString));
        return plugins.FirstOrDefault();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, PluginMetaData? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }
        writer.WriteStringValue(value.Type.ToString());
    }
}