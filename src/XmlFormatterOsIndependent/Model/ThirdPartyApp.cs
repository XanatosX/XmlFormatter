
using System.Text.Json.Serialization;

namespace XmlFormatterOsIndependent.Model;

/// <summary>
/// An object for an third party object
/// </summary>
internal class ThirdPartyApp
{
    /// <summary>
    /// The name of the third party app
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The version of the third party app
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = null!;

    /// <summary>
    /// The lincense of the third party app
    /// </summary>
    [JsonPropertyName("license")]
    public string License { get; init; } = null!;

    /// <summary>
    /// The lincense url of the third party app
    /// </summary>
    [JsonPropertyName("licenseUrl")]
    public string? LicenseUrl { get; init; }

    /// <summary>
    /// The project url of the third party app
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;
}
