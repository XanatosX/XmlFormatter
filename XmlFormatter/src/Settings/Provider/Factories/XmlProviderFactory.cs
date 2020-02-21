using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Settings.LoadingProvider;

namespace XmlFormatter.src.Settings.Provider.Factories
{
    class XmlProviderFactory : IPersistentFactory
    {
        public ISettingLoadProvider CreateLoader()
        {
            return new XmlLoaderProvider();
        }

        public ISettingSaveProvider CreateSaver()
        {
            return new XmlSaverProvider();
        }
    }
}
