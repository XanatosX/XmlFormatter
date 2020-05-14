using XmlFormatter.src.DataContainer;

namespace XmlFormatter.src.Interfaces.Updates
{
    /// <summary>
    /// An strategy to update the version
    /// </summary>
    interface IUpdateStrategy
    {
        /// <summary>
        /// The name to display in the select box
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Update the application with the current strategy
        /// </summary>
        /// <param name="versionInformation">The information about the new release</param>
        /// <returns>True if update was successful</returns>
        bool Update(VersionCompare versionInformation);
    }
}
