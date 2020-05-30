using PluginFramework.DataContainer;

namespace PluginFramework.Interfaces.PluginTypes
{
    /// <summary>
    /// An strategy to update the version
    /// </summary>
    public interface IUpdateStrategy : IPluginOverhead
    {
        /// <summary>
        /// Update the application with the current strategy
        /// </summary>
        /// <param name="versionInformation">The information about the new release</param>
        /// <returns>True if update was successful</returns>
        bool Update(VersionCompare versionInformation);
    }
}
