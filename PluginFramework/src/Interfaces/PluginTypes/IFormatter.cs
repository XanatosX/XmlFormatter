using PluginFramework.src.Enums;
using PluginFramework.src.EventMessages;
using System;

namespace PluginFramework.src.Interfaces.PluginTypes
{
    /// <summary>
    /// This interface defines a formatter
    /// </summary>
    public interface IFormatter : IPluginOverhead
    {
        /// <summary>
        /// Event if the status of the conversion has been changed
        /// </summary>
        event EventHandler<BaseEventArgs> StatusChanged;

        /// <summary>
        /// The Extension to use
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Convert the given file to the defined format
        /// </summary>
        /// <param name="filePath">The path to the file to convert</param>
        /// <param name="outputName">The path to the output file</param>
        /// <param name="mode">The mode to use for converting</param>
        /// <returns></returns>
        bool ConvertToFormat(string filePath, string outputName, ModesEnum mode);

        /// <summary>
        /// Convert the given file to a flat format
        /// </summary>
        /// <param name="filePath">The path to the file to convert</param>
        /// <param name="outputName">The path to the output file</param>
        /// <returns></returns>
        bool ConvertToFlat(string filePath, string outputName);

        /// <summary>
        /// Convert the given file to a formattet format
        /// </summary>
        /// <param name="filePath">The path to the file to convert</param>
        /// <param name="outputName">The path to the output file</param>
        /// <returns></returns>
        bool ConvertToFormatted(string filePath, string outputName);
    }
}
