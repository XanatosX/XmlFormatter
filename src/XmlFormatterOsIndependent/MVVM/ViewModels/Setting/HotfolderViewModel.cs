using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XMLFormatterModel.Hotfolder;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.MVVM.Views.Popups;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Setting
{
    public class HotfolderViewModel : ReactiveObject
    {
        [SettingProperty]
        public bool HotfolderActive { get; set; }

        public ObservableCollection<IHotfolder> Hotfolders { get; private set; }

        public ICommand AddHotfolder { get; }

        public ICommand EditHotfolder { get; }

        public HotfolderViewModel()
        {
            ISettingPair pair = DefaultManagerFactory.GetSettingsManager().GetSetting("Default", "HotfolderActive");
            HotfolderActive = pair == null ? false : pair.GetValue<bool>();

            AddHotfolder = new OpenPopupWindow(new AddHotfolderPopup());
        }
    }
}
