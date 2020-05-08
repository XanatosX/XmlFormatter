using PluginFramework.src.DataContainer;
using PluginFramework.src.Enums;
using PluginFramework.src.EventMessages;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.IO;

namespace PluginFramework.src.Formatter
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
        public event EventHandler<BaseEventArgs> StatusChanged;

        protected BaseFormatter(string fileExtension, PluginInformation pluginInformation)
        {
            Extension = fileExtension;
            Information = pluginInformation;
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
    }
}
