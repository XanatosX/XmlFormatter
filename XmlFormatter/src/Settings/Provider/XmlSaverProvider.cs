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
using XmlFormatter.src.Settings.Provider.DataStructure;

namespace XmlFormatter.src.Settings.Provider
{
    class XmlSaverProvider : ISettingSaveProvider
    {
        public bool SaveSettings(ISettingsManager settingsManager, string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            string directory = info.DirectoryName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            SettingContainer container = new SettingContainer();
            foreach (ISettingScope scope in settingsManager.GetScopes())
            {
                container.Scopes.Add(ConvertToSaveableScope(scope));
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingContainer));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, container);
            }
                

            return true;
        }

        private Scope ConvertToSaveableScope(ISettingScope scope)
        {
            List<Setting> settings = new List<Setting>();
            foreach (ISettingPair settingPair in scope.GetSettings())
            {
                settings.Add(new Setting()
                {
                    Name = settingPair.Name,
                    Type = settingPair.Type.ToString(),
                    Value = settingPair.Value.ToString(),
                });
            }
            Scope returnScope = new Scope()
            {
                Name = scope.Name,
                Settings = settings,
                ClassType = scope.GetType().ToString()
            };
            foreach (ISettingScope subScope in scope.GetSubScopes())
            {
                returnScope.SubScopes.Add(ConvertToSaveableScope(subScope));
            }
            return returnScope;
        }
    }
}
