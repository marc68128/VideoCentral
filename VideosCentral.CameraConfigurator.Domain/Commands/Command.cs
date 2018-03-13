using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideosCentral.CameraConfigurator.Domain.Commands
{
    public class Command : ICommand
    {
        #region Private fields

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        #endregion

        #region Constructors

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (o => true);
        }

        #endregion

        #region Public methods

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
