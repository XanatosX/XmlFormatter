using Avalonia.Data.Converters;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using XmlFormatterOsIndependent.DataSets.Files;

namespace XmlFormatterOsIndependent.Converters
{
    class ConvertConversionData : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            string InputFile = string.Empty;
            PluginMetaData pluginMetaData = null;
            ModesEnum mode = ModesEnum.Formatted;
            if (values.Count != 3)
            {
                return null;
            }
            if (values[0] is string file)
            {
                InputFile = file;
            }
            if (values[1] is PluginMetaData metaData)
            {
                pluginMetaData = metaData;
            }
            if (values[0] is ModesEnum selection)
            {
                mode = selection;
            }
            if (InputFile != string.Empty && pluginMetaData != null)
            {
                return new SaveFileConversionData(InputFile, mode, pluginMetaData);
            }
            return null;
        }
    }
}
