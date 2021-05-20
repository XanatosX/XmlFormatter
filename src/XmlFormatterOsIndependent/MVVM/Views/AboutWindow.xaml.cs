﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.MVVM.ViewModels;

namespace XmlFormatterOsIndependent.MVVM.Views
{
    public class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DefaultManagerFactory managerFactory = new DefaultManagerFactory();

            DataContext = new AboutWindowViewModel(new ViewContainer(this, this), managerFactory.GetSettingsManager(), managerFactory.GetPluginManager());
        }
    }
}