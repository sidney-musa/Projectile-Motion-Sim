using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projectile_Motion_FINAL.ViewModel
{
    /// <summary>
    /// implements ICommand interface to provide functionality to the Launch button
    /// </summary>
    class SubmitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _action;

        public SubmitCommand(Action<object> action) // creating an instance of command
        {
            _action = action;
        }
        public bool CanExecute(object parameter)// can always be executed so always true
        {
            return true;
        }

        public void Execute(object parameter) //execute the action given to this command
        {
            _action(parameter);
        }
    }
}
