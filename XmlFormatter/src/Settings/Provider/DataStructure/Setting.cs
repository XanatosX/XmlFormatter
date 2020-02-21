using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    public class Setting
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        private string dataValue;
        public string Value
        {
            get => dataValue;
            set => dataValue = value;
        }

        private string type;
        public string Type
        {
            get => type;
            set => type = value;
        }
    }
}