﻿using System;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    internal class ExecuteUpdateStrategyCommand : BaseDataCommand
    {
        public override Task AsyncExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is UpdateApplicationData;
        }

        public override void Execute(object parameter)
        {
            if (parameter is UpdateApplicationData data)
            {
                if (data.Strategy == null)
                {
                    return;
                }
                data.Strategy.Update(data.VersionCompare);
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
