using System.Collections.Generic;
using XmlFormatter.src.Interfaces.Settings.LoadingProvider;

namespace XmlFormatter.src.Interfaces.Settings.DataStructure
{
    public interface ISettingScope
    {
        string Name { get; }

        void SetName(string name);

        void AddSetting(ISettingPair setting);

        void AddSubScope(ISettingScope scope);


        ISettingScope GetSubScope(string name);

        List<ISettingScope> GetSubScopes();

        ISettingPair GetSetting(string name);

        List<ISettingPair> GetSettings();
    }
}