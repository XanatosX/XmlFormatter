using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Settings.DataStructure;

namespace XmlFormatter.src.Settings.DataStructure
{
    class SettingPair : ISettingPair
    {
        private readonly string name;
        public string Name => name;

        private Type type;
        public Type Type => type;

        private object value;
        public object Value => value;

        public SettingPair(string name)
        {
            this.name = name;
        }

        public void SetValue<T>(T value)
        {
            this.value = value;
            type = value.GetType();
        }

        public T GetValue<T>()
        {
            try
            {
                T returnValue = (T)value;
                return returnValue;
            }
            catch (Exception)
            {

            }

            return default(T);

        }
    }
}
