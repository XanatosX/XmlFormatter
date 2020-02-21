using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    public class Scope
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        private string classType;
        public string ClassType
        {
            get => classType;
            set => classType = value;
        }

        private List<Scope> subScopes;
        public List<Scope> SubScopes
        {
            get => subScopes;
            set => subScopes = value;
        }

        private List<Setting> settings;
        public List<Setting> Settings
        {
            get => settings;
            set => settings = value;
        }

        public Scope()
        {
            settings = new List<Setting>();
            subScopes = new List<Scope>();
        }
    }
}
