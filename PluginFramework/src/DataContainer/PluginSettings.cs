using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginFramework.src.DataContainer
{
    public class PluginSettings
    {
        public Dictionary<string, object> Settings { get; }

        public PluginSettings(Dictionary<string, object> settings)
        {
            this.Settings = settings;
        }

        public PluginSettings() : this(new Dictionary<string, object>())
        {
        }

        public void AddValue(string key, object value)
        {
            AddValue(key, value, false);
        }

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

        public T GetValue<T>(string key)
        {
            if (!Settings.ContainsKey(key))
            {
                return default;
            }

            T returnData = default;

            try
            {
                object dataSet = Settings[key];
                returnData = (T)dataSet;
            }
            catch (Exception)
            {
                //Cast did not work no error handling for now
            }

            return returnData;
        }
    }
}
