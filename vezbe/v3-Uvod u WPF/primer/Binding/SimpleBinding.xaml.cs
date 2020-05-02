using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PrimerCas4.Binding
{
    /// <summary>
    /// Interaction logic for SimpleBinding.xaml
    /// </summary>
    public partial class SimpleBinding : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> Test3
        {
            get;
            set;
        }

        #region NotifyProperties
        private string _test1;
        private string _test2;
        public string Test1
        {
            get
            {
                return _test1;
            }
            set
            {
                if (value != _test1)
                {
                    _test1 = value;
                    OnPropertyChanged("Test1");
                }
            }
        }
        public string Test2
        {
            get
            {
                return _test2;
            }
            set
            {
                if (value != _test2)
                {
                    _test2 = value;
                    OnPropertyChanged("Test2");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public SimpleBinding()
        {
            InitializeComponent();
            this.DataContext = this;
            Test1 = "abc";
            Test2 = "def";
            Test3 = new ObservableCollection<string>();
            Test3.Add("A");
            Test3.Add("B");
            Test3.Add("C");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test3.Add("D");
            Test3.Add("E");
            Test3.Add("F");
        }

    }
}
