using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatter.src.Interfaces.Settings.LoadingProvider
{
    /// <summary>
    /// A factory to get saving and loading provider for the setting manager
    /// </summary>
    public interface IPersistentFactory
    {
        /// <summary>
        /// Create and return the loading prodiver to use
        /// </summary>
        /// <returns>A loading provider which can be used to load files</returns>
        ISettingLoadProvider CreateLoader();

        /// <summary>
        /// Create and return the saving provider to use
        /// </summary>
        /// <returns>A saving provider which can be used to save the files</returns>
        ISettingSaveProvider CreateSaver();
    }
}
