using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Interfaces.Settings.LoadingProvider;

namespace XmlFormatter.src.Settings
{
    class SettingsManager : ISettingsManager
    {
        private readonly List<ISettingScope> scopes;

        private IPersistentFactory persistentFactory;

        private ISettingLoadProvider loadProvider;

        private ISettingSaveProvider saveProvider;

        public SettingsManager()
        {
            scopes = new List<ISettingScope>();
        }

        public void SetPersistendFactory(IPersistentFactory factory)
        {
            persistentFactory = factory;
        }

        public void AddScope(ISettingScope newScope)
        {
            scopes.RemoveAll((currentScope) =>
            {
                return currentScope.Name == newScope.Name;
            });
            scopes.Add(newScope);
        }

        public ISettingScope GetScope(string name)
        {
            return scopes.Find((currentScope) =>
            {
                return currentScope.Name == name;
            });
        }

        public List<ISettingScope> GetScopes()
        {
            return scopes;
        }

        public bool Load(string filePath)
        {
            if (persistentFactory == null)
            {
                return false;
            }
            if (loadProvider == null)
            {
                loadProvider = persistentFactory.CreateLoader();
            }
            List<ISettingScope> newScopes = loadProvider.LoadSettings(filePath);
            scopes.Clear();
            foreach (ISettingScope scopeToAdd in newScopes)
            {
                scopes.Add(scopeToAdd);
            }
            return true;
        }

        public bool Save(string filePath)
        {
            if (persistentFactory == null)
            {
                return false;
            }
            if (saveProvider == null)
            {
                saveProvider = persistentFactory.CreateSaver();
            }
            return saveProvider.SaveSettings(this, filePath);
        }
    }
}
