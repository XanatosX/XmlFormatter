using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Settings;

namespace XmlFormatter.src.Interfaces.Settings.LoadingProvider
{
    public interface ISettingSaveProvider
    {
        bool SaveSettings(ISettingsManager settingsManager, string filePath);
    }
}
