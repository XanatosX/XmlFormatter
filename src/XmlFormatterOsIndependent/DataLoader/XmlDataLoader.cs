using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlFormatterOsIndependent.DataLoader
{
    class XmlDataLoader<T> : IDataLoader<T>
    {
        private readonly XmlSerializer serializerToUse;

        public XmlDataLoader()
        {
            serializerToUse = new XmlSerializer(typeof(T));
        }
        public T Load(string path)
        {
            T result = default(T);
            try
            {
                StreamReader reader = GetStreamReader(path);
                if (reader == null)
                {
                    return result;
                }
                using (reader)
                {
                    result = (T)serializerToUse?.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }

        protected virtual StreamReader GetStreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
