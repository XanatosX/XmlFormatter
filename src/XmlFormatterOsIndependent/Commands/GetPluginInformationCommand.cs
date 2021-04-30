using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Command to use for getting the plugin information
    /// </summary>
    class GetPluginInformationCommand : BaseTriggerCommand
    {
        /// <summary>
        /// The plugin manager to use for loading the plugin data
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginManager">The plugin manager to use for loading plugin data</param>
        public GetPluginInformationCommand(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter != null && parameter is PluginInformation && pluginManager != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (parameter is PluginInformation pluginInfo)
            {
                CommandExecuted(new PluginInformationArg(pluginInfo));
            }
        }
    }
}
