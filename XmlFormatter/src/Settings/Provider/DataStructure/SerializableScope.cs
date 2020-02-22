using System.Collections.Generic;

namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    /// <summary>
    /// This class is a data container to for the xml saving
    /// </summary>
    public class SerializableScope
    {
        /// <summary>
        /// Name of the scope
        /// </summary>
        private string name;

        /// <summary>
        /// Public access to the scope name
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Namespace of the class this container was created from
        /// </summary>
        private string classType;

        /// <summary>
        /// Public access to the namespace of the class this container was created from
        /// </summary>
        public string ClassType
        {
            get => classType;
            set => classType = value;
        }

        /// <summary>
        /// All the sub scopes in this container
        /// </summary>
        private List<SerializableScope> subScopes;

        /// <summary>
        /// Public access to the sub scopes
        /// </summary>
        public List<SerializableScope> SubScopes
        {
            get => subScopes;
            set => subScopes = value;
        }

        /// <summary>
        /// All the settings in this scope
        /// </summary>
        private List<SerializableSetting> settings;

        /// <summary>
        /// Public access to all the settings in this scope
        /// </summary>
        public List<SerializableSetting> Settings
        {
            get => settings;
            set => settings = value;
        }

        /// <summary>
        /// Create a new instance of this scope
        /// </summary>
        public SerializableScope()
        {
            settings = new List<SerializableSetting>();
            subScopes = new List<SerializableScope>();
        }
    }
}
