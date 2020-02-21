using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Interfaces.Settings.LoadingProvider;
using XmlFormatter.src.Settings.DataStructure;
using XmlFormatter.src.Settings.Provider.DataStructure;

namespace XmlFormatter.src.Settings.Provider
{
    class XmlLoaderProvider : ISettingLoadProvider
    {
        public List<ISettingScope> LoadSettings(string filePath)
        {
            List<ISettingScope> settings = new List<ISettingScope>();

            if (!File.Exists(filePath))
            {
                return settings;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingContainer));
            using (TextReader reader = new StreamReader(filePath))
            {
                try
                {
                    SettingContainer result = (SettingContainer)xmlSerializer.Deserialize(reader);
                    foreach (Scope scope in result.Scopes)
                    {
                        ISettingScope settingScope = CreateSettingScope(scope);

                        settings.Add(settingScope);
                    }

                }
                catch (Exception)
                {
                    return settings;
                }
            }

            return settings;
        }

        private ISettingScope CreateSettingScope(Scope scope)
        {
            Type scopeType = Type.GetType(scope.ClassType);
            ISettingScope settingScope = (ISettingScope)Activator.CreateInstance(scopeType);
            settingScope.SetName(scope.Name);

            foreach (Setting setting in scope.Settings)
            {
                SettingPair settingPair = new SettingPair(setting.Name);
                Type type = Type.GetType(setting.Type);
                object value = setting.Value;
                value = Convert.ChangeType(value, type);
                settingPair.SetValue(value);

                settingScope.AddSetting(settingPair);
            }

            foreach (Scope subScope in scope.SubScopes)
            {
                settingScope.AddSubScope(CreateSettingScope(subScope));
            }

            return settingScope;
        }
    }
}
