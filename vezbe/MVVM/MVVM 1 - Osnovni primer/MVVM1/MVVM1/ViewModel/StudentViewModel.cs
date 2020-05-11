using MVVM1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM1.ViewModel
{
    public class StudentViewModel
    {
        public ObservableCollection<Student> Students { get; set; }

        public StudentViewModel()
        {
            LoadStudents();
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
    }
}
