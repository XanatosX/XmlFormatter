using XmlFormatter.src.DataContainer;

namespace XmlFormatter.src.Interfaces.Updates
{
    /// <summary>
    /// This interface defines how a application updater should look like
    /// </summary>
    interface IUpdater
    {
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
