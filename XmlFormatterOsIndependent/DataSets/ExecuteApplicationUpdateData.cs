using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;

namespace XmlFormatterOsIndependent.DataSets
{
    internal class ExecuteApplicationUpdateData
    {
        public IUpdateStrategy Strategy { get; }
        public VersionCompare VersionCompare { get; }

        public ExecuteApplicationUpdateData(IUpdateStrategy strategy, VersionCompare versionCompare)
        {
            Strategy = strategy;
            VersionCompare = versionCompare;
        }

    }
}
