using PluginFramework.src.DataContainer;
using PluginFramework.src.Enums;
using PluginFramework.src.EventMessages;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CorePlugin.src.Formatter
{
    /// <summary>
    /// A xml formatter class instance
    /// </summary>
    class XmlFormatterProvider : IFormatter
    {
        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> StatusChanged;

        /// <inheritdoc/>
        public PluginInformation Information => information;

        /// <summary>
        /// The readonly plugin information
        /// </summary>
        private readonly PluginInformation information;

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
            information = new PluginInformation("XmlFormatter", "Convert xml files", "XanatosX", new Version(1, 0));
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
            try
            {
                using (FileStream fileStream = File.Open(
                    inputFilePath,
                    FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.None
                )) {
                    fileStream.Close();
                }
                if (File.Exists(outputName))
                {
                    using (FileStream fileStream = File.Open(
                        outputName,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.None
                    )) {
                        fileStream.Close();
                    }
                }
            }
            catch (Exception)
            {
                FireEvent("Saving failed", "Files where locked!");
                return;
            }

            FireEvent("Loading", "Loading ...");
            XElement fileToConvert = await Task<XElement>.Run(() =>
            {
                XElement returnElement;
                try
                {
                    returnElement = XElement.Load(inputFilePath);
                }
                catch (Exception)
                {
                    FireEvent("Input file not valid", "The input file was not valid!");
                    returnElement = null;
                }
                return returnElement;
            });

            if (fileToConvert == null)
            {
                return;
            }

            FireEvent("Saving", "Saving ...");
            bool saveSuccess = await Task<bool>.Run(() => {
                    try
                    {
                        fileToConvert.Save(outputName, options);
                        return true;
                    }
                    catch (Exception)
                    {
                        FireEvent("Saving did fail", "Saving went wrong, maybe the file was used?");
                        return false;
                    }
                });           

            if (saveSuccess)
            {   
                FireEvent("Done", "Saving done!");
            }
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
