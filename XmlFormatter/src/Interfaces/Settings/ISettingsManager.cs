using System.Collections.Generic;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Interfaces.Settings.LoadingProvider;
using XmlFormatter.src.Settings.DataStructure;

namespace XmlFormatter.src.Interfaces.Settings
{
    public interface ISettingsManager
    {
        void SetPersistendFactory(IPersistentFactory factory);
        void AddScope(ISettingScope newScope);
        ISettingScope GetScope(string name);
        List<ISettingScope> GetScopes();

        bool Save(string filePath);
        bool Load(string filePath);
    }
}