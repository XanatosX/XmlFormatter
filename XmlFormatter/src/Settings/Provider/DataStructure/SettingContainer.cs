using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    public class SettingContainer
    {
        private List<Scope> scopes;
        public List<Scope> Scopes
        {
            get => scopes;
            set => scopes = value;
        }

        public SettingContainer()
        {
            scopes = new List<Scope>();
        }
    }
}
