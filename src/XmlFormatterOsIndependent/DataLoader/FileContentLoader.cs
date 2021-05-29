using System.IO;

namespace XmlFormatterOsIndependent.DataLoader
{
    public class FileContentLoader : IDataLoader<string>
    {
        public string Load(string path)
        {
            string returnData = string.Empty;
            if (!File.Exists(path))
            {
                return returnData;
            }

            using(StreamReader reader = new StreamReader(path))
            {
                returnData = reader.ReadToEnd();
            }

            return returnData;
        }
    }
}
