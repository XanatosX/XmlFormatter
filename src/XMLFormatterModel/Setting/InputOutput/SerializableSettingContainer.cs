using System.Collections.Generic;

namespace XMLFormatterModel.Setting.InputOutput
{
    /// <summary>
    /// A saveable settings container
    /// </summary>
    public class SerializableSettingContainer
    {

        /// <summary>
        /// Public access to all the scopes in this container
        /// </summary>
        public List<SerializableScope> Scopes;

        /// <summary>
        /// Create a new empty instance of this class
        /// </summary>
        public SerializableSettingContainer()
        {
            Scopes = new List<SerializableScope>();
        }
    }
}
