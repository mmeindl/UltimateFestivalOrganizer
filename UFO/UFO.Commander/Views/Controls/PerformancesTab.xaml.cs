using MigraDoc.DocumentObjectModel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UFO.Commander.ViewModels;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.Views.Controls
{
    /// <summary>
    /// Interaktionslogik für PerformancesTab.xaml
    /// </summary>
    public partial class PerformancesTab : UserControl
    {
        private IUFOServer server;
        private PerformanceVM selectedPerformanceVM;

        private bool isDragging;

        const string fontFamStr = "Verdana";
        XFont fntHeadline = new XFont(fontFamStr, 20, XFontStyle.Bold);
        XFont fntTimes = new XFont(fontFamStr, 14);
        XFont fntAreas = new XFont(fontFamStr, 18);
        XFont fntVenues = new XFont(fontFamStr, 14);
        XFont fntArtist = new XFont(fontFamStr, 6);
        XFont fntCountry = new XFont(fontFamStr, 6);
        XFont fntAbbreviation = new XFont(fontFamStr, 14);
        XFont fntLegend = new XFont(fontFamStr, 10);

        public PerformancesTab()
        {
            BLType type = (BLType)Enum.Parse(typeof(BLType), ConfigurationManager.AppSettings["BLType"]);

            server = UFOServerFactory.GetUFOServer(type);

            InitializeComponent();

            isDragging = false;
        }

        private void drag_performance(object sender, MouseButtonEventArgs e)
        {
            PerformanceCollectionVM performanceCollectionVM = ((FrameworkElement)sender).DataContext as PerformanceCollectionVM;

            DataGridCell cell = GetCell((DependencyObject)e.OriginalSource);

            GetPerformanceFromCell(cell);

            Cursor = Cursors.Cross;

            isDragging = true;
        }

        private void drop_performance(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                PerformanceVM performanceVMtoDrop = selectedPerformanceVM;

                DataGridCell cell = GetCell((DependencyObject)e.OriginalSource);

                if (GetPerformanceFromCell(cell) && selectedPerformanceVM.Performance.Id == 0 && performanceVMtoDrop != null)
                {
                    Performance p = new Performance(performanceVMtoDrop.Performance.DateTime.Date +
                                                    new TimeSpan(selectedPerformanceVM.Performance.DateTime.Hour, 0, 0),
                                                    selectedPerformanceVM.Performance.VenueId,
                                                    performanceVMtoDrop.Performance.ArtistId,
                                                    performanceVMtoDrop.Performance.Id);
                    bool success = false;

                    try
                    {
                        success = server.UpdatePerformance(p);
                    }
                    catch (Exception exc)
                    {
                        // nothing to do
                    }

                    if (success)
                    {
                        PerformanceVM dropSpot = new PerformanceVM(new Performance(performanceVMtoDrop.Performance.DateTime.Date +
                                                                                   new TimeSpan(selectedPerformanceVM.Performance.DateTime.Hour, 0, 0),
                                                                                   selectedPerformanceVM.Performance.VenueId,
                                                                                   performanceVMtoDrop.Performance.ArtistId,
                                                                                   performanceVMtoDrop.Performance.Id),
                                                                   selectedPerformanceVM.PerformanceRowVM,
                                                                   server);

                        PerformanceVM dragSpot = new PerformanceVM(new Performance(selectedPerformanceVM.Performance.DateTime.Date +
                                                                                   new TimeSpan(performanceVMtoDrop.Performance.DateTime.Hour, 0, 0),
                                                                                   performanceVMtoDrop.Performance.VenueId,
                                                                                   selectedPerformanceVM.Performance.ArtistId,
                                                                                   selectedPerformanceVM.Performance.Id),
                                                                   performanceVMtoDrop.PerformanceRowVM,
                                                                   server);

                        PerformanceRowVM dropRow = dropSpot.PerformanceRowVM;
                        int perfIndex1 = dropSpot.Performance.DateTime.Hour - 14;
                        dropRow.VenuePerformances.RemoveAt(perfIndex1);
                        dropRow.VenuePerformances.Insert(perfIndex1, dropSpot);

                        PerformanceRowVM dragRow = dragSpot.PerformanceRowVM;
                        int perfIndex2 = dragSpot.Performance.DateTime.Hour - 14;
                        dragRow.VenuePerformances.RemoveAt(perfIndex2);
                        dragRow.VenuePerformances.Insert(perfIndex2, dragSpot);
                        
                        if (dropRow.VenueName == dragRow.VenueName)
                        {
                            IList<PerformanceRowVM> rows = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.PerformanceRows;
                            int index = rows.IndexOf(dropRow);

                            rows.RemoveAt(index);
                            rows.Insert(index, dropRow);
                        }
                        else
                        {
                            IList<PerformanceRowVM> rows = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.PerformanceRows;
                            int index1 = rows.IndexOf(dropRow);
                            int index2 = rows.IndexOf(dragRow);

                            rows.RemoveAt(index1);
                            rows.Insert(index1, dropRow);

                            rows.RemoveAt(index2);
                            rows.Insert(index2, dragRow);
                        }
                    }
                }

                Cursor = Cursors.Arrow;
                performanceVMtoDrop = null;
                selectedPerformanceVM = null;
                isDragging = false;
            }
        }

        private void remember_performance(object sender, MouseEventArgs e)
        {
            DataGridCell cell = GetCell((DependencyObject)e.OriginalSource);
            GetPerformanceFromCell(cell);
        }

        private void AddPerformance(object sender, RoutedEventArgs e)
        {
            PerformanceVM performanceVM = selectedPerformanceVM;

            PerformanceArtistsWindow performanceArtistWindow = new PerformanceArtistsWindow();
            performanceArtistWindow.ShowDialog();

            
            
            
            try
            {
                var artist = performanceArtistWindow.Artist;
                Performance p = new Performance(selectedPerformanceVM.PerformanceRowVM.PerformanceCollectionVM.CurrentDate.Date +
                                                new TimeSpan(performanceVM.Performance.DateTime.Hour, 0, 0),
                                                performanceVM.Performance.VenueId, artist.Id);

                bool success = server.InsertPerformance(p);

                if (success)
                {
                    p = server.FindPerformanceByDateTimeAndArtistId(p.DateTime, p.ArtistId);

                    PerformanceVM pVM = new PerformanceVM(p, performanceVM.PerformanceRowVM, server);

                    PerformanceRowVM row = pVM.PerformanceRowVM;
                    int perfIndex = pVM.Performance.DateTime.Hour - 14;
                    row.VenuePerformances.RemoveAt(perfIndex);
                    row.VenuePerformances.Insert(perfIndex, pVM);

                    IList<PerformanceRowVM> rows = pVM.PerformanceRowVM.PerformanceCollectionVM.PerformanceRows;
                    int index = rows.IndexOf(row);

                    rows.RemoveAt(index);
                    rows.Insert(index, row);
                }
            }
            catch (Exception exc)
            {
                // nothing to do
            }

            selectedPerformanceVM = null;
        }

        private void RemovePerformance(object sender, RoutedEventArgs e)
        {
            PerformanceVM performanceVM = selectedPerformanceVM;

            try
            {
                bool success = server.DeletePerformance(performanceVM.Performance);

                if (success)
                {
                    PerformanceVM p = new PerformanceVM(new Performance(new DateTime(2000, 1, 1) + new TimeSpan(performanceVM.Performance.DateTime.Hour, 0, 0),
                                                                        performanceVM.Performance.VenueId, 0),
                                                        selectedPerformanceVM.PerformanceRowVM,
                                                        server);

                    PerformanceRowVM row = p.PerformanceRowVM;
                    int perfIndex = p.Performance.DateTime.Hour - 14;
                    row.VenuePerformances.RemoveAt(perfIndex);
                    row.VenuePerformances.Insert(perfIndex, p);

                    IList<PerformanceRowVM> rows = p.PerformanceRowVM.PerformanceCollectionVM.PerformanceRows;
                    int index = rows.IndexOf(row);

                    rows.RemoveAt(index);
                    rows.Insert(index, row);
                }
            }
            catch (Exception exc)
            {
                // nothing to do
            }

            selectedPerformanceVM = null;
        }

        private void EditMedia(object sender, RoutedEventArgs e)
        {
            PerformanceMediaWindow pmw = new PerformanceMediaWindow();
            pmw.DataContext = new PerformanceMediaCollectionVM(selectedPerformanceVM.Performance,
                UFOServerFactory.GetUFOServer());
            pmw.ShowDialog();

            selectedPerformanceVM = null;
        }


        private void SendEmail(object sender, RoutedEventArgs e)
        {

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential =
                new NetworkCredential("ufo.swk@gmx.at", "?ufoswk1");

            MailAddress fromAddress = new MailAddress("ufo.swk@gmx.at");

            smtpClient.Host = "mail.gmx.net";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            System.Net.Mail.Attachment attachment;
            CreatePDF();
            attachment = new System.Net.Mail.Attachment("../../bin/Debug/programm.pdf");

            DateTime date = Convert.ToDateTime(cmbPerformanceDay.SelectedValue.ToString());

            foreach (Artist artist in server.FindAllArtists())
            {
                MailMessage message = new MailMessage();
                message.Attachments.Add(attachment);

                message.From = fromAddress;
                message.Subject = "Programm of " + date.ToString().Substring(0, 10);
                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;


                message.Body = CreateMessageBody(artist, date);
                message.To.Add(artist.Email);

                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //Error, could not send the message
                    //MessageBox.Show(ex.Message, "Error");
                }
            }
            MessageBox.Show("E-mails were sent!", "Success");

        }

        private string CreateMessageBody(Artist artist, DateTime date)
        {
            string bodyStr = "<h1>Your appointments: " + artist.Name + "</h1>";
            bodyStr += "<ul>";
            foreach (Performance performance in server.FindPerformancesByDateAndArtist(date, artist))
            {
                bodyStr += "<li>";
                bodyStr += performance.DateTime.ToString().Substring(11, 5);
                bodyStr += " @ ";
                bodyStr += server.FindVenueById(performance.VenueId).Name;
                bodyStr += "</li>";
            }

            bodyStr += "</ul>";


            return bodyStr;
        }

        private void CreatePDF()
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            DateTime date = Convert.ToDateTime(cmbPerformanceDay.SelectedValue.ToString());

            foreach (Area area in server.FindAllAreas())
            {

                PdfPage page = document.AddPage();
                page.Orientation = PageOrientation.Landscape;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                DrawHeadline(0, 15, page, gfx, date);//20
                DrawArea(0, 60, page, gfx, area.Name);//18
                DrawTimes(40, 85, page, gfx);//14
                DrawContent(5, 120, page, gfx, area.Id, date); //16 144
                DrawVenueLegend(20, 550, page, gfx, area.Id);
            }

            // Save the document...
            try
            {
                const string filename = "programm.pdf";
                document.Save(filename);
            }
            catch (Exception exc)
            {
                MessageBox.Show("unable to close");
            }

        }

        private void DrawVenueLegend(double x, double y, PdfPage page, XGraphics gfx, int areaId)
        {
            string abbrevStr;
            string venueStr;
            double xPos = x;
            double yPos = y;
            double xWidth = 100;
            double yHeight = 12;

            foreach (Venue venue in server.FindVenuesByAreaId(areaId))
            {
                abbrevStr = venue.ShortName;
                venueStr = venue.Name;
                gfx.DrawString(abbrevStr, fntLegend, XBrushes.Black,
                    new XRect(xPos, yPos, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(venueStr, fntLegend, XBrushes.Black,
                    new XRect(xPos, yPos + yHeight, page.Width, page.Height), XStringFormats.TopLeft);
                xPos += xWidth;
            }
        }

        private void DrawHeadline(double x, double y, PdfPage page, XGraphics gfx, DateTime date)
        {
            string headlineStr = "Performance of the day " + date.ToString().Substring(0, 10);
            gfx.DrawString(headlineStr, fntHeadline, XBrushes.Black,
                new XRect(x, y, page.Width, page.Height), XStringFormats.TopCenter);
        }

        private void DrawArea(double x, double y, PdfPage page, XGraphics gfx, string name)
        {
            gfx.DrawString(name, fntAreas, XBrushes.Black,
                new XRect(x, y, page.Width, page.Height), XStringFormats.TopCenter);
        }

        private void DrawTimes(double x, double y, PdfPage page, XGraphics gfx)
        {
            int time = 14;
            string timeStr;
            for (int i = 0; i < 810; i += 90)
            {
                timeStr = time.ToString() + " - " + (time + 1).ToString();
                gfx.DrawString(timeStr, fntTimes, XBrushes.Black,
                    new XRect(x + i, y, page.Width, page.Height), XStringFormats.TopLeft);
                time++;
            }

        }

        private void DrawContent(double x, double y, PdfPage page, XGraphics gfx, int areaId, DateTime date)
        {
            string artistStr;
            string countryStr;
            double xPos = x;
            double yPos = y;
            double xWidth = 90;
            double yHeight = 8;
            double yLineHeight = 40;
            double xWidthFirstRow = 40;
            double xIndent = 3;
            int curHour;

            foreach (Venue venue in server.FindVenuesByAreaId(areaId))
            {
                gfx.DrawString(venue.ShortName, fntAbbreviation, XBrushes.Black,
                    new XRect(xPos, yPos, page.Width, page.Height), XStringFormats.TopLeft);

                xPos += xWidthFirstRow;
                curHour = 14;

                foreach (Performance performance in server.FindPerformancesByDateAndVenue(date, venue))
                {

                    while (performance.DateTime.Hour != curHour)
                    {
                        curHour++;
                        xPos += xWidth;
                    }
                    artistStr = server.FindArtistById(performance.ArtistId).Name;
                    countryStr = "(" + server.FindArtistById(performance.ArtistId).CountryId.ToUpper() + ")";
                    gfx.DrawString(artistStr, fntArtist, XBrushes.Black,
                        new XRect(xPos, yPos, page.Width, page.Height), XStringFormats.TopLeft);
                    gfx.DrawString(countryStr, fntCountry, XBrushes.Black,
                        new XRect(xPos + xIndent, yPos + yHeight, page.Width, page.Height), XStringFormats.TopLeft);
                    curHour++;
                    xPos += xWidth;
                }

                yPos += yLineHeight;
                xPos = x;
            }

        }

        private void TestPdf(object sender, RoutedEventArgs e)
        {
            CreatePDF();
        }

        // Helpers

        private DataGridCell GetCell(DependencyObject dep)
        {
            while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            return dep as DataGridCell;
        }

        private DataGridRow GetRow(DependencyObject cell)
        {
            while ((cell != null) && !(cell is DataGridRow))
            {
                cell = VisualTreeHelper.GetParent(cell);
            }

            return cell as DataGridRow;
        }

        private int GetRowIndex(DataGridRow row)
        {
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);

            return index;
        }

        private bool GetPerformanceFromCell(DataGridCell cell)
        {
            const int STARTCOLIDX = 2;

            if (cell == null)
            {
                return false;
            }

            int colIndex = cell.Column.DisplayIndex;
            DataGridRow row = GetRow(cell);

            if (colIndex >= STARTCOLIDX)
            {
                PerformanceRowVM performanceRowVM = (PerformanceRowVM)row.Item;
                selectedPerformanceVM = performanceRowVM.VenuePerformances.ElementAt(colIndex - STARTCOLIDX);
                return true;
            }

            selectedPerformanceVM = null;
            return false;

        }
    }
}
