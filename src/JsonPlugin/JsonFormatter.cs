using PluginFramework.DataContainer;
using PluginFramework.Formatter;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonPlugin
{
    /// <summary>
    /// A foramtter to convert json files
    /// </summary>
    public class JsonFormatter : BaseFormatter
    {
        /// <summary>
        /// Create a new instance of this formatter
        /// </summary>
        public JsonFormatter()
            : base(
                  "json",
                  new PluginInformation(
                      "Json formatter",
                      "This plugin will convert json files",
                      "XanatosX",
                      new Version(1, 1),
                      "https://github.com/XanatosX",
                      "https://github.com/XanatosX/XmlFormatter"
                      )
                  )
        {
            Information.SetMarkdownDescription(LoadFromEmbeddedResource("JsonPlugin.Resources.Description.md"));
        }

        /// <inheritdoc/>
        public override bool ConvertToFlat(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, false);
            return true;
        }

        /// <inheritdoc/>
        public override bool ConvertToFormatted(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, true);
            return true;
        }

        /// <summary>
        /// This method will convert the file 
        /// </summary>
        /// <param name="inputFilePath">The input file to convert</param>
        /// <param name="outputName">The output file to generate</param>
        /// <param name="options">The save options to use</param>
        private async void FormatFile(string inputFilePath, string outputName, bool Indent)
        {
            if (!IsFileReadableWriteable(inputFilePath, outputName))
            {
                FireEvent("Saving failed", "Files where locked!");
                return;
            }

            FireEvent("Loading", "Loading ...");
            dynamic? data = await Task.Run(() =>
            {
                dynamic? returnData = null;
                try
                {
                    returnData = JsonSerializer.Deserialize<dynamic>(File.ReadAllText(inputFilePath));
                }
                catch (Exception)
                {
                    FireEvent("Input file not valid", "The input file was not valid!");
                    returnData = null;
                }
                return returnData;
            });

            if (data is null)
            {
                return;
            }

            FireEvent("Saving", "Saving ...");
            bool saveSuccess = await Task<bool>.Run(() =>
            {
                try
                {
                    string writeableData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(outputName, writeableData);
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
