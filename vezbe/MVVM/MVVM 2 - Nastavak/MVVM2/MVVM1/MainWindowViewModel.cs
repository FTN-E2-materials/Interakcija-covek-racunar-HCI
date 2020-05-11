using MVVM1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM1
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private StudentViewModel studentViewModel = new StudentViewModel();
        private HomeViewModel homeViewModel = new HomeViewModel();
        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homeViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch(destination)
            {
                case "home":
                    CurrentViewModel = homeViewModel;
                    break;
                case "student":
                    CurrentViewModel = studentViewModel;
                    break;
            }
        }
    }
}
