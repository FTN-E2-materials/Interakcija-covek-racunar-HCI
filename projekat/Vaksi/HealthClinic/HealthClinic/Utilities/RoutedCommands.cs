using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthClinic.Utilities
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand DodajZaposlenogRouterCommand = new RoutedUICommand(
            "Dodaj zaposlenog",
            "DodajZaposlenogRouterCommand",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D, ModifierKeys.Control),
                new KeyGesture(Key.D, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );
    }
}
