using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Views
{
    public partial class MainWindow : CustomWindowBarWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (_, _) =>
            {
                Close();
            });

            WeakReferenceMessenger.Default.Register<RequestMainWindowMessage>(this, (_, e) =>
            {
                e.Reply(this);
            });

            AddHandler(DragDrop.DragOverEvent, (_, data) =>
            {
                bool result = false;
                try
                {
                    result = CheckIfDragAndDropIsValid(data, result);
                }
                catch (System.Exception)
                {
                    //TODO Add some kind of logging
                }

                data.DragEffects = result ? DragDropEffects.Copy : DragDropEffects.None;
            });

            AddHandler(DragDrop.DropEvent, (_, data) =>
            {
                bool result = false;
                try
                {
                    result = CheckIfDragAndDropIsValid(data, result);
                }
                catch (System.Exception)
                {
                    //TODO Add some kind of logging
                }

                if (!result)
                {
                    return;
                }
                WeakReferenceMessenger.Default.Send(new DragDropFileChanged(data.Data.GetFiles().Select(file => file.Path.AbsolutePath)?.FirstOrDefault()));
            });
        }

        private static bool CheckIfDragAndDropIsValid(DragEventArgs data, bool result)
        {
            string? file = data.Data.GetFiles().Select(file => file.Path.AbsolutePath)?.OfType<string>()?.FirstOrDefault();
            if (file is not null)
            {
                result = WeakReferenceMessenger.Default.Send(new IsDragDropFileValidMessage { FileName = file }) ?? false;
            }

            return result;
        }
    }
}
