using MVVM1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM1.ViewModel
{
    public class StudentViewModel : BindableBase
    {
        public ObservableCollection<Student> Students { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        private Student selectedStudent;
        private string fnText;
        private string lnText;

        public StudentViewModel()
        {
            LoadStudents();
            DeleteCommand = new MyICommand(OnDelete,CanDelete);
            AddCommand = new MyICommand(OnAdd);
        }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public void LoadStudents()
        {
            ObservableCollection<Student> students = 
                new ObservableCollection<Student>();

            students.Add(new Student { FirstName = "Petar", LastName = "Petrovic" });
            students.Add(new Student { FirstName = "Marko", LastName = "Markovic" });
            students.Add(new Student { FirstName = "Jovan", LastName = "Jovanovic" });

            Students = students;
        }

        private bool CanDelete()
        {
            return SelectedStudent != null;
        }

        private void OnDelete()
        {
            Students.Remove(SelectedStudent);
        }

        public string FNText
        {
            get { return fnText; }
            set
            {
                if(fnText!=value)
                {
                    fnText = value;
                    OnPropertyChanged("FNText");
                }
            }
        }

        public string LNText
        {
            get { return lnText; }
            set
            {
                if (lnText != value)
                {
                    lnText = value;
                    OnPropertyChanged("LNText");
                }
            }
        }

        private void OnAdd()
        {
            Students.Add(new Student { FirstName = FNText, LastName = LNText });
        }
    }
}
