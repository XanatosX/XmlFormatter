using PluginFramework.Enums;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XMLFormatterModel.Hotfolder;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.Commands.Settings;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.DataSets.SaveableContainers;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.MVVM.Models;
using XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors;
using XmlFormatterOsIndependent.MVVM.Views.Popups;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Setting
{
    public class HotfolderViewModel : ReactiveObject, ISaveSettingView
    {
        [SettingProperty]
        public bool HotfolderActive
        {
            get => hotfolderActive;
            set
            {
                this.RaiseAndSetIfChanged(ref hotfolderActive, value);
                this.RaisePropertyChanged("ShowHotfolderTable");
                this.RaisePropertyChanged("ShowHotfolderEditing");
                AddHotfolder?.DataHasChanged();
                EditHotfolder?.DataHasChanged();
            }
        }

        private bool hotfolderActive;

        public bool AddHotfolderMode
        {
            get => addHotfolderMode;
            private set
            {
                this.RaiseAndSetIfChanged(ref addHotfolderMode, value);
                this.RaisePropertyChanged("ShowHotfolderTable");
                this.RaisePropertyChanged("ShowHotfolderEditing");
            }
        }

        private bool addHotfolderMode;

        public bool ShowHotfolderTable => HotfolderActive && !AddHotfolderMode;

        public bool ShowHotfolderEditing => HotfolderActive && AddHotfolderMode;

        public ObservableCollection<HotfolderContainerData> Hotfolders { get; private set; }

        public HotfolderContainerData SelectedHotfolder
        {
            get => selectedHotfolder;
            set
            {
                SaveHotfolderChanges?.DataHasChanged();
                EditHotfolder?.DataHasChanged();
                this.RaiseAndSetIfChanged(ref selectedHotfolder, value);
            }
        }
        private HotfolderContainerData selectedHotfolder;

        public int SelectedHotfolderIndex { get; set; }

        public IReadOnlyList<IFormatter> AvailableFormatter { get; }

        public int SelectedFormatterIndex
        {
            get => selectedFormatterIndex;
            set => this.RaiseAndSetIfChanged(ref selectedFormatterIndex, value);
        }

        private int selectedFormatterIndex;

        public IReadOnlyList<ModeSelection> ModeSelections { get; }

        public int SelectedModeIndex
        {
            get => selectedModeIndex;
            set => this.RaiseAndSetIfChanged(ref selectedModeIndex, value);
        }

        private int selectedModeIndex;

        public string SaveMessage
        {
            get => saveMessage;
            set => this.RaiseAndSetIfChanged(ref saveMessage, value);
        }
        private string saveMessage;

        public ITriggerCommand AddHotfolder { get; }

        public ITriggerCommand EditHotfolder { get; }

        public ITriggerCommand ResetHotfolderChanges { get; }

        public ITriggerCommand SaveHotfolderChanges { get; }

        public HotfolderViewModel()
        {
            List<IFormatter> formatter = DefaultManagerFactory.GetPluginManager().ListPlugins<IFormatter>()
                                                                         .Select(metaData => DefaultManagerFactory.GetPluginManager().LoadPlugin<IFormatter>(metaData))
                                                                         .ToList();
            formatter.Sort((formatterA, formatterB) => formatterA.Information.Name.CompareTo(formatterB.Information.Name));
            AvailableFormatter = formatter;
            ModeSelections = GetModeSelections();
            ISettingPair pair = DefaultManagerFactory.GetSettingsManager().GetSetting("Default", "HotfolderActive");
            HotfolderActive = pair == null ? false : pair.GetValue<bool>();

            AddHotfolder = new RelayCommand(
                parameter =>
                {
                    return HotfolderActive && AvailableFormatter.Count > 0;
                },
                parameter =>
                {
                    SelectedHotfolder = new HotfolderContainerData();
                    SelectedFormatterIndex = 0;
                    SelectedModeIndex = 0;
                    SelectedModeIndex = -1;
                    AddHotfolderMode = true;
                }
            );

            EditHotfolder = new RelayCommand(
                parameter =>
                {
                    return HotfolderActive && SelectedHotfolder != null;
                },
                parameter =>
                {
                    AddHotfolderMode = true;
                }
            );

            ResetHotfolderChanges = new RelayCommand(
                parameter =>
                {
                    SelectedHotfolder = null;
                    AddHotfolderMode = false;
                    SaveMessage = string.Empty;
                }
            );

            SaveHotfolderChanges = new RelayCommand(
                parameter =>
                {
                    return SelectedHotfolder != null;
                },
                parameter =>
                {
                    if (!SelectedHotfolder.IsValid())
                    {
                        SaveMessage = "Configuration for hotfolder is not valid, please check your entries";
                        return;
                    }
                    SaveMessage = string.Empty;
                    AddHotfolderMode = false;
                    SelectedHotfolderIndex = -1;
                }
            );

            Hotfolders = Hotfolders ?? new ObservableCollection<HotfolderContainerData>();
            Hotfolders.Add(new HotfolderContainerData(AvailableFormatter[0], string.Empty));
            SelectedFormatterIndex = 0;
            SelectedModeIndex = 0;
        }

        private IReadOnlyList<ModeSelection> GetModeSelections()
        {
            ModesEnum[] values = (ModesEnum[])Enum.GetValues(typeof(ModesEnum));
            return values.Select(value => new ModeSelection(value.ToString(), value)).ToList();
        }

        public void SaveSettings()
        {
            ISettingsManager settingsManager = DefaultManagerFactory.GetSettingsManager();
            ISettingScope settingScope = settingsManager.GetScope("Hotfolders");
            if (settingScope == null)
            {
                settingScope = new SettingScope("Hotfolders");
                settingsManager.AddScope(settingScope);
            }

            settingScope.ClearSubScopes();
            foreach (SaveableHotfolder saveableHotfolder in Hotfolders.Select(hotfolder => new SaveableHotfolder(hotfolder)))
            {
                ISettingScope subSettingScope = new SettingScope("Hotfolder_" + settingScope.GetSubScopes().Count);
                foreach(ISettingPair settingPair in saveableHotfolder.GetSettingPairs())
                {
                    subSettingScope.AddSetting(settingPair);
                }
                settingScope.AddSubScope(subSettingScope);
            }
            
            settingsManager.Save(DefaultManagerFactory.GetSettingPath());
        }

        private ISettingScope ConvertToSupScope(IHotfolder hotfolder)
        {

            return null;
        }
    }
}
