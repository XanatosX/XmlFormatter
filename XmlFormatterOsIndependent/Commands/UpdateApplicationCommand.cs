using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    internal class UpdateApplicationCommand : BaseDataCommand
    {
        public override Task AsyncExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is IUpdateStrategy;
        }

        public override void Execute(object parameter)
        {
            if (parameter is IUpdateStrategy strategy)
            {
                strategy.Update(null);
            }
        }

        public override T GetData<T>()
        {
            throw new NotImplementedException();
        }

        public override bool IsExecuted()
        {
            throw new NotImplementedException();
        }
    }
}
