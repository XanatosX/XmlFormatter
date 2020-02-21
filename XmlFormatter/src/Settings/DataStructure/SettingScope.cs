using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Settings.DataStructure;

namespace XmlFormatter.src.Settings.DataStructure
{
    class SettingScope : ISettingScope
    {
        private string name;
        public string Name => name;

        private readonly List<ISettingScope> subScopes;

        private readonly List<ISettingPair> settings;

        public SettingScope()
        {
            settings = new List<ISettingPair>();
            subScopes = new List<ISettingScope>();
        }

        public SettingScope(string name)
        {
            settings = new List<ISettingPair>();
            subScopes = new List<ISettingScope>();
            SetName(name);
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void AddSetting(ISettingPair setting)
        {
            ISettingPair stored = GetSetting(setting.Name);
            if (stored != null)
            {
                stored.SetValue(setting.Value);
                return;
            }

            settings.Add(setting);
        }

        public ISettingPair GetSetting(string name)
        {
            return settings.Find((currentObject) =>
            {
                return currentObject.Name == name;
            });
        }

        public List<ISettingPair> GetSettings()
        {
            return settings;
        }

        public void AddSubScope(ISettingScope scope)
        {
            subScopes.RemoveAll((currentScope) =>
            {
                return currentScope.Name == scope.Name;
            });
            subScopes.Add(scope);
        }

        public ISettingScope GetSubScope(string name)
        {
            return subScopes.Find((currentScope) =>
            {
                return currentScope.Name == name;
            });
        }

        public List<ISettingScope> GetSubScopes()
        {
            return subScopes;
        }


    }
}
