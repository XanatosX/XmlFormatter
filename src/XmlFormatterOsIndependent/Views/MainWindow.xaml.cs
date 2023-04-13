using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (_, _) =>
            {
                Close();
            });

            WeakReferenceMessenger.Default.Register<RequestMainWindowMessage>(this, async (_, e) =>
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
                WeakReferenceMessenger.Default.Send(new DragDropFileChanged(data.Data.GetFileNames()?.FirstOrDefault()));
            });
        }

        private static bool CheckIfDragAndDropIsValid(DragEventArgs data, bool result)
        {
            string? file = data.Data.GetFileNames()?.OfType<string>()?.FirstOrDefault();
            if (file is not null)
            {
                result = WeakReferenceMessenger.Default.Send(new IsDragDropFileValidMessage { FileName = file }) ?? false;
            }

            return result;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
