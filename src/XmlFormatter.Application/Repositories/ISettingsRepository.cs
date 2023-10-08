namespace XmlFormatter.Application;

/// <summary>
/// Repository to create or update settings
/// </summary>
/// <typeparam name="T">The type of settings to store</typeparam>
public interface ISettingsRepository<T> where T : class, new()
{
    /// <summary>
    /// Create a new set of settings or load an existing one
    /// </summary>
    /// <returns>The loaded or created settings, otherwise null</returns>
    T? CreateOrLoad();

    /// <summary>
    /// Create a new set of settings or load a existing one async
    /// </summary>
    /// <returns>The loaded or created settings, otherwise null</returns>
    Task<T?> CreateOrLoadAsync();

    /// <summary>
    /// Update the settings file
    /// </summary>
    /// <param name="settings">The settings to use as an update</param>
    /// <returns>The updated settings</returns>
    T? Update(T settings);

    /// <summary>
    /// Update the settings file async
    /// </summary>
    /// <param name="settings">The settings to use as an update</param>
    /// <returns>The updated setting file</returns>
    Task<T?> UpdateAsync(T settings);

    /// <summary>
    /// Update the settings file
    /// </summary>
    /// <param name="settingActions">The action used to alter the current settings</param>
    /// <returns>The updated settings</returns>
    T? Update(Action<T?> settingActions);

    /// <summary>
    /// Update the settings file async
    /// </summary>
    /// <param name="settingActions">The action used to alter the current settings</param>
    /// <returns>The updated settings</returns>
    Task<T?> UpdateAsync(Action<T?>settingActions);
}

