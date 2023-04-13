using System;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Get the theme of the application
    /// </summary>
    [Obsolete]
    internal class GetThemeCommand : BaseDataCommand
    {
        /// <summary>
        /// The theme to use 
        /// </summary>
        private ThemeEnum theme;

        /// <summary>
        /// Was this command executed
        /// </summary>
        private bool executed;

        /// <inheritdoc/>
        public async override Task AsyncExecute(object parameter)
        {
            await Task.Run(Execute);
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is ISettingsManager;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            theme = ThemeEnum.Light;
            executed = false;
            if (parameter is ISettingsManager manager)
            {
                ISettingScope scope = manager.GetScope("Default");
                if (scope != null)
                {
                    ISettingPair themeSetting = scope.GetSetting("Theme");
                    if (themeSetting == null)
                    {
                        return;
                    }
                    string themeString = themeSetting.GetValue<string>();
                    if (themeString != string.Empty)
                    {
                        Enum.TryParse(themeString, out theme);
                        executed = true;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {

            Type type = typeof(T);
            return type == typeof(ThemeEnum) ? (T)Convert.ChangeType(theme, typeof(T)) : default;
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            return executed;
        }
    }
}
