using PluginFramework.DataContainer;
using System;

namespace XmlFormatterOsIndependent.EventArg
{
    /// <summary>
    /// Event argurment class to store the information for a single plugin
    /// </summary>
    class PluginInformationArg : EventArgs
    {
        /// <summary>
        /// The stored information of the plugin
        /// </summary>
        public PluginInformation Information { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="information">The information to transfere to the event method</param>
        public PluginInformationArg(PluginInformation information)
        {
            Information = information;
        }
    }
}
