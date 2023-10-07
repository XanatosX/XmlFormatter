using System;
using System.Collections.Generic;

namespace XmlFormatterModel.Setting
{
    /// <summary>
    /// This class is the default settings manager
    /// </summary>
    [Obsolete("Please use the repository Instead")]
    public class SettingsManager : ISettingsManager
    {
        /// <summary>
        /// All the scopes in this manager
        /// </summary>
        private readonly List<ISettingScope> scopes;

        /// <summary>
        /// The factory to use for getting the saving and loading provider
        /// </summary>
        private IPersistentFactory persistentFactory;

        /// <summary>
        /// The current loading provider to use
        /// </summary>
        private ISettingLoadProvider loadProvider;

        /// <summary>
        /// The current saving provider to use
        /// </summary>
        private ISettingSaveProvider saveProvider;


        public SettingsManager(IPersistentFactory persistentFactory, ISettingLoadProvider loadProvider, ISettingSaveProvider saveProvider)
        {
            this.persistentFactory = persistentFactory;
            this.loadProvider = loadProvider;
            this.saveProvider = saveProvider;
            scopes = new List<ISettingScope>();
        }

        /// <summary>
        /// Create a new empty instance of this manager
        /// </summary>
        public SettingsManager() : this(null, null, null)
        {
        }


        /// <inheritdoc/>
        public void SetPersistentFactory(IPersistentFactory factory)
        {
            persistentFactory = factory;
            loadProvider = null;
            saveProvider = null;
        }

        /// <inheritdoc/>
        public void AddScope(ISettingScope newScope)
        {
            scopes.RemoveAll((currentScope) =>
            {
                return currentScope.Name == newScope.Name;
            });
            scopes.Add(newScope);
        }

        /// <inheritdoc/>
        public ISettingScope GetScope(string name)
        {
            return scopes.Find((currentScope) =>
            {
                return currentScope.Name == name;
            });
        }

        /// <inheritdoc/>
        public List<ISettingScope> GetScopes()
        {
            return scopes;
        }

        /// <inheritdoc/>
        public bool Load(string filePath)
        {
            if (persistentFactory == null)
            {
                return false;
            }
            loadProvider = loadProvider ?? persistentFactory.CreateLoader();
            List<ISettingScope> newScopes = loadProvider.LoadSettings(filePath);
            scopes.Clear();
            foreach (ISettingScope scopeToAdd in newScopes)
            {
                scopes.Add(scopeToAdd);
            }
            return true;
        }

        /// <inheritdoc/>
        public bool Save(string filePath)
        {
            if (persistentFactory == null)
            {
                return false;
            }
            saveProvider = saveProvider ?? persistentFactory.CreateSaver();
            return saveProvider.SaveSettings(this, filePath);
        }

        /// <inheritdoc/>
        public bool RemoveScope(string name)
        {
            int removed = scopes.RemoveAll(scope =>
            {
                return scope.Name == name;
            });

            return removed > 0;
        }

        /// <inheritdoc/>
        public void RemoveScopes()
        {
            scopes.Clear();
        }
    }
}
