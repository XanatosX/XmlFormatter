using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.Commands
{
    class RelayCommand : BaseTriggerCommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public RelayCommand(Action<object> execute)
            :this(parameter => true, execute)
        {
        }
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        public override bool CanExecute(object parameter)
        {
            return canExecute != null ? canExecute(parameter) : false;
        }

        public override void Execute(object parameter)
        {
            if (execute != null && CanExecute(parameter))
            {
                execute(parameter);
            }
        }
    }
}
