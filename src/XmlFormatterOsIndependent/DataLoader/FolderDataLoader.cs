using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XmlFormatterOsIndependent.DataLoader
{
    public class FolderDataLoader : IDataLoader<List<FileInfo>>
    {
        private readonly Predicate<FileInfo> filter;

        public FolderDataLoader()
            : this(data => true)
        {
        }

        public FolderDataLoader(Predicate<FileInfo> filter)
        {
            this.filter = filter;
        }

        public List<FileInfo> Load(string path)
        {
            if (!Directory.Exists(path))
            {
                return new List<FileInfo>();
            }
            return Directory.GetFiles(path).ToList()
                            .Select(filePath => new FileInfo(filePath))
                            .Where(info => filter(info))
                            .OrderBy(info => info.Name)
                            .ToList();

        }
    }
}
