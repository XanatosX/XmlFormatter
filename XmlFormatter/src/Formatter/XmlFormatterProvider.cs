using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using XmlFormatter.src.Enums;
using XmlFormatter.src.EventMessages;
using XmlFormatter.src.Interfaces.Formatter;

namespace XmlFormatter.src.Formatter
{
    /// <summary>
    /// A xml formatter class instance
    /// </summary>
    class XmlFormatterProvider : IFormatter
    {
        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> StatusChanged;

        /// <summary>
        /// The readonly name of this formatter
        /// </summary>
        private readonly string name;

        /// <inheritdoc/>
        public string Name => name;

        /// <summary>
        /// The extension supported by this formatter
        /// </summary>
        private readonly string extension;

        /// <inheritdoc/>
        public string Extension => extension;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public XmlFormatterProvider()
        {
            name = "XmlFormatter";
            extension = "xml";
        }

        /// <inheritdoc/>
        public bool ConvertToFormat(string filePath, string outputName, ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.Formatted:
                    return ConvertToFormatted(filePath, outputName);
                case ModesEnum.Flat:
                    return ConvertToFlat(filePath, outputName);
                default:
                    break;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool ConvertToFlat(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, SaveOptions.DisableFormatting);
            return true;
        }

        /// <inheritdoc/>
        public bool ConvertToFormatted(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, SaveOptions.None);
            return true;
        }

        /// <summary>
        /// This method will convert the file 
        /// </summary>
        /// <param name="inputFilePath">The input file to convert</param>
        /// <param name="outputName">The output file to generate</param>
        /// <param name="options">The save options to use</param>
        private async void FormatFile(string inputFilePath, string outputName, SaveOptions options)
        {
            FireEvent("Loading", "Loading ...");
            XElement fileToConvert = await Task<XElement>.Run(() =>
            {
                return XElement.Load(inputFilePath);
            });

            try
            {
                using (FileStream fileStream = File.Open(inputFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fileStream.Close();
                }
                using (FileStream fileStream = File.Open(outputName,FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
                FireEvent("Saving failed", "Files where locked!");
                return;
            }
            FireEvent("Saving", "Saving ...");
            await Task.Run(() => fileToConvert.Save(outputName, options));           

            FireEvent("Done", "Saving done!");
        }

        /// <summary>
        /// Fire a new event
        /// </summary>
        /// <param name="caption">The message bodyThe message caption</param>
        /// <param name="message"></param>
        private void FireEvent(string caption, string message)
        {
            EventHandler<BaseEventArgs> handle = StatusChanged;
            BaseEventArgs dataMessage = new BaseEventArgs(caption, message);
            handle?.Invoke(this, dataMessage);
        }
    }
}
