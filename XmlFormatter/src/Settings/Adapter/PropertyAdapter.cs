using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Settings.DataStructure;

namespace XmlFormatter.src.Settings.Adapter
{
    class PropertyAdapter : ISettingScope
    {
        private string name;
        public string Name => name;

        public PropertyAdapter()
        {
            SetName("Default");
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void AddSetting(ISettingPair setting)
        {
            try
            {
                Type type = Properties.Settings.Default[setting.Name].GetType();
                var test = Properties.Settings.Default[setting.Name];
                if (type == setting.Type)
                {
                    Properties.Settings.Default[setting.Name] = setting.Value;
                }
            }
            catch (Exception)
            {

            }
        }

        public ISettingPair GetSetting(string name)
        {
            ISettingPair returnValue = null;
            try
            {
                var test = Properties.Settings.Default[name];
                returnValue = new SettingPair(name);
                returnValue.SetValue(test);
            }
            catch (Exception)
            {
            }
            
            return returnValue;
        }

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

        public void AddSubScope(ISettingScope scope)
        {
            return;
        }

        public List<ISettingScope> GetSubScopes()
        {
            return new List<ISettingScope>();
        }

        public ISettingScope GetSubScope(string name)
        {
            return null;
        }
    }
}
