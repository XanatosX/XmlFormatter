using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (_, _) =>
            {
                Close();
            });

            WeakReferenceMessenger.Default.Register<WindowDeltaDragEvent>(this, (_, e) =>
            {
                if (DataContext is IWindowWithId windowWithId)
                {
                    if (e.windowId == windowWithId.WindowId)
                    {
                        Position = new PixelPoint(Position.X + e.Value.X, Position.Y + e.Value.Y);
                        return;
                    }
                }

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
    }
}
