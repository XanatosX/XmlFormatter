using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Hotfolder;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.Interfaces.Hotfolder;
using XmlFormatter.src.Interfaces.Settings;
using XmlFormatter.src.Interfaces.Settings.DataStructure;
using XmlFormatter.src.Settings.DataStructure;

namespace XmlFormatter.src.Settings.Hotfolder
{
    class HotfolderExtension
    {
        private readonly ISettingsManager settingsManager;

        public HotfolderExtension(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
        }

        public List<IHotfolder> GetHotFoldersFromSettings()
        {
            List<IHotfolder> hotfoldersToReturn = new List<IHotfolder>();
            ISettingScope settingScope = settingsManager.GetScope("Hotfolder");
            if (settingScope == null)
            {
                return hotfoldersToReturn;
            }
            foreach (ISettingScope subSetting in settingScope.GetSubScopes())
            {
                string type = subSetting.GetSetting("Type").GetValue<string>();
                string watchFolder = subSetting.GetSetting("WatchedFolder").GetValue<string>();
                Type realType = Type.GetType(type);
                IFormatter formatter = (IFormatter)Activator.CreateInstance(realType);
                IHotfolder hotfolderToAdd = new HotfolderContainer(formatter, watchFolder);
                string mode = subSetting.GetSetting("Mode").GetValue<string>();
                hotfolderToAdd.Mode = (ModesEnum)Enum.Parse(typeof(ModesEnum), mode);
                hotfolderToAdd.Filter = subSetting.GetSetting("Filter").GetValue<string>();
                hotfolderToAdd.OutputFolder = subSetting.GetSetting("OutputFolder").GetValue<string>();
                hotfolderToAdd.OutputFileScheme = subSetting.GetSetting("Scheme").GetValue<string>();
                hotfolderToAdd.OnRename = subSetting.GetSetting("Rename").GetValue<bool>();
                hotfolderToAdd.RemoveOld = subSetting.GetSetting("Remove").GetValue<bool>();
                hotfoldersToReturn.Add(hotfolderToAdd);
            }

            return hotfoldersToReturn;
        }
    }
}
