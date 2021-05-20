using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Windows.Input;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors;
using XmlFormatterOsIndependent.MVVM.Views;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class MainWindowViewModelNew : ReactiveObject, IEventView
    {
        public object CurrentView {
            get => currentView;
            private set
            {
                this.RaiseAndSetIfChanged(ref currentView, value);
                if (currentView is UserControl control
                    && control.DataContext is IEventView eventView
                    && Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    eventView.RegisterEvents(desktop.MainWindow);
                }
            }
        }

        private object currentView;

        public ICommand FormattingView { get; }

        public ICommand PluginsView { get; }

        public ICommand SettingsView { get; }

        public ICommand AboutView { get; }

        public ICommand CloseWindow { get; }

        public ICommand MaximizeWindow { get; }

        public ICommand OpenUrl { get; }

        private bool propertyChangedRegisterd;

        private readonly Lazy<FormatterView> formatterView;
        private readonly Lazy<PluginView> pluginView;
        private readonly Lazy<SettingsView> settingsView;
        private readonly Lazy<AboutView> aboutView;


        public MainWindowViewModelNew()
        {
            formatterView = new Lazy<FormatterView>();
            pluginView = new Lazy<PluginView>();
            settingsView = new Lazy<SettingsView>();
            aboutView = new Lazy<AboutView>();

            CurrentView = formatterView.Value;

            FormattingView = new RelayCommand(
                (parameter) => CurrentView = formatterView.Value
                );
            PluginsView = new RelayCommand(
                (parameter) => CurrentView = pluginView.Value
                );
            SettingsView = new RelayCommand(
                (parameter) => CurrentView = settingsView.Value
                );
            AboutView = new RelayCommand(
                (parameter) => CurrentView = aboutView.Value
                );

            CloseWindow = new RelayCommand(
                (parameter) =>
                {
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        desktop.MainWindow.Close();
                    }
                });

            MaximizeWindow = new RelayCommand(
                paramter =>
                {
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        desktop.MainWindow.WindowState = WindowState.Minimized;
                    }
                });

            OpenUrl = new OpenBrowserUrl();
            propertyChangedRegisterd = false;
        }

        public void RegisterEvents(Window currentWindow)
        {
            if (currentWindow == null)
            {
                return;
            }

            if (currentView is UserControl control
                && control.DataContext is IEventView eventView
                && control.DataContext != this)
            {
                eventView.RegisterEvents(currentWindow);
            }

            if (propertyChangedRegisterd)
            {
                return;
            }
            currentWindow.PropertyChanged += (sender, data) =>
            {
                if (data.Property.Name == "WindowState"
                && (WindowState)data.NewValue != WindowState.Normal
                && ((WindowState)data.NewValue == WindowState.Maximized
                || (WindowState)data.NewValue == WindowState.FullScreen))
                {
                    Console.WriteLine("Changed window Size!");
                    currentWindow.WindowState = WindowState.Normal;
                    return;
                }
            };
            propertyChangedRegisterd = true;
        }

        public void UnregisterEvents(Window currentWindow)
        {
        }
    }
}
