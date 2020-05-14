using Newtonsoft.Json;
using PluginFramework.src.DataContainer;
using PluginFramework.src.Formatter;
using System;
using System.IO;
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
                  new PluginInformation("Json formatter", "This plugin will convert json files", "XanatosX", new Version(1, 0))
                  )
        {
        }

        /// <inheritdoc/>
        public override bool ConvertToFlat(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, Formatting.None);
            return true;
        }

        /// <inheritdoc/>
        public override bool ConvertToFormatted(string filePath, string outputName)
        {
            FormatFile(filePath, outputName, Formatting.Indented);
            return true;
        }

        /// <summary>
        /// This method will convert the file 
        /// </summary>
        /// <param name="inputFilePath">The input file to convert</param>
        /// <param name="outputName">The output file to generate</param>
        /// <param name="options">The save options to use</param>
        private async void FormatFile(string inputFilePath, string outputName, Formatting formatting)
        {
            if (!IsFileReadableWriteable(inputFilePath, outputName))
            {
                FireEvent("Saving failed", "Files where locked!");
                return;
            }

            FireEvent("Loading", "Loading ...");
            object data = await Task<object>.Run(() =>
            {
                object returnData = null;
                try
                {
                    
                    returnData = JsonConvert.DeserializeObject(File.ReadAllText(inputFilePath));
                }
                catch (Exception)
                {
                    FireEvent("Input file not valid", "The input file was not valid!");
                    returnData = null;
                }
                return returnData;
            });

            if (data == null)
            {
                return;
            }

            FireEvent("Saving", "Saving ...");
            bool saveSuccess = await Task<bool>.Run(() => {
                try
                {
                    string writeableData = JsonConvert.SerializeObject(data, formatting);
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
