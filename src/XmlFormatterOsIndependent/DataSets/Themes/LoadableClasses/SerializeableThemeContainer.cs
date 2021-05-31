using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.Themes.LoadableClasses
{
    public class SerializeableThemeContainer
    {
        public List<SerializeableTheme> Themes;

        public ThemeContainer GetThemeContainer()
        {
            return new ThemeContainer(Themes.Select(theme => theme.GetTheme()).ToList());
        }
    }
}
