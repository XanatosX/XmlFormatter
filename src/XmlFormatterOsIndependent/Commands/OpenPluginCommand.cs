using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Models;

namespace XmlFormatterOsIndependent.Commands
{
    class OpenPluginCommand : BaseTriggerCommand
    {
        private readonly IPluginManager pluginManager;

        public OpenPluginCommand(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null && parameter is PluginTreeViewItem && pluginManager != null;
        }

        public override void Execute(object parameter)
        {
            if (parameter is PluginTreeViewItem viewItem)
            {
                CommandExecuted(new PluginInformationArg(viewItem.Information));
            }
        }
    }
}
