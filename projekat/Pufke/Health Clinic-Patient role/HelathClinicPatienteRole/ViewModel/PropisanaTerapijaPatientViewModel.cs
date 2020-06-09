using C_Validation_ByCustom;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using HelathClinicPatienteRole.Model;
using Syncfusion.Pdf.Tables;
using System.Data;
using Syncfusion.Pdf.Grid;

namespace HelathClinicPatienteRole.ViewModel
{
    class PropisanaTerapijaPatientViewModel : ObservableObject
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        List<string> eventNameCollection;
        List<Brush> colorCollection;
        public PropisanaTerapijaPatientViewModel()
        {
            GenerisiIzvestajCommand = new RelayCommand(GenerisiIzvestaj);
            Meetings = new ObservableCollection<Meeting>();
            CreateEventNameCollection();
            CreateColorCollection();
            CreateAppointments();
        }

        private void CreateAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = GettingTimeRanges();
            DateTime date;
            DateTime DateFrom = DateTime.Now.AddDays(-10);
            DateTime DateTo = DateTime.Now.AddDays(10);
            DateTime dataRangeStart = DateTime.Now.AddDays(-3);
            DateTime dataRangeEnd = DateTime.Now.AddDays(3);


            for (date = DateFrom; date < DateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dataRangeStart) > 0) && (DateTime.Compare(date, dataRangeEnd) < 0))
                {
                    for (int AdditionalAppointmentIndex = 0; AdditionalAppointmentIndex < 3; AdditionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = (randomTime.Next((int)randomTimeCollection[AdditionalAppointmentIndex].X, (int)randomTimeCollection[AdditionalAppointmentIndex].Y));
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = (meeting.From.AddHours(1));
                        meeting.EventName = eventNameCollection[randomTime.Next(9)];
                        meeting.Color = colorCollection[randomTime.Next(9)];
                        if (AdditionalAppointmentIndex % 3 == 0)
                            meeting.AllDay = true;
                        Meetings.Add(meeting);
                    }
                }
                else
                {
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = (meeting.From.AddHours(1));
                    meeting.EventName = eventNameCollection[randomTime.Next(9)];
                    meeting.Color = colorCollection[randomTime.Next(9)];
                    Meetings.Add(meeting);
                }
            }
        }

        /// <summary>  
        /// Creates event names collection.  
        /// </summary>  
        private void CreateEventNameCollection()
        {
            eventNameCollection = new List<string>();
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za krvne sudove");
            eventNameCollection.Add("Brufen");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Masaza");
            eventNameCollection.Add("Panadol");
            eventNameCollection.Add("Lek za imunitet");
            eventNameCollection.Add("Vitamin C");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za pritisak");
        }

        /// <summary>  
        /// Creates color collection.  
        /// </summary>  
        private void CreateColorCollection()
        {
            colorCollection = new List<Brush>();
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1BA1E2")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
        }

        /// <summary>
        /// Gets the time ranges.
        /// </summary>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));
            return randomTimeCollection;
        }



        public RelayCommand GenerisiIzvestajCommand { get; private set; }

        public void GenerisiIzvestaj(object obj)
        {
            using (PdfDocument document = new PdfDocument())
            {
          
                PdfPage page = document.Pages.Add();
                PdfGrid pdfGrid = new PdfGrid();
                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                //Create a header and draw the image.

                System.Drawing.RectangleF bounds = new System.Drawing.RectangleF(0, 0, document.Pages[0].GetClientSize().Width, 50);

                PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
                //Add the header at the top.

                PdfImage image = new PdfBitmap(@"C:\Users\Pufke\Desktop\interakcija-covek-racunar\projekat\Pufke\Health Clinic-Patient role\HelathClinicPatienteRole\Images and Videos\logo.png");

                //Draw the image in the header.

                header.Graphics.DrawImage(image, new System.Drawing.PointF(0, 0), new System.Drawing.SizeF(60, 60));

                document.Template.Top = header;
                //Create a Page template that can be used as footer.

                PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);

                PdfBrush brush = new PdfSolidBrush(System.Drawing.Color.Black);

                //Create page number field.

                PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

                //Create page count field.

                PdfPageCountField count = new PdfPageCountField(font, brush);

                //Add the fields in composite fields.

                PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);

                compositeField.Bounds = footer.Bounds;

                //Draw the composite field in footer.

                compositeField.Draw(footer.Graphics, new System.Drawing.PointF(470, 20));

                //Add the footer template at the bottom.

                document.Template.Bottom = footer;


                table.Columns.Add("Ponedeljak");
                table.Columns.Add("Utorak");
                table.Columns.Add("Sreda");
                table.Columns.Add("Četvrtak");
                table.Columns.Add("Petak");
                table.Columns.Add("Subota");
                table.Columns.Add("Nedelja");

                pdfGrid.DataSource = table;


                int i = 1;
                foreach (string e in eventNameCollection)
                {
                    if (i == 29)
                    {
                        table.Rows.Add(i + " Jun 2020 Terapija: " + e, i+1 + " Jun 2020 Terapija: " + e, "" , "" , "", "" , "" );
                        break;
                    }
                    table.Rows.Add(i + " Jun 2020 Terapija: " + e, i+1 + " Jun 2020 Terapija: "+ e , i+2 + " Jun 2020 Terapija: " + e, i+3 + " Jun 2020 Terapija: " + e, i+4 + " Jun 2020 Terapija: " + e, i+5 + " Jun 2020 Terapija: " + e, i+6 + " Jun 2020 Terapija: " + e);
                    i = i + 7;
                    
                }

                pdfLightTable.DataSource = table;

                PdfLightTableStyle lightTableStyle = new PdfLightTableStyle();
                lightTableStyle.CellPadding = 12;
                lightTableStyle.CellSpacing = 2;
                lightTableStyle.ShowHeader = true;
                lightTableStyle.RepeatHeader = true;

                pdfLightTable.Style = lightTableStyle;

                pdfLightTable.Draw(page, new System.Drawing.PointF(0, 40));

      
                font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);

                page.Graphics.DrawString("Izveštaj o uzimanju terpaija za Pacijenta Marka Markovića", font, PdfBrushes.Black, new System.Drawing.PointF(80, 0));
                page.Graphics.DrawString("Datum generisanja:  " + DateTime.Now, font, PdfBrushes.Black, new System.Drawing.PointF(80, 20));

                document.Save("C:\\Users\\Pufke\\Desktop\\Izvestaj.pdf");
                MessageBox.Show("Izvestaj u vidu kalendara je izgenerisan da Desktop vaseg racunara");
            }
            
        }
    }


   
}
