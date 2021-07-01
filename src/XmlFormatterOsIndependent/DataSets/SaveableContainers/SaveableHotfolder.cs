using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterModel.Setting;
using XMLFormatterModel.Hotfolder;
using XmlFormatterOsIndependent.DataSets.Attributes;

namespace XmlFormatterOsIndependent.DataSets.SaveableContainers
{
    public class SaveableHotfolder : AbstractSaveableContainer<IHotfolder>
    {
        private readonly IHotfolder hotfolder;

        [SettingProperty]
        public string Mode { get; }

        [SettingProperty]
        public string FormatterToUse { get; }

        [SettingProperty]
        public string WatchedFolder { get; }

        [SettingProperty]
        public string Filter { get; }

        [SettingProperty]
        public string OutputFolder { get; }

        [SettingProperty]
        public string OutputFileScheme { get; }

        [SettingProperty]
        public bool OnRename { get; }

        [SettingProperty]
        public bool RemoveOld { get; }

        public SaveableHotfolder(IHotfolder hotfolder)
        {
            this.hotfolder = hotfolder;
            Mode = hotfolder.Mode.ToString();
            FormatterToUse = hotfolder.FormatterToUse.ToString();
            WatchedFolder = hotfolder.WatchedFolder;
            Filter = hotfolder.Filter;
            OutputFolder = hotfolder.OutputFolder;
            OutputFileScheme = hotfolder.OutputFileScheme;
            OnRename = hotfolder.OnRename;
            RemoveOld = hotfolder.RemoveOld;
        }

        public override IHotfolder GetLoadedInstance(List<ISettingPair> settingsPairs)
        {
            //this.GetType().GetProperties().
            return null;
        }

        public override IHotfolder GetStoredInstance()
        {
            return hotfolder;
        }
    }
}
