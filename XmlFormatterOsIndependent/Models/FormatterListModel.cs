using PluginFramework.DataContainer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Models
{
    public class FormatterListModel : ViewModelBase
    {
        public FormatterListModel()
        {

        }

        public FormatterListModel(IEnumerable<PluginMetaData> items)
        {
            Items = new ObservableCollection<PluginMetaData>(items);
        }

        public ObservableCollection<PluginMetaData> Items { get; }
    }
}
