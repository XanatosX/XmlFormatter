using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Settings;

namespace XmlFormatter.src.Interfaces.Settings.LoadingProvider
{
    /// <summary>
    /// This interface is representing a save provider for the settings manager
    /// </summary>
    public interface ISettingSaveProvider
    {
        /// <summary>
        /// Save all the scope which are in the manager
        /// </summary>
        /// <param name="settingsManager">The manager to save</param>
        /// <param name="filePath">The file path to save the data to</param>
        /// <returns>True if saving was a success</returns>
        bool SaveSettings(ISettingsManager settingsManager, string filePath);
    }
}
