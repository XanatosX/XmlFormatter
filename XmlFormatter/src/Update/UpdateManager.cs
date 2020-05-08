using PluginFramework.src.DataContainer;
using PluginFramework.src.Interfaces.PluginTypes;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.Interfaces.Updates;

namespace XmlFormatter.src.Update
{
    /// <summary>
    /// Default update manager
    /// </summary>
    class UpdateManager : IUpdater
    {
        /// <summary>
        /// The strategy to use
        /// </summary>
        private IUpdateStrategy strategy;

        /// <summary>
        /// Is there a update strategy set
        /// </summary>
        public bool IsStrategySet => strategy != null;

        /// <inheritdoc/>
        public void SetStrategy(IUpdateStrategy updateStrategy)
        {
            strategy = updateStrategy;
        }

        /// <inheritdoc/>
        public bool UpdateApplication(VersionCompare versionInformation)
        {
            if (strategy == null)
            {
                return false;
            }

            return strategy.Update(versionInformation);
        }
    }
}
