using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.DataLoader;
using XmlFormatterOsIndependent.DataSets.Themes;
using XmlFormatterOsIndependent.DataSets.Themes.LoadableClasses;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Manager;
using XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors;
using XmlFormatterOsIndependent.MVVM.Views.Bars;
using XmlFormatterOsIndependent.MVVM.Views.Main;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Main
{
    class MainWindowViewModel : ReactiveObject, IEventView
    {

        public object TitleBar { get; }
        public object CurrentView
        {
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

        public ICommand OpenUrl { get; }

        public ICommand CloseWindow { get; }

        private bool propertyChangedRegisterd;

        private readonly Lazy<FormatterView> formatterView;
        private readonly Lazy<PluginView> pluginView;
        private readonly Lazy<SettingsView> settingsView;
        private readonly Lazy<AboutView> aboutView;


        public MainWindowViewModel()
        {
            TitleBar = new TitleBar();
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

            OpenUrl = new OpenBrowserUrl();
            propertyChangedRegisterd = false;

            CloseWindow = new RelayCommand(
                        (parameter) =>
                        {
                            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                            {
                                desktop.MainWindow.Close();
                            }
                        });
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
            SetInitalTheme();
        }

        private void SetInitalTheme()
        {
            ISettingPair themeSetting = DefaultManagerFactory.GetSettingsManager().GetSetting("Default", "CurrentTheme");
            if (themeSetting != null)
            {
                IDataLoader<SerializeableThemeContainer> containerLoader = new EmbeddedXmlDataLoader<SerializeableThemeContainer>();
                SerializeableThemeContainer container = containerLoader.Load("XmlFormatterOsIndependent.EmbeddedData.ThemesLibrary.xml");
                ThemeContainer realContainer = container.GetThemeContainer();
                Theme theme = realContainer.Themes.Find(data => data.Name == themeSetting.GetValue<string>());
                ThemeManager.ChangeTheme(theme);
            }
        }

        public void UnregisterEvents(Window currentWindow)
        {
        }
    }
}
