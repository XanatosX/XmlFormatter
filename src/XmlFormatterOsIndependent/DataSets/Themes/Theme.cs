using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.Themes
{
    public class Theme
    {
        public string Name { get; }
        public string Url { get; }

        public Theme(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
