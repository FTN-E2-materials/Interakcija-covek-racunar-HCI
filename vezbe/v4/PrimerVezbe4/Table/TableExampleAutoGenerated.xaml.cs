﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrimerCas4.Table
{
    /// <summary>
    /// Interaction logic for TableExample2.xaml
    /// </summary>
    public partial class TableExampleAutoGenerated : Window
    {
        private int colNum = 0;
        public ObservableCollection<Student> Studenti
        {
            get;
            set;
        }
        public TableExampleAutoGenerated()
        {
            InitializeComponent();
            this.DataContext = this;
            Studenti = new ObservableCollection<Student>();
            Studenti.Add(new Student() { Ime = "Pera", Prezime = "Peric", Indeks = "RA 12/2016" });
            Studenti.Add(new Student() { Ime = "Mika", Prezime = "Mikic", Indeks = "RA 13/2016" });
            Studenti.Add(new Student() { Ime = "Zika", Prezime = "Zikic", Indeks = "RA 14/2016" });
        }

        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            colNum++;
            if (colNum == 3)
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void obrisiStudenta(object sender, RoutedEventArgs e)
        {
            if (Studenti.Count > 0)
            {
                Studenti.RemoveAt(Studenti.Count - 1);
            }
            else
            {
                MessageBox.Show("Nije moguce brisati iz prazne tabele.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
