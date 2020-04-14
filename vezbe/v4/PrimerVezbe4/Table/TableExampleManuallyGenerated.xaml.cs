using System.Windows;
using System.Collections.ObjectModel;

namespace PrimerCas4.Table
{
    /// <summary>
    /// Interaction logic for TableExample.xaml
    /// </summary>
    public partial class TableExampleManuallyGenerated : Window
    {
        public ObservableCollection<Student> Studenti
        {
            get;
            set;
        }
        public TableExampleManuallyGenerated()
        {
            InitializeComponent();
            this.DataContext = this;
            Studenti = new ObservableCollection<Student>();
            Studenti.Add(new Student {Ime = "Petar", Prezime = "Petrovic", Indeks = "SW 1\\2061" });
            Studenti.Add(new Student { Ime = "Milica", Prezime = "Milicevic", Indeks = "SW 2\\2061" });
            Studenti.Add(new Student { Ime = "Zoran", Prezime = "Zoranovic", Indeks = "SW 3\\2061" });
            Studenti.Add(new Student { Ime = "Suzana", Prezime = "Suzanic", Indeks = "SW 4\\2061" });
            Studenti.Add(new Student { Ime = "Goran", Prezime = "Goranski", Indeks = "SW 5\\2061" });
        }
    }
}
