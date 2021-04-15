using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// Default update manager
    /// </summary>
    public class UpdateManager : IUpdater
    {
        /// <summary>
        /// The strategy to use
        /// </summary>
        private IUpdateStrategy strategy;

        /// <summary>
        /// Is there a update strategy set
        /// </summary>
        public bool IsStrategySet => strategy != null;

        /// <summary>
        /// The version manager to use
        /// </summary>
        private IVersionManager versionManager;

        /// <inheritdoc/>
        public bool IsVersionManagerSet => versionManager != null;

        /// <inheritdoc/>
        public void SetStrategy(IUpdateStrategy updateStrategy)
        {
            strategy = updateStrategy;
        }

        /// <inheritdoc/>
        public void SetVersionManager(IVersionManager manager)
        {
            versionManager = manager;
        }

        /// <inheritdoc/>
        public bool UpdateApplication()
        {
            Task<VersionCompare> compare = versionManager.RemoteVersionIsNewer();
            TaskAwaiter<VersionCompare> awaiter = compare.GetAwaiter();
            bool returnValue = false;
            awaiter.OnCompleted(() =>
            {
                returnValue = UpdateApplication(awaiter.GetResult());
            });
            return returnValue;
        }

        /// <inheritdoc/>
        public bool UpdateApplication(VersionCompare versionInformation)
        {
            if (strategy == null)
            {
                return false;
            }

            return strategy.Update(versionInformation, (asset) => true);
        }

        /// <inheritdoc/>
        public bool UpdateApplication(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter)
        {
            if (strategy == null)
            {
                return false;
            }

            return strategy.Update(versionInformation, assetFilter);
        }


    }
}
