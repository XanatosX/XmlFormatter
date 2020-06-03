using System;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Commands
{
    internal class GetThemeCommand : BaseDataCommand
    {
        private ThemeEnum theme;
        private bool executed;

        public async override Task AsyncExecute(object parameter)
        {
            Execute();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is ISettingsManager;
        }

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

        public override T GetData<T>()
        {

            Type type = typeof(T);
            return type == typeof(ThemeEnum) ? (T)Convert.ChangeType(theme, typeof(T)) : default;
        }

        public override bool IsExecuted()
        {
            return executed;
        }
    }
}
