using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;

namespace XmlFormatterModel.HotfolderFeature
{
    /// <summary>
    /// This class is a helper to load the hotfolder configuration
    /// </summary>
    [Obsolete]
    public class HotfolderExtension
    {
        /// <summary>
        /// Plugin manager to use
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new instance of this extension class
        /// </summary>
        /// <param name="settingsManager">The settings manager to use</param>
        public HotfolderExtension(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        /// <summary>
        /// Get the hot folders from the settings
        /// </summary>
        /// <returns>A list with all the hot folders</returns>
        public List<Hotfolder> GetHotFoldersFromSettings()
        {
            List<Hotfolder> hotfoldersToReturn = new List<Hotfolder>();
            return new();
            //TODO Fix code!
            /**
            ISettingScope settingScope = settingsManager.GetScope("Hotfolder");
            if (settingScope == null)
            {
                return hotfoldersToReturn;
            }
            foreach (ISettingScope subSetting in settingScope.GetSubScopes())
            {
                string type = subSetting.GetSetting("Type").GetValue<string>();
                string watchFolder = subSetting.GetSetting("WatchedFolder").GetValue<string>();
                IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(type);
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
            */
        }
    }
}
