using PluginFramework.src.DataContainer;
using PluginFramework.src.Enums;
using PluginFramework.src.EventMessages;
using PluginFramework.src.Interfaces.PluginTypes;
using System;

namespace PluginFramework.src.Formatter
{
    /// <summary>
    /// Base class for formatting
    /// </summary>
    public abstract class BaseFormatter : IFormatter
    {
        /// <inheritdoc/>
        public string Extension => extension;

        /// <summary>
        /// Instance for the extension
        /// </summary>
        private readonly string extension;

        /// <inheritdoc/>
        public PluginInformation Information => information;

        /// <summary>
        /// Instance of the plugin information
        /// </summary>
        private readonly PluginInformation information;

        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> StatusChanged;

        public BaseFormatter(string fileExtension, PluginInformation pluginInformation)
        {
            extension = fileExtension;
            information = pluginInformation;
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
    }
}
