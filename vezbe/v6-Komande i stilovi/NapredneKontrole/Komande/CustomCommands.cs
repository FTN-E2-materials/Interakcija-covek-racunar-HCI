using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NapredneKontrole.Komande
{
    public static class CustomCommands
    {
        public static readonly ICommand testKomanda = new TestKomanda();
    }

    public class TestKomanda : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //ovde ide model-level obrada komande, tj. deo
            //koji ne radi sa interfejsom
            Console.WriteLine("Test");
            Console.Beep();
        }
    }
}
