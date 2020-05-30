using Avalonia.Controls;
using ReactiveUI;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string TextBoxText => textBoxText.ToString();
        private StringBuilder textBoxText;

        public string CurrentFile
        {
            get => currentFilePath;
            set => this.RaiseAndSetIfChanged(ref currentFilePath, value);
        }
        private string currentFilePath;
        private readonly ViewContainer view;

        public MainWindowViewModel(ViewContainer view)
        {
            textBoxText = new StringBuilder();
            textBoxText.AppendFormat("Select {0}-file path", "empty");

            currentFilePath = string.Empty;
            this.view = view;
        }

        public void LoadFileCommand()
        {
            IDataCommand openFile = new OpenFileCommand();
            List<FileDialogFilter> filters = new List<FileDialogFilter>();
            List<string> extensions = new List<string>();
            extensions.Add("xml");
            filters.Add(new FileDialogFilter()
            {
                Name = "XML",
                Extensions = extensions
            }) ;
            FileDialogData data = new FileDialogData(view.GetWindow(), filters);
            if (openFile.CanExecute(data))
            {
                openFile.AsyncExecute(data);
                openFile.Executed += OpenFile_Executed;
            }
        }

        private void OpenFile_Executed(object sender, System.EventArgs e)
        {
            if (sender is IDataCommand command)
            {
                CurrentFile = command.IsExecuted() ? command.GetData<string>() : currentFilePath;
            }
            
        }
    }
}
