using System;
using System.Windows.Input;

namespace StylusQualificationSelector
{
    class RelayCommand : ICommand
    {
        private Action action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action newAction)
        {
            action = newAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
