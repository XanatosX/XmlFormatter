using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.Commands.Settings
{
    class SaveSettingsCommand : BaseTriggerCommand
    {
        private readonly List<object> propertyClasses;
        private readonly string settingScopeName;

        public SaveSettingsCommand(List<object> propertyClasses)
            : this(propertyClasses, "Default")
        {
        }

        public SaveSettingsCommand(List<object> propertyClasses, string settingScopeName)
        {
            this.propertyClasses = propertyClasses.Where(item => item != null).ToList();
            this.settingScopeName = settingScopeName == null || settingScopeName == string.Empty ? "Default" : settingScopeName;
        }

        public override bool CanExecute(object parameter)
        {
            return propertyClasses.Count > 0;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            List<ISettingPair> propertiesToSave = propertyClasses.Select(currentClass => new SaveInformation(currentClass, currentClass.GetType().GetProperties()))
                                                                 .Select(prop => PropertyToISettingPair(prop.Container, prop.SaveableProperties))
                                                                 .SelectMany(item => item)
                                                                 .ToList();

            ISettingScope scope = DefaultManagerFactory.GetSettingsManager().GetScope("Default");
            if (scope == null)
            {
                scope = new SettingScope(settingScopeName);
            }
            foreach(ISettingPair pair in propertiesToSave)
            {
                scope.AddSetting(pair);
            }
            DefaultManagerFactory.GetSettingsManager().AddScope(scope);
            DefaultManagerFactory.GetSettingsManager().Save(DefaultManagerFactory.GetSettingPath());
            CommandExecuted(null);
        }

        private List<ISettingPair> PropertyToISettingPair(object currentClass, IReadOnlyList<PropertyInfo> property)
        {
            return property.Select(property =>
            {
                string propertyName = char.ToUpper(property.Name[0]) + property.Name.Substring(1);
                ISettingPair pair = new SettingPair(propertyName);
                object value = property.GetValue(currentClass, null);
                if (value == null)
                {
                    return pair;
                }
                
                pair.SetValue(value);
                return pair;
            })
            .Where(setting => setting.Value != null)
            .ToList();
        }
    }

    internal class SaveInformation
    {
        public IReadOnlyList<PropertyInfo> SaveableProperties { get; }
        public object Container { get; }

        public SaveInformation(object container, PropertyInfo[] propertyInfos)
        {
            Container = container;
            SaveableProperties = propertyInfos.Where(propInfo => propInfo.GetCustomAttributes(typeof(SettingProperty)).Count() > 0)
                                              .GroupBy(prop => prop.Name)
                                              .Select(group => group.First())
                                              .Where(item => item != null)
                                              .ToList();
        }
    }
}
