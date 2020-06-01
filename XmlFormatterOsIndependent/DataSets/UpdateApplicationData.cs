using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;

namespace XmlFormatterOsIndependent.DataSets
{
    internal class UpdateApplicationData
    {
        public IUpdateStrategy Strategy { get; }
        public VersionCompare VersionCompare { get; }

        public UpdateApplicationData(IUpdateStrategy strategy, VersionCompare versionCompare)
        {
            Strategy = strategy;
            VersionCompare = versionCompare;
        }

    }
}
