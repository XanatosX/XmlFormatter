using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatter.src.Interfaces.Settings.LoadingProvider
{
    public interface IPersistentFactory
    {
        ISettingLoadProvider CreateLoader();

        ISettingSaveProvider CreateSaver();
    }
}
