using PluginFramework.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.MVVM.Models;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Commands.Conversion;
using System.Threading.Tasks;
using System.Threading;
using Avalonia.Input;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class XmlFormatterViewModel : ReactiveObject
    {
        public ITriggerCommand LoadDocument { get; }
        public ITriggerCommand SaveDocument { get; }
        public ITriggerCommand ClearFile { get; }

        public IReadOnlyList<ModeSelection> ModeSelections { get; }

        public IReadOnlyList<PluginMetaData> FormatterPlugins { get; }

        private string selectedFile;

        public string SelectedFile
        {
            get => selectedFile;
            set
            {
                SaveDocument?.DataHasChanged();
                this.RaiseAndSetIfChanged(ref selectedFile, value);
            }
        }


        public bool TextBoxVisible
        {
            get => SelectedFile != string.Empty;
            set
            {
                ClearFile.DataHasChanged();
                this.RaiseAndSetIfChanged(ref textBoxVisible, value);
            }
        }
        private bool textBoxVisible;

        private bool statusVisible;

        public bool StatusVisible
        {
            get => statusVisible;
            set
            {
                ClearFile.DataHasChanged();
                this.RaiseAndSetIfChanged(ref statusVisible, value);
            }
        }

        private string statusText;

        public string StatusText
        {
            get => statusText;
            set
            {
                this.RaiseAndSetIfChanged(ref statusText, value);
            }
        }

        private readonly DefaultManagerFactory managerFactory;

        public XmlFormatterViewModel()
        {
            SelectedFile = string.Empty;
            managerFactory = new DefaultManagerFactory();
            ModeSelections = GetModeSelections();
            IPluginManager pluginManager = managerFactory.GetPluginManager();
            FormatterPlugins = pluginManager.ListPlugins<IFormatter>();
            LoadDocument = new SelectConversionFile(pluginManager);
            LoadDocument.ContinueWith += (sender, data) =>
            {
                if (data is FileSelectedArg fileSelectedArg)
                {
                    SelectedFile = fileSelectedArg.SelectedFile;
                    TextBoxVisible = SelectedFile.Length > 0;
                }
            };
            SaveDocument = new ConvertFileCommand(pluginManager, (sender, data) =>
            {
                StatusVisible = true;
                StatusText = data.Message;
            });

            SaveDocument.ContinueWith += (sender, data) =>
            {
                Task.Run(() => Thread.Sleep(5000)).ContinueWith((data) =>
                {
                    StatusVisible = false;
                });

            };
            ClearFile = new RelayCommand((parameter) => !StatusVisible, 
                (parameter) => {
                    SelectedFile = string.Empty;
                    TextBoxVisible = false;
                }
            );
        }

        private IReadOnlyList<ModeSelection> GetModeSelections()
        {
            ModesEnum[] values = (ModesEnum[])Enum.GetValues(typeof(ModesEnum));
            return values.Select(value => new ModeSelection(value.ToString(), value)).ToList();
        }

        public void DragOverEvent(object sender, DragEventArgs eventArgs)
        {

        }
        public void DropEvent(object sender, DragEventArgs eventArgs)
        {

        }
    }
}
