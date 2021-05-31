using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.Themes.LoadableClasses
{
    public class SerializeableTheme
    {
        public string Name;
        public string Url;

        public Theme GetTheme()
        {
            return new Theme(Name, Url);
        }
    }
}
