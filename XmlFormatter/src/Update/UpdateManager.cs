using System;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.Interfaces.Updates;

namespace XmlFormatter.src.Update
{
    /// <summary>
    /// Default update manager
    /// </summary>
    class UpdateManager : IUpdater
    {
        private IUpdateStrategy strategy;

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
