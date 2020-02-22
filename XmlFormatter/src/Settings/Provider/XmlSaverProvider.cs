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
            SerializableSettingContainer container = new SerializableSettingContainer();
            foreach (ISettingScope scope in settingsManager.GetScopes())
            {
                container.Scopes.Add(ConvertToSaveableScope(scope));
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializableSettingContainer));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, container);
            }
                

            return true;
        }

        private SerializableScope ConvertToSaveableScope(ISettingScope scope)
        {
            List<SerializableSetting> settings = new List<SerializableSetting>();
            foreach (ISettingPair settingPair in scope.GetSettings())
            {
                settings.Add(new SerializableSetting()
                {
                    Name = settingPair.Name,
                    Type = settingPair.Type.ToString(),
                    Value = settingPair.Value.ToString(),
                });
            }
            SerializableScope returnScope = new SerializableScope()
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
