using PluginFramework.DataContainer;
using PluginFramework.Formatter;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CorePlugin.Formatter
{
    /// <summary>
    /// A xml formatter class instance
    /// </summary>
    class XmlFormatterProvider : BaseFormatter
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public XmlFormatterProvider() : base(
            "xml",
            new PluginInformation(
                "Xml Formatter",
                "Convert xml files",
                "XanatosX",
                new Version(1, 1),
                "https://github.com/XanatosX",
                "https://github.com/XanatosX/XmlFormatter"))
        {
            Information.SetMarkdownDescription(LoadFromEmbeddedResource("CorePlugin.Resources.XmlFormatterDescription.md"));
        }

        /// <inheritdoc/>
        public override bool ConvertToFlat(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, SaveOptions.DisableFormatting);
            return true;
        }

        /// <inheritdoc/>
        public override bool ConvertToFormatted(string filePath, string outputName)
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
            if (!IsFileReadableWriteable(inputFilePath, outputName))
            {
                FireEvent("Saving failed", "Files where locked!");
                return;
            }

            FireEvent("Loading", "Loading ...");
            XElement? fileToConvert = await Task<XElement>.Run(() =>
            {
                XElement? returnElement;
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
            bool saveSuccess = await Task<bool>.Run(() =>
            {
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
    }
}
