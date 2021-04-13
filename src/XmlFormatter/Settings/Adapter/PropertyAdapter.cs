using System;
using System.Collections.Generic;
using System.Configuration;
using XmlFormatterModel.Setting;

namespace XmlFormatter.Settings.Adapter
{
    /// <summary>
    /// This class is a adapter between the new setting methods and the old resource based
    /// </summary>
    class PropertyAdapter : ISettingScope
    {
        /// <summary>
        /// The name of this scope
        /// </summary>
        private string name;

        /// <summary>
        /// Readonly access to the scope name
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Create a new instance of this adapter which will always use "Default" as scope name
        /// </summary>
        public PropertyAdapter()
        {
            SetName("Default");
        }

        /// <summary>
        /// Set the name of this adapter
        /// </summary>
        /// <param name="name">The new name to use</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Add a new setting to the resource file.
        /// This will only work if the setting is already defined
        /// </summary>
        /// <param name="setting">The setting to add</param>
        public void AddSetting(ISettingPair setting)
        {
            try
            {
                Type type = Properties.Settings.Default[setting.Name].GetType();
                if (type == setting.Type)
                {
                    Properties.Settings.Default[setting.Name] = setting.Value;
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Get a setting from the resource file
        /// </summary>
        /// <param name="name">The name of the setting to get</param>
        /// <returns>A instance of the setting or null if nothing was present</returns>
        public ISettingPair GetSetting(string name)
        {
            ISettingPair returnValue = null;
            try
            {
                var propertyValue = Properties.Settings.Default[name];
                returnValue = new SettingPair(name);
                returnValue.SetValue(propertyValue);
            }
            catch (Exception)
            {
                //Not important since we can return an empty value
            }

            return returnValue;
        }

        /// <summary>
        /// Get all the settings stored in the resource file
        /// </summary>
        /// <returns>All the settings present in the resource file</returns>
        public List<ISettingPair> GetSettings()
        {
            List<ISettingPair> settingPairs = new List<ISettingPair>();
            foreach (SettingsProperty property in Properties.Settings.Default.Properties)
            {
                if (property.Attributes.Contains(typeof(ApplicationScopedSettingAttribute)))
                {
                    continue;
                }
                SettingPair settingPair = new SettingPair(property.Name);
                settingPair.SetValue(Properties.Settings.Default[property.Name]);
                settingPairs.Add(settingPair);
            }
            return settingPairs;
        }

        /// <summary>
        /// This method is not working for this adapter
        /// </summary>
        /// <param name="scope">The scope to add</param>
        public void AddSubScope(ISettingScope scope)
        {
        }

        /// <summary>
        /// This method is not working for this adapter
        /// </summary>
        /// <returns>A new empty list</returns>
        public List<ISettingScope> GetSubScopes()
        {
            return new List<ISettingScope>();
        }

        /// <summary>
        /// This method is not working for this adapter
        /// </summary>
        /// <param name="name">The name of the sub scope to get</param>
        /// <returns>null</returns>
        public ISettingScope GetSubScope(string name)
        {
            return null;
        }

        /// <summary>
        /// This method is not supported by this adapter
        /// </summary>
        /// <param name="name">The name of the scope to remove</param>
        /// <returns>Always true</returns>
        public bool RemoveSubScope(string name)
        {
            return true;
        }

        /// <summary>
        /// Method is not supported by the adapter
        /// </summary>
        public void ClearSubScopes()
        {
        }

        /// <summary>
        /// This method is not supported by this adapter
        /// </summary>
        /// <param name="name">The name of the setting to remove</param>
        /// <returns>Always true</returns>
        public bool RemoveSetting(string name)
        {
            return true;
        }

        /// <summary>
        /// Method is not supported by the adapter
        /// </summary>
        public void RemoveSettings()
        {
        }
    }
}
