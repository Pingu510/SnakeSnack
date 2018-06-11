using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SnakeSnack.ViewModels.Commands
{
    class DelegateCommand : ICommand

    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public DelegateCommand(Action<object> execute, Predicate<object> canexecute)
        {
            _execute = execute;
            _canExecute = canexecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return false;

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
