using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.Utilities;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using HealthClinic.Views.Dialogs.Brisanje;
using System.Windows;
using Syncfusion.Pdf.Tables;
using System.Data;
using HealthClinic.Utilities.Validations;
using System.Windows.Controls;
using HealthClinic.Views.Dialogs.ProduzeneInformacije;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment;

namespace HealthClinic.ViewModels
{
    public class LekoviViewModel : ObservableObject
    {
        
        public LekoviViewModel()
        {
            ucitavanjeLekova();
            PieChart();

            DodajLekCommand = new RelayCommand(DodajLek);
            IzmeniLekCommand = new RelayCommand(IzmeniLek);
            GenerisiIzvestajLekaCommand = new RelayCommand(GenerisiIzvestaj);
            IzbrisiLekCommand = new RelayCommand(IzbrisiLek);

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);
            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);
            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);

            odredjivanjeMogucihVrstaLekova();

            NuspojaveCommand = new RelayCommand(PrikaziNuspojave);
            AlergijeCommand = new RelayCommand(PrikaziAlergije);
        }

        



        #region Selektovani lek

        private Lek _selektovaniLek;


        // Bajndujem na SelectedItem u tabeli i dalje radim sa njim sta hocu
        // mogu ga dalje prikazivati
        // a moze se i proslediti u dijalog
        // tako sto se .DatContext tog dijalog postavi na this
        public Lek SelektovaniLek
        {
            get { return _selektovaniLek; }
            set { _selektovaniLek = value; OnPropertyChanged("SelektovaniLek"); }
        }


        #endregion

        #region Lek za izmenu

        private Lek _lekZaIzmenu;

        public Lek LekZaIzmenu
        {
            get { return _lekZaIzmenu; }
            set { _lekZaIzmenu = value; OnPropertyChanged("LekZaIzmenu"); }
        }

        #endregion

        #region Lek za dodavanje

        private Lek _lekZaDodavanje;

        public Lek LekZaDodavanje
        {
            get { return _lekZaDodavanje; }
            set { _lekZaDodavanje = value; OnPropertyChanged("LekZaDodavanje"); }
        }


        #endregion

        #region Trenutno aktivni prozor

        /// <summary>
        /// Promenljiva u kojoj cuvam trenutno otvoreni prozor
        /// kako bih kasnije u komandama u zavisnosti od komande
        /// recimo zatvorio trenutni prozor
        /// </summary>
        private Window _trenutniProzor;

        public Window TrenutniProzor
        {
            get { return _trenutniProzor; }
            set { _trenutniProzor = value; }
        }

        #endregion

        #region Komande

        public RelayCommand PotvrdaBrisanjaPodatakaCommand { get; private set; }
        public RelayCommand PotvrdaDodavanjaPodatakaCommand { get; private set; }
        public RelayCommand PotvrdaIzmenePodatakaCommand { get; private set; }
        public RelayCommand GenerisiIzvestajLekaCommand { get; private set; }
        public RelayCommand DodajLekCommand { get; private set; }
        public RelayCommand IzmeniLekCommand { get; private set; }
        public RelayCommand IzbrisiLekCommand { get; private set; }
        public RelayCommand NuspojaveCommand { get; private set; }
        public RelayCommand AlergijeCommand { get; private set; }

        #endregion

        #region Funkcije koje komande koriste
        public void PotvrdaBrisanjaPodataka(object obj)
        {
            // sprecavam kada nije selektovan lek da ne pukne program
            if (SelektovaniLek is null)
                return;
            foreach (Lek trenutniLek in Lekovi)
            {
                if (trenutniLek.NazivLeka == SelektovaniLek.NazivLeka)
                {
                    MessageBox.Show("Uspesno ste izbrisali lek " + trenutniLek.NazivLeka);
                    podesiBrojOdredjenihLekova(trenutniLek, -1);        // -1 da bih mu smanjio kolicinu vrste ovog leka
                    Lekovi.Remove(trenutniLek);
                    
                    break;
                }
            }
            this.TrenutniProzor.Close();
        }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {

            if (!validanLek(LekZaDodavanje))
                return;

            // dodajem lek u listu lekova
            Lekovi.Add(LekZaDodavanje);
            podesiBrojOdredjenihLekova(LekZaDodavanje,1);

            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
        }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // regulisem da prvo povecam kolicinu novo izmenjene vrste leka
            podesiBrojOdredjenihLekova(LekZaIzmenu, 1);

            // a onda i smanjim od stare vrste
            podesiBrojOdredjenihLekova(SelektovaniLek, -1);

            // provera leka koji je korisnik menjao
            if (!validanLek(LekZaIzmenu))
                return;

            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovaniLek.Kolicina = LekZaIzmenu.Kolicina;
            SelektovaniLek.NazivLeka = LekZaIzmenu.NazivLeka;
            SelektovaniLek.VrstaLeka = LekZaIzmenu.VrstaLeka;
            SelektovaniLek.Alergije = LekZaIzmenu.Alergije;
            SelektovaniLek.Nuspojave = LekZaIzmenu.Nuspojave;

            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
        }


        /// <summary>
        /// Koristan link: https://help.syncfusion.com/file-formats/pdf/working-with-flow-layout?cs-save-lang=1&cs-lang=csharp#working-with-table
        /// </summary>
        /// <param name="obj"></param>
        public void GenerisiIzvestaj(object obj)
        {
            using (WordDocument document = new WordDocument())
            {
                //Adding a new section to the document.
                WSection section = document.AddSection() as WSection;
                //Set Margin of the section
                section.PageSetup.Margins.All = 20;

                
                #region Create paragraph styles
                WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
                style.CharacterFormat.FontName = "Calibri";
                style.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Justify;
                style.CharacterFormat.FontSize = 11f;
                style.ParagraphFormat.AfterSpacing = 8;
                style.ParagraphFormat.FirstLineIndent = 36f;

                style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
                style.ApplyBaseStyle("Normal");
                style.CharacterFormat.FontName = "Calibri Light";
                style.CharacterFormat.FontSize = 16f;
                style.CharacterFormat.TextColor = Color.FromArgb(46, 116, 181);
                #endregion

                #region Appends paragraph
                IWParagraph paragraph = section.AddParagraph();
                paragraph.ApplyStyle("Heading 1");
                paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                paragraph.ParagraphFormat.AfterSpacing = 10;
                WTextRange textRange = paragraph.AppendText("\nIzve\u0161taj stanja lekova Zdravo Korporacije") as WTextRange;
                textRange.CharacterFormat.FontSize = 18f;
                textRange.CharacterFormat.FontName = "Calibri";
                string text =
                " Datum kreiranja izve\u0161taja: " + DateTime.Now.ToShortDateString() + "\n Vreme kreiranja izve\u0161taja: " + DateTime.Now.ToShortTimeString();
                //Appends paragraph.
                paragraph = section.AddParagraph();
                textRange = paragraph.AppendText(text) as WTextRange;



                #endregion

                #region Tabela deo
                // Adding a new Table
                WTable table = section.AddTable() as WTable;
                table.TableFormat.Paddings.All = 2;
                table.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                // Inserting rows to the table.
                table.ResetCells(Lekovi.Count + 2, 4);

                //Appends paragraph with header.
                IWParagraph paragraphTable = table[0, 0].AddParagraph();
                paragraphTable.AppendText("ID leka");

                paragraphTable = table[0, 1].AddParagraph();
                paragraphTable.AppendText("Ime leka");

                paragraphTable = table[0, 2].AddParagraph();
                paragraphTable.AppendText("Tip leka");

                paragraphTable = table[0, 3].AddParagraph();
                paragraphTable.AppendText("Broj lekova");


                // Dodavnje kroz for petlju
                for(int red = 1; red < Lekovi.Count+1; red++)
                {
                    int kolona = 0;
                    int indexElementaNiza = red - 1;

                    paragraphTable = table[red, kolona++].AddParagraph();
                    paragraphTable.AppendText(red.ToString());

                    paragraphTable = table[red, kolona++].AddParagraph();
                    paragraphTable.AppendText(Lekovi[indexElementaNiza].NazivLeka);

                    paragraphTable = table[red, kolona++].AddParagraph();
                    paragraphTable.AppendText(Lekovi[indexElementaNiza].VrstaLeka);

                    paragraphTable = table[red, kolona++].AddParagraph();
                    paragraphTable.AppendText(Lekovi[indexElementaNiza].Kolicina);
                }

                int redova = Lekovi.Count + 1;
                table[redova, 0].CellFormat.HorizontalMerge = CellMerge.Start;
                for(int kolona=1; kolona < 4; kolona++)
                {
                    table[redova, kolona].CellFormat.HorizontalMerge = CellMerge.Continue;
                }

                //Apply built-in table style to the table.
                table.ApplyStyle(BuiltinTableStyle.MediumShading1Accent1);
                #endregion

                //Creates an instance of the DocToPDFConverter
                DocToPDFConverter converter = new DocToPDFConverter();
                //Converts Word document into PDF document
                PdfDocument pdfDocument = converter.ConvertToPDF(document);

                //Save and close the PDF document 
                pdfDocument.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajLekova.pdf");
                pdfDocument.Close(true);
                //Close the document
                document.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajLekova.docx");
                document.Close();
            }
            MessageBox.Show("Uspe\u0161no kreiran izve\u0161taj lekova, mo\u017Eete ga pogledati u teku\u0107em direktorijumu");
        }

        public void DodajLek(object obj)
        {
            // ako bude potvrda za dodavanje ovaj lek cu dodati
            LekZaDodavanje = new Lek();

            // prikaz dijaloga dodavanja leka
            TrenutniProzor = new DodajLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();

        }

        public void IzmeniLek(object obj)
        {
            // Lek za izmenu/stimanje preuzima podatke od selektovanog leka
            if (!(SelektovaniLek is null))
            {
                LekZaIzmenu = new Lek()
                {
                    NazivLeka = SelektovaniLek.NazivLeka,
                    Kolicina = SelektovaniLek.Kolicina,
                    SifraLeka = SelektovaniLek.SifraLeka,
                    VrstaLeka = SelektovaniLek.VrstaLeka,
                    Nuspojave = SelektovaniLek.Nuspojave,
                    Alergije = SelektovaniLek.Alergije
                    
                };
            }
            else
            {
                return;
            }
                



            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmenaLekaDijalog();
            TrenutniProzor.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            TrenutniProzor.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        public void IzbrisiLek(object obj)
        {
            if (SelektovaniLek is null)
                return;

            // TODO: Mozda dodati nekad da pise tacno koji lek se brise ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void PrikaziNuspojave(object obj)
        {
            TrenutniProzor = new NuspojaveDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void PrikaziAlergije(object obj)
        {
            TrenutniProzor = new AlergijeDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();

        }

        #endregion

        #region Tabela & vrste lekova
        
        private ObservableCollection<Lek> _lek;

        public ObservableCollection<Lek> Lekovi
        {
            get { return _lek; }
            set { _lek = value; OnPropertyChanged("Lekovi"); }
        }

        private void ucitavanjeLekova()
        {
            Lekovi = new ObservableCollection<Lek>();
            Lekovi.Add(new Lek() { NazivLeka = "Andol", SifraLeka = "0x21FDAF", Kolicina = "10" , VrstaLeka="antibiotik"});
            Lekovi.Add(new Lek() { NazivLeka = "Sabax", SifraLeka = "0x22FDAF", Kolicina = "11", VrstaLeka = "analgetik" });
            Lekovi.Add(new Lek() { NazivLeka = "Kafetin", SifraLeka = "0x23FDAF", Kolicina = "12", VrstaLeka = "kardio vaskularni" });
            Lekovi.Add(new Lek() { NazivLeka = "Bensedin", SifraLeka = "0x24FDAF", Kolicina = "13", VrstaLeka = "anestetik" });
            
            // Odredjivanje koliko imamo kog leka
            foreach (Lek lek in Lekovi)
            {
                podesiBrojOdredjenihLekova(lek,1);
            }

        }
        /// <summary>
        /// Podesavam trenutnu kolicinu odredjene GRUPE LEKOVA
        /// </summary>
        /// <param name="lek"></param>
        /// <param name="koeficijentPravca"> Prosledjuje se broj koji govori koliko povecavam/smanjujem odredjenog leka</param>
        private void podesiBrojOdredjenihLekova(Lek lek, int koeficijentPravca)
        {
            if (lek.VrstaLeka == "antibiotik")
            {
                if (this.UkupnoAntibiotika is null)
                    BrojacAntibiotika = 1;
                else
                    BrojacAntibiotika += koeficijentPravca;
                this.UkupnoAntibiotika = new ChartValues<int>() { BrojacAntibiotika };

            }
            else if (lek.VrstaLeka == "analgetik")
            {
                if (this.UkupnoAnalgetika is null)
                    BrojacAnalgetika = 1;
                else
                    BrojacAnalgetika += koeficijentPravca;
                this.UkupnoAnalgetika = new ChartValues<int>() { BrojacAnalgetika };
            }
            else if (lek.VrstaLeka == "kardio vaskularni")
            {
                if (this.UkupnoKardioVaskularnih is null)
                    BrojacKardioVaskularnih = 1;
                else
                    BrojacKardioVaskularnih += koeficijentPravca;
                this.UkupnoKardioVaskularnih = new ChartValues<int>() { BrojacKardioVaskularnih };
            }
            else if (lek.VrstaLeka == "anestetik")
            {
                if (this.UkupnoAnestetika is null)
                    BrojacAnestetika = 1;
                else
                    BrojacAnestetika += koeficijentPravca;
                this.UkupnoAnestetika = new ChartValues<int>() { BrojacAnestetika };
            }
        }

        #endregion

        #region Brojaci odredjene vrste lekova

        /// <summary>
        /// Potreban mi je i brojac koji ce se upisivati u cart,
        /// ne moze direktno i samo brojac da bude ali ni cart.
        /// </summary>
        private int _brojacAnalgetika;
        private int _brojacAnestetika;
        private int _brojacKardioVaskularnih;
        private int _brojacAntibiotika;


        public int BrojacAnalgetika
        {
            get { return _brojacAnalgetika; }
            set { _brojacAnalgetika = value; OnPropertyChanged("BrojacAnalgetika"); }
        }

        public int BrojacAnestetika
        {
            get { return _brojacAnestetika; }
            set { _brojacAnestetika = value; OnPropertyChanged("BrojacAnestetika"); }
        }

        public int BrojacKardioVaskularnih
        {
            get { return _brojacKardioVaskularnih; }
            set { _brojacKardioVaskularnih = value; OnPropertyChanged("BrojacKardioVaskularnih"); }
        }

        public int BrojacAntibiotika
        {
            get { return _brojacAntibiotika; }
            set { _brojacAntibiotika = value; OnPropertyChanged("BrojacAntibiotika"); }
        }

        #endregion

        #region Deo vezan za grafikon

        private ChartValues<int> _ukupnoAntibiotika;
        private ChartValues<int> _ukupnoAnalgetika;
        private ChartValues<int> _ukupnoKardioVaskularnih;
        private ChartValues<int> _ukupnoAnestetika;

       
        public ChartValues<int> UkupnoAntibiotika
        {
            get { return _ukupnoAntibiotika; }
            set { _ukupnoAntibiotika = value; OnPropertyChanged("UkupnoAntibiotika"); }
        }

        public ChartValues<int> UkupnoAnalgetika
        {
            get { return _ukupnoAnalgetika; }
            set { _ukupnoAnalgetika = value; OnPropertyChanged("UkupnoAnalgetika"); }
        }

        public ChartValues<int> UkupnoKardioVaskularnih
        {
            get { return _ukupnoKardioVaskularnih; }
            set { _ukupnoKardioVaskularnih = value; OnPropertyChanged("UkupnoKardioVaskularnih"); }
        }
        
        public ChartValues<int> UkupnoAnestetika
        {
            get { return _ukupnoAnestetika; }
            set { _ukupnoAnestetika = value; OnPropertyChanged("UkupnoAnestetika"); }
        }


        public Func<ChartPoint, string> LabelPoint { get; set; }

        private void PieChart()
        {
            LabelPoint = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);

        }

        #endregion

        #region Moguce vrste lekova 

        private ObservableCollection<string> _moguceVrsteLekova;

        public ObservableCollection<string> MoguceVrsteLekova
        {
            get { return _moguceVrsteLekova; }
            set { _moguceVrsteLekova = value; OnPropertyChanged("MoguceVrsteLekova"); }
        }

        private void odredjivanjeMogucihVrstaLekova()
        {
            MoguceVrsteLekova = new ObservableCollection<string>();
            MoguceVrsteLekova.Add("analgetik");
            MoguceVrsteLekova.Add("antibiotik");
            MoguceVrsteLekova.Add("kardio vaskularni");
            MoguceVrsteLekova.Add("anestetik");
        }

        #endregion


        #region Validiranje lekova
        private bool validanLek(Lek lek)
        {
            if (lek.Kolicina is null)
            {
                MessageBox.Show("Niste uneli kolicinu leka");
                return false;
            }

            if (lek.NazivLeka is null)
            {
                MessageBox.Show("Niste uneli naziv leka");
                return false;
            }

            if (lek.VrstaLeka is null)
            {
                MessageBox.Show("Niste uneli vrstu leka");
                return false;
            }


            // TODO: Kako da proverim da li nasa aplikacija ima ErrorContenta da bih bacio poruku greske ovde neku
            

            

            return true;
        }

        #endregion

        #region Singlton pattern
        private static LekoviViewModel instance = null;
        private static readonly object padlock = new object();


        public static LekoviViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LekoviViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
