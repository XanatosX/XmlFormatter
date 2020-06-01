using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
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
