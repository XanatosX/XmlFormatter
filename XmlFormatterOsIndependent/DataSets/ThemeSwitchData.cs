using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.DataSets
{
    class ThemeSwitchData
    {
        public ViewContainer View { get; }
        public ThemeEnum Theme { get;  }

        public ThemeSwitchData(ViewContainer view) : this(view, ThemeEnum.Light)
        {

        }

        public ThemeSwitchData(ViewContainer view, ThemeEnum theme)
        {
            View = view;
            Theme = theme;
        }
    }
}
