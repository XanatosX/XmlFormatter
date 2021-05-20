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
using XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors;
using System.IO;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class XmlFormatterViewModel : ReactiveObject, IEventView
    {
        private static string DROP_FILE_IDLE_TEXT = "Drop file here";
        private static string DROP_FILE_HOVER_TEXT = "Release file now";
        private static string WRONG_DROP_FILE_FORMAT_TEXT = "Wrong file format";
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

        private PluginMetaData currentPlugin;
        public PluginMetaData CurrentPlugin
        {
            get => currentPlugin;
            set
            {
                ClearFile?.DataHasChanged();
                ClearFile?.Execute(null);
                SaveDocument?.DataHasChanged();
                this.RaiseAndSetIfChanged(ref currentPlugin, value);
            }
        }

        private string dropFileText;

        public string DropFileText
        {
            get => dropFileText;
            set => this.RaiseAndSetIfChanged(ref dropFileText, value);
            }

        //



        private readonly DefaultManagerFactory managerFactory;

        public XmlFormatterViewModel()
        {
            SelectedFile = string.Empty;
            managerFactory = new DefaultManagerFactory();
            ModeSelections = GetModeSelections();
            IPluginManager pluginManager = managerFactory.GetPluginManager();
            FormatterPlugins = pluginManager.ListPlugins<IFormatter>();
            CurrentPlugin = FormatterPlugins.FirstOrDefault();
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

            DropFileText = DROP_FILE_IDLE_TEXT;
        }

        private IReadOnlyList<ModeSelection> GetModeSelections()
        {
            ModesEnum[] values = (ModesEnum[])Enum.GetValues(typeof(ModesEnum));
            return values.Select(value => new ModeSelection(value.ToString(), value)).ToList();
        }

        public void RegisterEvents(Avalonia.Controls.Window currentWindow)
        {
            if (currentWindow == null)
            {
                return;
            }
            currentWindow.AddHandler(DragDrop.DragOverEvent, (sender, data) =>
            {
                if (!CheckDragDropFile(data))
                {
                    DropFileText = WRONG_DROP_FILE_FORMAT_TEXT;
                    data.DragEffects = DragDropEffects.None;
                    return;
                }

                DropFileText = DROP_FILE_HOVER_TEXT;
                data.DragEffects = DragDropEffects.Copy;
            });

            currentWindow.AddHandler(DragDrop.DragLeaveEvent, (sender, data) => DropFileText = DROP_FILE_IDLE_TEXT);

            currentWindow.AddHandler(DragDrop.DropEvent, (sender, data) =>
            {
                if (!CheckDragDropFile(data))
                {
                    return;
                }
                SelectedFile = data.Data.GetFileNames().First();
                TextBoxVisible = true;
                DropFileText = DROP_FILE_IDLE_TEXT;
            });
        }
        /// <summary>
        /// Check if the file which was dragged dropped into is correct
        /// </summary>
        /// <param name="data">The dataset to check</param>
        /// <returns>True if the file is valid</returns>
        private bool CheckDragDropFile(DragEventArgs data)
        {
            
            IFormatter currentFormatter = managerFactory.GetPluginManager().LoadPlugin<IFormatter>(CurrentPlugin);
            IReadOnlyList<string> files = (List<string>)data.Data.GetFileNames();
            if (currentFormatter == null
                || files.Count == 0
                || files.Count > 1)
            {
                return false;
            }
            string firstFile = files.First();
            FileInfo info = new FileInfo(firstFile);
            if (info.Extension.Replace(".", string.Empty) != currentFormatter.Extension)
            {
                return false;
            }
            return true;
        }


        public void UnregisterEvents(Avalonia.Controls.Window currentWindow)
        {
            
        }
    }
}
