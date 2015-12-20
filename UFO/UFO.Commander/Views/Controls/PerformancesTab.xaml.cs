using MigraDoc.DocumentObjectModel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
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
            server = UFOServerFactory.GetUFOServer();

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
                    Performance p = performanceVMtoDrop.Performance;
                        p.DateTime =
                        performanceVMtoDrop.Performance.DateTime.Date +
                        new TimeSpan(selectedPerformanceVM.Performance.DateTime.Hour, 0, 0);

                    p.VenueId = selectedPerformanceVM.Performance.VenueId;

                    bool success = false;

                    try
                    {
                        success = server.UpdatePerformance(p);
                    }
                    catch (Exception exc)
                    {
                        // inform user?
                    }

                    if (success)
                    {
                        /*
                        DateTime curDate = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.CurrentDate;
                        Area curArea = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.CurrentArea;

                        this.DataContext = new PerformanceCollectionVM(
                            UFOServerFactory.GetUFOServer());
*/
                        /*
                        PerformanceVM helperPerformance = selectedPerformanceVM;
                        
                        selectedPerformanceVM.Performance.ArtistId = performanceVMtoDrop.Performance.ArtistId;
                        selectedPerformanceVM.Performance.VenueId = performanceVMtoDrop.Performance.VenueId;
                        selectedPerformanceVM.Performance.Id = performanceVMtoDrop.Performance.Id;
                        selectedPerformanceVM.PerformanceArtistVM = performanceVMtoDrop.PerformanceArtistVM;
                        selectedPerformanceVM.PerformanceRowVM = performanceVMtoDrop.PerformanceRowVM;

                        performanceVMtoDrop.Performance.ArtistId = helperPerformance.Performance.ArtistId;
                        performanceVMtoDrop.Performance.VenueId = helperPerformance.Performance.VenueId;
                        performanceVMtoDrop.Performance.Id = helperPerformance.Performance.Id;
                        performanceVMtoDrop.PerformanceArtistVM = helperPerformance.PerformanceArtistVM;
                        performanceVMtoDrop.PerformanceRowVM = helperPerformance.PerformanceRowVM;

/*
                        /*
                        switch (selectedPerformanceVM.Performance.DateTime.Hour)
                        {
                            case 14:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[0] = performanceVMtoDrop;
                                break;
                            case 15:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[1] = performanceVMtoDrop;
                                break;
                            case 16:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[2] = performanceVMtoDrop;
                                break;
                            case 17:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[3] = performanceVMtoDrop;
                                break;
                            case 18:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[4] = performanceVMtoDrop;
                                break;
                            case 19:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[5] = performanceVMtoDrop;
                                break;
                            case 20:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[6] = performanceVMtoDrop;
                                break;
                            case 21:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[7] = performanceVMtoDrop;
                                break;
                            case 22:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[8] = performanceVMtoDrop;
                                break;
                            default:
                                break;
                        }

                        switch (performanceVMtoDrop.Performance.DateTime.Hour)
                        {
                            case 14:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[0] = 
                                    new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 14, 0, 0, 0), 0, 0),
                                                                      performanceVMtoDrop.PerformanceRowVM,
                                                                      server);
                                break;
                            case 15:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[1] =
                                    new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 15, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 16:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[2] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 16, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 17:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[3] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 17, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 18:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[4] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 18, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 19:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[5] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 19, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 20:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[6] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 20, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 21:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[7] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 21, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 22:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[8] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 22, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            default:
                                break;
                        }*/
                    }

                }

                Cursor = Cursors.Arrow;

                isDragging = false;
            }
        }

        private void RemovePerformance(object sender, RoutedEventArgs e)
        {
            //AreaVM areaVM = ((FrameworkElement)sender).DataContext as AreaVM;

            //try
            //{
            //    bool success = server.DeleteArea(areaVM.Area);

            //    if (success)
            //    {
            //        areaVM.VenueCollectionVM.Areas.Remove(areaVM);
            //        AreaVM currentArea = areaVM.VenueCollectionVM.Areas[0];
            //        areaVM.VenueCollectionVM.CurrentArea = currentArea;
            //        dgAreas.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            //        dgAreas.ScrollIntoView(currentArea);
            //    }
            //}
            //catch (Exception exc)
            //{
            //    // Inform User
            //}
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential =
                new NetworkCredential("ufo.swk@gmx.at", "?ufoswk1");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("ufo.swk@gmx.at");

            smtpClient.Host = "mail.gmx.net";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            System.Net.Mail.Attachment attachment;
            CreatePDF();
            attachment = new System.Net.Mail.Attachment("../../bin/Debug/programm.pdf");
            message.Attachments.Add(attachment);

            message.From = fromAddress;
            message.Subject = "Programm was changed";
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;

            message.Body = CreateMessageBody();
            message.To.Add("t.roither@gmail.com");

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("E-mail was sent!", "Success");
            }
            catch (Exception ex)
            {
                //Error, could not send the message
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private string CreateMessageBody()
        {
            string bodyStr = "<h1>Your appointments:</h1>";
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

            foreach(Venue venue in server.FindVenuesByAreaId(areaId))
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

                foreach(Performance performance in server.FindPerformancesByDateAndVenue(date, venue))
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
