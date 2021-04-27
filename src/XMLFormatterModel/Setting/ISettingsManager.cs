using System.Collections.Generic;

namespace XmlFormatterModel.Setting
{
    /// <summary>
    /// This interface is a representation of a settings manager class
    /// </summary>
    public interface ISettingsManager
    {
        /// <summary>
        /// Set the factory to get the loading and savings providers
        /// </summary>
        /// <param name="factory">The factory to use</param>
        void SetPersistendFactory(IPersistentFactory factory);

        /// <summary>
        /// Add a new scope to this manager. If the name already exist the old will be overwritten.
        /// </summary>
        /// <param name="newScope">The new scope to add</param>
        void AddScope(ISettingScope newScope);

        /// <summary>
        /// Remove a scope from the manager
        /// </summary>
        /// <param name="name">The name of the scope tp remove</param>
        /// <returns>True if removing was successful</returns>
        bool RemoveScope(string name);

        /// <summary>
        /// Get a scope by there name/key
        /// </summary>
        /// <param name="name">They key/name of the scope to get</param>
        /// <returns>Will return the scope or null if nothing was found</returns>
        ISettingScope GetScope(string name);

        /// <summary>
        /// Get all the scopes saved in this manager
        /// </summary>
        /// <returns>A List with all the scopes in this manager</returns>
        List<ISettingScope> GetScopes();

        /// <summary>
        /// Remove all the scopes from this manager
        /// </summary>
        void RemoveScopes();

        /// <summary>
        /// Save every scope in this manager to a file
        /// You need to set the persistend factory first!
        /// </summary>
        /// <param name="filePath">The file path to save the data to</param>
        /// <returns>True if saving was successful</returns>
        bool Save(string filePath);

        /// <summary>
        /// Load all scopes from a file into this manager instance
        /// </summary>
        /// <param name="filePath">The file path to load</param>
        /// <returns>True if loading was successful</returns>
        bool Load(string filePath);
    }
}