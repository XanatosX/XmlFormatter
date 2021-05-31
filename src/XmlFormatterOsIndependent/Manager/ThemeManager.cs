using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.DataSets.Themes;

namespace XmlFormatterOsIndependent.Manager
{
    public static class ThemeManager
    {
        private static Theme lastTheme;

        private static List<IStyledElement> styledElements;

        public static void RegisterWindow(IStyledElement styledElement)
        {
            styledElements = styledElements ?? new List<IStyledElement>();
            styledElements.Add(styledElement);
        }

        public static void UnregisterWindow(IStyledElement styledElement)
        {
            styledElements?.Remove(styledElement);
        }

        public static void ChangeTheme(Theme theme)
        {
            if (theme == null || lastTheme == theme || styledElements == null)
            {
                return;
            }
            foreach (IStyledElement styledElement in styledElements)
            {
                styledElement.Styles.Clear();
                StyleInclude styleToUse = new StyleInclude(new Uri("resm:Styles?assembly=ControlCatalog"))
                {
                    Source = new Uri(theme.Url)
                };
                styledElement.Styles.Add(styleToUse);
            }
            lastTheme = theme;
        }
    }
}
