using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace Common
{
    public class CommandAction : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;

        public CommandAction(Action SomeAction)
        {
            _action = SomeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
            
        }
    }
}
