using System.Collections.Generic;

namespace XmlFormatter.src.Interfaces.Settings.DataStructure
{
    /// <summary>
    /// A setting scope containing multiple settings
    /// </summary>
    public interface ISettingScope
    {
        /// <summary>
        /// The name of the scope used for saving
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A method to set the name of the scope, used from the loading providers
        /// </summary>
        /// <param name="name"></param>
        void SetName(string name);

        /// <summary>
        /// Add a new setting pair to this scope
        /// </summary>
        /// <param name="setting">The new setting to add to this scope</param>
        void AddSetting(ISettingPair setting);

        /// <summary>
        /// Add a sub scope to this scope
        /// </summary>
        /// <param name="scope">The new scope to add as sub scope</param>
        void AddSubScope(ISettingScope scope);

        /// <summary>
        /// Get a specific sub scope by name
        /// </summary>
        /// <param name="name">The name of the sub scope to return</param>
        /// <returns>The matching sub scope or null if nothing was found</returns>
        ISettingScope GetSubScope(string name);

        /// <summary>
        /// Get all the sub scopes in this scope
        /// </summary>
        /// <returns>A list with all sub scopes</returns>
        List<ISettingScope> GetSubScopes();

        /// <summary>
        /// Get a single setting by there key/name
        /// </summary>
        /// <param name="name">The key/name of the setting</param>
        /// <returns>The setting or null if nothing was found</returns>
        ISettingPair GetSetting(string name);

        /// <summary>
        /// Get all the settings from this scope container
        /// </summary>
        /// <returns></returns>
        List<ISettingPair> GetSettings();
    }
}