using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatterOsIndependent.Serializer;

internal sealed class PluginMetaDataSerializer : JsonConverter<PluginMetaData?>
{
    private readonly IPluginManager pluginManager;

    public PluginMetaDataSerializer(IPluginManager pluginManager)
    {
        this.pluginManager = pluginManager;
    }

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