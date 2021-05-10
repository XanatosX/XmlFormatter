using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.MVVM.Views;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class MainWindowViewModelNew : ReactiveObject
    {
        public object CurrentView {
            get => currentView;
            private set
            {
                this.RaiseAndSetIfChanged(ref currentView, value);
            }
        }

        private object currentView;

        public ICommand FormattingView { get; }

        public ICommand PluginsView { get; }


        public ICommand SettingsView { get; }


        public ICommand AboutView { get; }

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
                (parameter) => true,
                (parameter) => CurrentView = formatterView.Value
                );
            PluginsView = new RelayCommand(
                (parameter) => true,
                (parameter) => CurrentView = pluginView.Value
                );
            SettingsView = new RelayCommand(
                (parameter) => true,
                (parameter) => CurrentView = settingsView.Value
                );
            AboutView = new RelayCommand(
                (parameter) => true,
                (parameter) => CurrentView = aboutView.Value
                );
        }
    }
}
