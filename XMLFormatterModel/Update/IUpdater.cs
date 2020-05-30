using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// This interface defines how a application updater should look like
    /// </summary>
    public interface IUpdater
    {
        bool IsStrategySet { get; }

        /// <summary>
        /// The strategy to use for the update
        /// </summary>
        /// <param name="updateStrategy">The concrete strategy</param>
        void SetStrategy(IUpdateStrategy updateStrategy);

        /// <summary>
        /// Update this application with the strategy
        /// </summary>
        /// <param name="versionInformation">The information about the versions</param>
        /// <returns>True if update was successful</returns>
        bool UpdateApplication(VersionCompare versionInformation);
    }
}
