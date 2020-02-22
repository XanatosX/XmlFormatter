namespace XmlFormatter.src.Settings.Provider.DataStructure
{
    /// <summary>
    /// This class is a data container to for the xml saving
    /// </summary>
    public class SerializableSetting
    {
        /// <summary>
        /// Public access to hey key/name of this setting
        /// </summary>
        public string Name;

        /// <summary>
        /// Public access to the data string the
        /// </summary>
        public string Value;

        /// <summary>
        /// Public access to the type the value should be castet to
        /// </summary>
        public string Type;
    }
}