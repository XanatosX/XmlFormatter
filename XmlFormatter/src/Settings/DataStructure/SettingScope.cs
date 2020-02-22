using System.Collections.Generic;
using XmlFormatter.src.Interfaces.Settings.DataStructure;

namespace XmlFormatter.src.Settings.DataStructure
{
    /// <inheritdoc/>
    class SettingScope : ISettingScope
    {
        /// <summary>
        /// The key/name for this scope
        /// </summary>
        private string name;

        /// <inheritdoc/>
        public string Name => name;

        /// <summary>
        /// All the sub scopes for this specific scopes
        /// This is a readonly list
        /// </summary>
        private readonly List<ISettingScope> subScopes;

        /// <summary>
        /// All the settings in this scope
        /// The list is readonly
        /// </summary>
        private readonly List<ISettingPair> settings;

        /// <summary>
        /// Create a new instance with empty lists
        /// </summary>
        public SettingScope()
        {
            settings = new List<ISettingPair>();
            subScopes = new List<ISettingScope>();
        }

        /// <summary>
        /// Create a new instance and set a name
        /// </summary>
        /// <param name="name"></param>
        public SettingScope(string name)
            : this()
        {
            SetName(name);
        }

        /// <inheritdoc/>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <inheritdoc/>
        public void AddSetting(ISettingPair setting)
        {
            ISettingPair stored = GetSetting(setting.Name);
            if (stored != null)
            {
                stored.SetValue(setting.Value);
                return;
            }

            settings.Add(setting);
        }

        /// <inheritdoc/>
        public ISettingPair GetSetting(string name)
        {
            return settings.Find((currentObject) =>
            {
                return currentObject.Name == name;
            });
        }

        /// <inheritdoc/>
        public List<ISettingPair> GetSettings()
        {
            return settings;
        }

        /// <inheritdoc/>
        public void AddSubScope(ISettingScope scope)
        {
            subScopes.RemoveAll((currentScope) =>
            {
                return currentScope.Name == scope.Name;
            });
            subScopes.Add(scope);
        }

        /// <inheritdoc/>
        public ISettingScope GetSubScope(string name)
        {
            return subScopes.Find((currentScope) =>
            {
                return currentScope.Name == name;
            });
        }

        /// <inheritdoc/>
        public List<ISettingScope> GetSubScopes()
        {
            return subScopes;
        }
    }
}
