using System;
using System.Collections.Generic;

namespace XmlFormatter.Domain.PluginFeature
{
    /// <summary>
    /// The settings of the plugin
    /// </summary>
    public class PluginSettings
    {
        /// <summary>
        /// Dictionary with all the settings
        /// </summary>
        public Dictionary<string, object> Settings { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settings">The initial settings</param>
        public PluginSettings(Dictionary<string, object> settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public PluginSettings() : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Add a new value to the settings
        /// </summary>
        /// <param name="key">The key of the setting</param>
        /// <param name="value">The value of the setting</param>
        public void AddValue(string key, object value)
        {
            AddValue(key, value, false);
        }

        /// <summary>
        /// Add a new value to the settings
        /// </summary>
        /// <param name="key">The key of the setting</param>
        /// <param name="value">The value of the setting</param>
        /// <param name="allowOverride">Should we override already existing settings</param>
        public void AddValue(string key, object value, bool allowOverride)
        {
            if (Settings.ContainsKey(key))
            {
                if (allowOverride)
                {
                    Settings[key] = value;
                }

                return;
            }

            Settings.Add(key, value);
        }

        /// <summary>
        /// The a value from the settings
        /// </summary>
        /// <typeparam name="T">The type of the setting to get</typeparam>
        /// <param name="key">The key to get</param>
        /// <returns>An instance of type T of the requested setting</returns>
        public T GetValue<T>(string key)
        {
            if (!Settings.ContainsKey(key))
            {
                return default;
            }

            try
            {
                object dataSet = Settings[key];
                return (T)dataSet;
            }
            catch (Exception)
            {
                //Cast did not work no error handling for now
            }

            return default;
        }
    }
}