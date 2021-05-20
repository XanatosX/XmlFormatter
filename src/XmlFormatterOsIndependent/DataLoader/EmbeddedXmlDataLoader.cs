using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace XmlFormatterOsIndependent.DataLoader
{
    class EmbeddedXmlDataLoader<T> : XmlDataLoader<T>
    {
        private readonly Assembly assembly;
        public EmbeddedXmlDataLoader()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        protected override StreamReader GetStreamReader(string path)
        {
            Stream stream = assembly.GetManifestResourceStream(path);
            return new StreamReader(stream);
        }
    }
}
