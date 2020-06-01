using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Commands
{
    class SwitchStyleCommand : ICommand
    {
        private readonly StyleInclude light;
        private readonly StyleInclude dark;

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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is ThemeSwitchData;
        }

        public void Execute(object parameter)
        {
            if (parameter is ThemeSwitchData data)
            {
                StyleInclude styleToUse = data.Theme == ThemeEnum.Dark ? dark : light;
                Window window = data.View.GetWindow();
                var styles = window.Styles;
                styles.Clear();
                styles.Add(styleToUse);
            }
        }
    }
}
