namespace PluginFramework.DataContainer
{
    /// <summary>
    /// Data for any third party library
    /// </summary>
    public class ThirdPartyLibrary
    {
        /// <summary>
        /// Name of the library
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Author of the library
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Url to the library
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="name">Name of the library</param>
        /// <param name="author">Name of the library creator</param>
        /// <param name="url">Url to the library or empty if unknown</param>
        public ThirdPartyLibrary(string name, string author, string url)
        {
            Name = name.Trim();
            Author = author.Trim();
            Url = url.Trim();
        }
    }
}
