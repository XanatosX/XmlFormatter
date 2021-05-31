using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.Themes
{
    public class ThemeContainer
    {
        public List<Theme> Themes { get; }

        public ThemeContainer(List<Theme> themes)
        {
            Themes = themes == null ? new List<Theme>() : themes;
        }
    }
}
