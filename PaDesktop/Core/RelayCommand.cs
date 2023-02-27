using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaDesktop.Core
{
    public class RelayCommand : ICommand
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute=null)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecute is not null ?  _canExecute(parameter) : true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

    }
}
