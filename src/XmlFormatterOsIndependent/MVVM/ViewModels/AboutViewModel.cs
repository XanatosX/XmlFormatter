using Avalonia.Controls;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using PluginFramework.Utils;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using XmlFormatterModel.Update.Strategies;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.DataLoader;
using XmlFormatterOsIndependent.DataSets.ThirdParty;
using XmlFormatterOsIndependent.DataSets.ThirdParty.LoadableClasses;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    class AboutViewModel : ReactiveObject
    {
        /// <summary>
        /// The version to show on the screen
        /// </summary>
        public string Version { get; }

        public IReadOnlyList<ThirdClassLibraryData> ThirdPartyLibraries { get; }

        private IDataLoader<SerializeableThirdPartyData> dataLoader;

        private readonly ICommand LinkOpener;

        public ThirdClassLibraryData SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                if (value != null && value.Url != string.Empty)
                {
                    LinkOpener.Execute(value.Url);
                }
            }
        }
        private ThirdClassLibraryData selectedItem;


        public AboutViewModel()
        {
            LocalVersionRecieverStrategy localVersionRecieverStrategy = new LocalVersionRecieverStrategy();
            Task<Version> versionTask = localVersionRecieverStrategy.GetVersion(new DefaultStringConvertStrategy());


            Version version = versionTask.Result;
            Version = version.Major + "." + version.Minor + "." + version.Build;
            List<ThirdClassLibraryData> libraries = GetThirdPartyData().LibraryData.ToList();
            DefaultManagerFactory factory = new DefaultManagerFactory();
            IPluginManager pluginManager = factory.GetPluginManager();
            List<PluginMetaData> plugins = pluginManager.ListPlugins<IFormatter>();
            plugins.AddRange(pluginManager.ListPlugins<IUpdateStrategy>());

            List<PluginInformation> pluginInfos = plugins.Select(meta => meta.Information).ToList();
            foreach(PluginInformation information in pluginInfos)
            {
                libraries.AddRange(
                    information.Libraries.Select(Library => new ThirdClassLibraryData(Library.Name, Library.Author, Library.Url, "Plugin: " + information.Name))
                                        .ToList()
                );
            }
            ThirdPartyLibraries = libraries.Distinct(new ThirdPartyDataComparer())
                                           .OrderBy(library => library.Scope)
                                           .ThenBy(itemA => itemA.Name)
                                           .ToList();
            LinkOpener = new OpenBrowserUrl();
        }

        private ThirdPartyData GetThirdPartyData()
        {
            if (dataLoader == null)
            {
                dataLoader = new EmbeddedXmlDataLoader<SerializeableThirdPartyData>();
            }


            SerializeableThirdPartyData thirdPartyLibraries = dataLoader.Load("XmlFormatterOsIndependent.EmbeddedData.ThirdPartyTools.xml");
            return thirdPartyLibraries.GetImmutableClass();
        }


    }
}
