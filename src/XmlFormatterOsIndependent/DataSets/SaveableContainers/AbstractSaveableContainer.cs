using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.DataSets.Attributes;

namespace XmlFormatterOsIndependent.DataSets.SaveableContainers
{
    public abstract class AbstractSaveableContainer<T> : ISaveableContainer<T>
    {
        /// <inheritdoc/>
        public abstract T GetLoadedInstance(List<ISettingPair> settingsPairs);

        public abstract T GetStoredInstance();

        /// <inheritdoc/>
        public virtual List<ISettingPair> GetSettingPairs()
        {
            return GetSettingPairs(this);
        }

        protected List<ISettingPair> GetSettingPairs(object propertyClass)
        {
            return propertyClass.GetType().GetProperties().Where(propInfo => propInfo.GetCustomAttributes(typeof(SettingProperty)).Count() > 0)
                                                          .Select(property => GetSettingPair(propertyClass, property))
                                                          .Where(settingsPair => settingsPair != null)
                                                          .ToList();
        }

        protected List<ISettingPair> PropertyToISettingPair(object currentClass, IReadOnlyList<PropertyInfo> property)
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


        protected ISettingPair GetSettingPair(object currentClass, PropertyInfo property)
        {
            string propertyName = char.ToUpper(property.Name[0]) + property.Name.Substring(1);
            ISettingPair pair = new SettingPair(propertyName);
            object value = property.GetValue(currentClass, null);
            if (value == null)
            {
                return null;
            }

            pair.SetValue(value);
            return pair;
        }
    }
}
