using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.EventMessages;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PluginFramework.Formatter
{
    /// <summary>
    /// Base class for formatting
    /// </summary>
    public abstract class BaseFormatter : IFormatter
    {
        /// <inheritdoc/>
        public string Extension { get; }

        /// <inheritdoc/>
        public PluginInformation Information { get; }

        /// <inheritdoc/>
        public PluginSettings Settings { get; private set; }

        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> StatusChanged;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="fileExtension">The file extension this formatter is using</param>
        /// <param name="pluginInformation">The information for this plugin</param>
        protected BaseFormatter(string fileExtension, PluginInformation pluginInformation)
        {
            Extension = fileExtension;
            Information = pluginInformation;
            Settings = new PluginSettings();
        }

        /// <inheritdoc/>
        public bool ConvertToFormat(string filePath, string outputName, ModesEnum mode)
        {
            return mode == ModesEnum.Flat ? ConvertToFlat(filePath, outputName) : ConvertToFormatted(filePath, outputName);
        }

        /// <inheritdoc/>
        public void ChangeSettings(PluginSettings settings)
        {
            Settings = settings;
        }

        /// <inheritdoc/>
        public abstract bool ConvertToFormatted(string filePath, string outputName);

        /// <inheritdoc/>
        public abstract bool ConvertToFlat(string filePath, string outputName);

        /// <summary>
        /// Fire a new event
        /// </summary>
        /// <param name="caption">The message bodyThe message caption</param>
        /// <param name="message"></param>
        protected void FireEvent(string caption, string message)
        {
            EventHandler<BaseEventArgs> handle = StatusChanged;
            BaseEventArgs dataMessage = new BaseEventArgs(caption, message);
            handle?.Invoke(this, dataMessage);
        }

        /// <summary>
        /// Check if the input file is readable and the output file is writeable
        /// </summary>
        /// <param name="inputFilePath">The input file</param>
        /// <param name="outputName">The output file</param>
        /// <returns>If it is readable</returns>
        protected bool IsFileReadableWriteable(string inputFilePath, string outputName)
        {
            bool readable;
            try
            {
                using (FileStream fileStream = File.Open(
                    inputFilePath,
                    FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.None
                ))
                {
                    fileStream.Close();
                }
                if (File.Exists(outputName))
                {
                    using (FileStream fileStream = File.Open(
                        outputName,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.None
                    ))
                    {
                        fileStream.Close();
                    }
                }
                readable = true;
            }
            catch (Exception)
            {
                readable = false;
            }

            return readable;
        }

        /// <summary>
        /// Method to load information from an resource embedded into the plugin
        /// </summary>
        /// <param name="resourcePath">The path to the embedded resource</param>
        /// <returns>The content of the resource or an empty string if nothing was found</returns>
        protected string LoadFromEmbeddedResource(string resourcePath)
        {
            string returnData = string.Empty;
            Assembly assembly = Assembly.GetCallingAssembly();
            if (assembly is null || string.IsNullOrEmpty(resourcePath))
            {
                return returnData;
            }
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        returnData = reader.ReadToEnd();
                    }
                }
            }


            return returnData;
        }
    }
}
