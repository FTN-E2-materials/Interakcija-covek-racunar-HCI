using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthClinic.ViewModels.Commands
{
    public class PorukaCommand : ICommand
    {

        private Action _execute;

        public PorukaCommand(Action execute)
        {
            // Action je delegate koji kaze da ocekujemo metodu kao parametar
            // Action - bez parametara
            // Action<string> - string parametar

            _execute = execute;

        }

        #region Implementacija ICommanda

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();   
        }

        #endregion
    }
}
