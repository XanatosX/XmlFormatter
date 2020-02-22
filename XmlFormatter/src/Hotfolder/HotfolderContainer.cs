using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Enum;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.Interfaces.Hotfolder;

namespace XmlFormatter.src.Hotfolder
{
    class HotfolderContainer : IHotfolder
    {
        private ModesEnum mode;
        public ModesEnum Mode
        {
            get => mode;
            set => mode = value;
        }

        private IFormatter formatterToUse;
        public IFormatter FormatterToUse => formatterToUse;

        private string watchedFolder;
        public string WatchedFolder
        {
            get => watchedFolder;
            set => watchedFolder = value;
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set => filter = value; 
        }

        private string outputFolder;
        public string OutputFolder
        {
            get => outputFolder;
            set => outputFolder = value;
        }

        private string outputFileScheme;
        public string OutputFileScheme
        {
            get => outputFileScheme;
            set => outputFileScheme = value;
        }

        private bool onRename;
        public bool OnRename
        {
            get => onRename;
            set => onRename = value;
        }

        private bool removeOld;
        public bool RemoveOld
        {
            get => removeOld;
            set => removeOld = value;
        }

        public HotfolderContainer(IFormatter formatter, string watchedFolder)
        {
            formatterToUse = formatter;
            this.watchedFolder = watchedFolder;
            filter = "*.*";
            outputFolder = this.watchedFolder + "\\output";
            outputFileScheme = "{inputfile}_{format}.{extension}";
        }
    }
}
