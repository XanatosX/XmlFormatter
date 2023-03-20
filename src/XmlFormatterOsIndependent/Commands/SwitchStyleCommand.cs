using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using System;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Switch the style of the current window
    /// </summary>
    class SwitchStyleCommand : ICommand
    {
        /// <summary>
        /// The style to include for light theme
        /// </summary>
        private readonly StyleInclude light;

        /// <summary>
        /// The style to include for the dark theme
        /// </summary>
        private readonly StyleInclude dark;

        /// <summary>
        /// Create a new instance of this command class
        /// </summary>
        public SwitchStyleCommand()
        {
            light = new StyleInclude(new Uri("resm:Styles?assembly=ControlCatalog"))
            {
                Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default")
            };

            dark = new StyleInclude(new Uri("resm:Styles?assembly=ControlCatalog"))
            {
                Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default")
            };
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return parameter is ThemeSwitchData;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (parameter is ThemeSwitchData data)
            {
                StyleInclude styleToUse = data.Theme == ThemeEnum.Dark ? dark : light;
                //@TODO: Fix
                //Window window = data.View.GetWindow();
                //var styles = window.Styles;
                //styles.Clear();
                //styles.Add(styleToUse);
            }
        }
    }
}
