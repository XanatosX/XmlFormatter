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

    public JsonSettingRepository(SettingsOptions settingsOptions, JsonSerializerOptions options)
    {
        this.settingsOptions = settingsOptions;
        this.options = options;
    }

    public T? CreateOrLoad()
    {
        if (File.Exists(settingsOptions.GetSettingPath()))
        {
            return LoadSettings();
        }

        T? settings = new T();
        if (settings is null)
        {
            return default;
        }
        return Update(settings);
    }

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

    public async Task<T?> CreateOrLoadAsync()
    {
        return await Task.Run(() => CreateOrLoad());
    }

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
        catch (System.Exception ex)
        {
            //TODO Add code in case of failure
        }

        return CreateOrLoad();
    }

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

    public Task<T?> UpdateAsync(T settings)
    {
        return Task.Run(() => Update(settings));
    }

    public Task<T?> UpdateAsync(Action<T?> settingActions)
    {
        return Task.Run(() => Update(settingActions));
    }
}
