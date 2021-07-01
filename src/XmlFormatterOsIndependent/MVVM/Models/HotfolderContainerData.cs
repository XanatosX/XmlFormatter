using PluginFramework.Interfaces.PluginTypes;
using System.IO;
using XMLFormatterModel.Hotfolder;

namespace XmlFormatterOsIndependent.MVVM.Models
{
    public class HotfolderContainerData : HotfolderContainer
    {
        public HotfolderContainerData(IFormatter formatter, string watchedFolder)
            : base(formatter, watchedFolder)
        {
        }

        public HotfolderContainerData()
            : base(null, string.Empty)
        {
            RemoveOld = false;
            OnRename = false;
        }

        public HotfolderContainerData(IHotfolder hotfolder)
            : base(hotfolder.FormatterToUse, hotfolder.WatchedFolder)
        {
            Mode = hotfolder.Mode;
            Filter = hotfolder.Filter;
            OutputFolder = hotfolder.OutputFolder;
            OutputFileScheme = hotfolder.OutputFileScheme;
            OnRename = hotfolder.OnRename;
            RemoveOld = hotfolder.RemoveOld;
        }

        public bool IsValid()
        {
            bool valid = FormatterToUse != null;
            valid &= Directory.Exists(OutputFolder);
            valid &= Directory.Exists(WatchedFolder);
            valid &= Filter != string.Empty;
            valid &= OutputFileScheme != string.Empty;
            valid &= OutputFolder != WatchedFolder;
            return valid;
        }
    }
}
