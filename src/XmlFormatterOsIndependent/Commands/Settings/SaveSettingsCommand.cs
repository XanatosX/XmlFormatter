using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.Commands.Settings
{
    class SaveSettingsCommand : BaseTriggerCommand
    {
        private readonly DefaultManagerFactory defaultManagerFactory;
        private readonly object propertyClass;
        private readonly string filePath;

        public SaveSettingsCommand(DefaultManagerFactory defaultManagerFactory, object propertyClass, string filePath)
        {
            this.defaultManagerFactory = defaultManagerFactory;
            this.propertyClass = propertyClass;
            this.filePath = filePath;
        }

        public override bool CanExecute(object parameter)
        {
            return defaultManagerFactory != null
                   && propertyClass != null
                   && propertyClass.GetType().GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(SettingProperty)).Count() > 0) != null;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            List<ISettingPair> propertiesToSave = propertyClass.GetType().GetProperties()
                                     .Where(prop => prop.GetCustomAttributes(typeof(SettingProperty)).Count() > 0)
                                     .Select(prop => PropertyToISettingPair(prop))
                                     .Where(setting => setting.Value != null)
                                     .ToList();

            ISettingScope scope = defaultManagerFactory.GetSettingsManager().GetScope("Default");
            if (scope == null)
            {
                return;
            }
            foreach(ISettingPair pair in propertiesToSave)
            {
                scope.AddSetting(pair);
            }
            //defaultManagerFactory.GetSettingsManager().AddScope(scope);
            defaultManagerFactory.GetSettingsManager().Save(filePath);
        }

        private ISettingPair PropertyToISettingPair(PropertyInfo property)
        {
            string propertyName = char.ToUpper(property.Name[0]) + property.Name.Substring(1);
            ISettingPair pair = new SettingPair(propertyName);
            object value = property.GetValue(propertyClass, null);
            if (value == null)
            {
                return pair;
            }
            pair.SetValue(value);
            return pair;
        }
    }
}
