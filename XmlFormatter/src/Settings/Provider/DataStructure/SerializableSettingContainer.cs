using System.Collections.Generic;

namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    /// <summary>
    /// A saveable settings container
    /// </summary>
    public class SerializableSettingContainer
    {
        /// <summary>
        /// All the scopes in this container
        /// </summary>
        private List<SerializableScope> scopes;

        /// <summary>
        /// Public access to all the scopes in this container
        /// </summary>
        public List<SerializableScope> Scopes
        {
            get => scopes;
            set => scopes = value;
        }

        /// <summary>
        /// Create a new empty instance of this class
        /// </summary>
        public SerializableSettingContainer()
        {
            scopes = new List<SerializableScope>();
        }
    }
}
