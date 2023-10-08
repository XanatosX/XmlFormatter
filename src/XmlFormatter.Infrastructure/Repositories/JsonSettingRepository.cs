using System.Text.Json;
using XmlFormatter.Application;
using XmlFormatter.Infrastructure.Configuration;

namespace XmlFormatter.Infrastructure.Repositories;

/// <summary>
/// Implementation of the settings repository by saving it as json files
/// </summary>
/// <typeparam name="T">The type of data to be saved as settings</typeparam>
public class JsonSettingRepository<T> : ISettingsRepository<T> where T : class, new()
{
    /// <summary>
    /// The options for the settings folders
    /// </summary>
    private readonly SettingsOptions settingsOptions;

    /// <summary>
    /// The settings options used for serialization
    /// </summary>
    private readonly JsonSerializerOptions options;

    /// <summary>
    /// Create a new instance for this class
    /// </summary>
    /// <param name="settingsOptions">The settings option to use for finding the setting file</param>
    /// <param name="options">The conversion options to use</param>
    public JsonSettingRepository(SettingsOptions settingsOptions, JsonSerializerOptions options)
    {
        this.settingsOptions = settingsOptions;
        this.options = options;
    }

    /// <inheritdoc/>
    public T? CreateOrLoad()
    {
        if (File.Exists(settingsOptions.GetSettingPath()))
        {
            return LoadSettings();
        }

        T? settings = new();
        if (settings is null)
        {
            return default;
        }
        return Update(settings);
    }

    /// <inheritdoc/>
    private T? LoadSettings()
    {
        T? returnData = default;
        using (FileStream fileStream = new FileStream(settingsOptions.GetSettingPath(), FileMode.Open, FileAccess.Read))
        {
            try
            {
                returnData = JsonSerializer.Deserialize<T>(fileStream, options);

            }
            catch (System.Exception)
            {
                //TODO Log error if something goes wrong
            }
        }
        return returnData;
    }

    /// <inheritdoc/>
    public async Task<T?> CreateOrLoadAsync()
    {
        return await Task.Run(() => CreateOrLoad());
    }

    /// <inheritdoc/>
    public T? Update(T settings)
    {
        if (!Directory.Exists(settingsOptions.SettingDirectory))
        {
            Directory.CreateDirectory(settingsOptions.SettingDirectory);
        }
        try
        {
            using (FileStream fileStream = new FileStream(settingsOptions.GetSettingPath(), FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize(fileStream, settings, options);
            }
        }
        catch (System.Exception)
        {
            //TODO Add code in case of failure
        }

        return CreateOrLoad();
    }

    /// <inheritdoc/>
    public T? Update(Action<T?> settingActions)
    {
        T? settings = CreateOrLoad();
        settingActions(settings);
        if (settings is null)
        {
            return default;
        }

        return Update(settings);
    }

    /// <inheritdoc/>
    public Task<T?> UpdateAsync(T settings)
    {
        return Task.Run(() => Update(settings));
    }

    /// <inheritdoc/>
    public Task<T?> UpdateAsync(Action<T?> settingActions)
    {
        return Task.Run(() => Update(settingActions));
    }
}
