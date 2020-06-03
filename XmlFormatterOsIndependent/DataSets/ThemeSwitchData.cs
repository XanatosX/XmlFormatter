using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Data set with information to switch the theme
    /// </summary>
    internal class ThemeSwitchData
    {
        /// <summary>
        /// The window to switch the theme on
        /// </summary>
        public ViewContainer View { get; }

        /// <summary>
        /// The theme to switch to
        /// </summary>
        public ThemeEnum Theme { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="view">The view to switch theme on</param>
        public ThemeSwitchData(ViewContainer view) : this(view, ThemeEnum.Light)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="view">The view to switch theme on</param>
        /// <param name="theme">The theme to use</param>
        public ThemeSwitchData(ViewContainer view, ThemeEnum theme)
        {
            View = view;
            Theme = theme;
        }
    }
}
