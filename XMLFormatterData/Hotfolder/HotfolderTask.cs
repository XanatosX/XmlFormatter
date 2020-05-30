using XMLFormatterModel.Hotfolder;

namespace XmlFormatterModel.Hotfolder
{
    /// <summary>
    /// This class is a task which needs to be done by the hotfolder manager
    /// </summary>
    public class HotfolderTask
    {
        /// <summary>
        /// Readonly file to convert
        /// </summary>
        private readonly string inputFile;

        /// <summary>
        /// Input file to change
        /// </summary>
        public string InputFile => inputFile;

        /// <summary>
        /// Readonly access to the configuration to use
        /// </summary>
        private readonly IHotfolder configuration;

        /// <summary>
        /// The configuration to use for converting
        /// </summary>
        public IHotfolder Configuration => configuration;

        /// <summary>
        /// Create a new instance of the configuration
        /// </summary>
        /// <param name="inputFile">The file to use as input</param>
        /// <param name="configuration">The configuration to use</param>
        public HotfolderTask(string inputFile, IHotfolder configuration)
        {
            this.inputFile = inputFile;
            this.configuration = configuration;
        }
    }
}
