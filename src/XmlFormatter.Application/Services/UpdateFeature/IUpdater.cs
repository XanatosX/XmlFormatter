using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatter.Application.Services.UpdateFeature
{
    /// <summary>
    /// This interface defines how a application updater should look like
    /// </summary>
    public interface IUpdater
    {
        /// <summary>
        /// Is the strategy for updating set
        /// </summary>
        bool IsStrategySet { get; }

        /// <summary>
        /// Is the version manager set
        /// </summary>
        bool IsVersionManagerSet { get; }

        /// <summary>
        /// Is there a strategy to use to get the newest version
        /// </summary>
        /// <param name="manager">The manager to use to get the newest version</param>
        void SetVersionManager(IVersionManager manager);

        /// <summary>
        /// The strategy to use for the update
        /// </summary>
        /// <param name="updateStrategy">The concrete strategy</param>
        void SetStrategy(IUpdateStrategy updateStrategy);
        /// <summary>
        /// Update this application with the set strategy and version managers
        /// </summary>
        /// <returns>True if the update was successful</returns>
        bool UpdateApplication();

        /// <summary>
        /// Update this application with the strategy
        /// </summary>
        /// <param name="versionInformation">The information about the versions</param>
        /// <returns>True if update was successful</returns>
        bool UpdateApplication(VersionCompare versionInformation);

        /// <summary>
        /// Update this application with the strategy
        /// </summary>
        /// <param name="versionInformation">The information about the versions</param>
        /// <param name="assetFilter">The asset filter to apply</param>
        /// <returns>True if update was successful</returns>
        bool UpdateApplication(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter);
    }
}
