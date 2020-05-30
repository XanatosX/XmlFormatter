﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            IPluginManager manager = new DefaultManager();
            StringBuilder builder = new StringBuilder();


            FileInfo folderInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string folder = folderInfo.DirectoryName;
            builder.AppendFormat("{0}{1}Plugins{1}", folder, Path.DirectorySeparatorChar);
            manager.SetDefaultLoadStrategy(new PluginFolder(builder.ToString()));

            DataContext = new MainWindowViewModel(new ViewContainer(this, this), manager);
        }
    }
}
