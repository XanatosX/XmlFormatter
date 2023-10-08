namespace XmlFormatter.Domain.PluginFeature
{
    /// <summary>
    /// Represents the overhead of a plugin this interface needs to be extend for all manageable plugins
    /// </summary>
    public interface IPluginOverhead
    {
        PluginSettings Settings { get; }

        /// <summary>
        /// The information for the plugin
        /// </summary>
        PluginInformation Information { get; }

        /// <summary>
        /// This method will allow the host application to change the plugin settings
        /// </summary>
        /// <param name="settings"></param>
        void ChangeSettings(PluginSettings settings);
    }
}
