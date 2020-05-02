using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NapredneKontrole.Komande
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand HelloWorld = new RoutedUICommand(
            "Hello World",
            "HelloWorld",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control),
                new KeyGesture(Key.H, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Enable = new RoutedUICommand(
            "Enable",
            "Enable",
            typeof(RoutedCommands)
            );

        public static readonly RoutedUICommand Komanda = new RoutedUICommand(
            "Komanda",
            "Komanda",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.K, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand Ugradjene = new RoutedUICommand(
            "Ugradjene Komande",
            "UgradjeneKomande",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.U, ModifierKeys.Control),
            }
            );
    }
}
