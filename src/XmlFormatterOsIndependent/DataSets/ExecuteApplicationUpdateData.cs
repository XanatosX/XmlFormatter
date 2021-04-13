using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Update application data class
    /// </summary>
    internal class ExecuteApplicationUpdateData
    {
        /// <summary>
        /// The strategy used for updating
        /// </summary>
        public IUpdateStrategy Strategy { get; }

        /// <summary>
        /// The version compare container used for the update
        /// </summary>
        public VersionCompare VersionCompare { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="strategy">The strategy used for updating</param>
        /// <param name="versionCompare">The data set with the compare data</param>
        public ExecuteApplicationUpdateData(IUpdateStrategy strategy, VersionCompare versionCompare)
        {
            Strategy = strategy;
            VersionCompare = versionCompare;
        }

    }
}
