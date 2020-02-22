namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    /// <summary>
    /// This class is a data container to for the xml saving
    /// </summary>
    public class SerializableSetting
    {
        /// <summary>
        /// The key/name of the setting
        /// </summary>
        private string name;

        /// <summary>
        /// Public access to hey key/name of this setting
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// The data from the setting as a string
        /// </summary>
        private string dataValue;

        /// <summary>
        /// Public access to the data string the
        /// </summary>
        public string Value
        {
            get => dataValue;
            set => dataValue = value;
        }

        /// <summary>
        /// The type the value should be castet to
        /// </summary>
        private string type;

        /// <summary>
        /// Public access to the type the value should be castet to
        /// </summary>
        public string Type
        {
            get => type;
            set => type = value;
        }
    }
}