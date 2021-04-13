using System.Collections.Generic;

namespace XMLFormatterModel.Setting.InputOutput
{
    /// <summary>
    /// This class is a data container to for the xml saving
    /// </summary>
    public class SerializableScope
    {
        /// <summary>
        /// Public access to the scope name
        /// </summary>
        public string Name;

        /// <summary>
        /// Public access to the namespace of the class this container was created from
        /// </summary>
        public string ClassType;

        /// <summary>
        /// Public access to the sub scopes
        /// </summary>
        public List<SerializableScope> SubScopes;

        /// <summary>
        /// Public access to all the settings in this scope
        /// </summary>
        public List<SerializableSetting> Settings;

        /// <summary>
        /// Create a new instance of this scope
        /// </summary>
        public SerializableScope()
        {
            Settings = new List<SerializableSetting>();
            SubScopes = new List<SerializableScope>();
        }
    }
}
