using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GrainGrowthGui
{
    public class ResetCommand : ICommand
    {
        readonly Func<Boolean> _canExecute;
        readonly Action _execute;

        public ResetCommand(Action execute)
            : this(execute, null)
        {
        }

        public ResetCommand(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }
    }

}
