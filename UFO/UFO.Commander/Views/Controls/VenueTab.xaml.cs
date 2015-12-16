using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaktionslogik für ArtistTab.xaml
    /// </summary>
    public partial class VenueTab : UserControl
    {
        IUFOServer server;

        public VenueTab()
        {
            server = UFOServerFactory.GetUFOServer();

            InitializeComponent();
            DataContext = new VenueCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

        private void AddArea(object sender, RoutedEventArgs e)
        {
            VenueCollectionVM venueCollectionVM = ((FrameworkElement)sender).DataContext as VenueCollectionVM;

            string name = txtNameNew.Text;

            if (name != null)
            {
                Area area = new Area(name);
                bool success = server.InsertArea(area);
                if (success)
                {
                    area = server.FindAreaByName(name);

                    AreaVM areaVM = new AreaVM(area, venueCollectionVM, server);
                    venueCollectionVM.Areas.Add(areaVM);

                    venueCollectionVM.CurrentArea = areaVM;
                    dgAreas.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                    dgAreas.ScrollIntoView(areaVM);
                }
            }
        }

        private void RemoveArea(object sender, RoutedEventArgs e)
        {
            AreaVM areaVM = ((FrameworkElement)sender).DataContext as AreaVM;

            try
            {
                bool success = server.DeleteArea(areaVM.Area);

                if (success)
                {
                    areaVM.VenueCollectionVM.Areas.Remove(areaVM);
                    AreaVM currentArea = areaVM.VenueCollectionVM.Areas[0];
                    areaVM.VenueCollectionVM.CurrentArea = currentArea;
                    dgAreas.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    dgAreas.ScrollIntoView(currentArea);
                }
            }
            catch (Exception exc)
            {
                // Inform User
            }
        }

        private void AddVenue(object sender, RoutedEventArgs e)
        {
            VenueCollectionVM venueCollectionVM = ((FrameworkElement)sender).DataContext as VenueCollectionVM;

            

            Venue venue = new Venue();

            bool success = false;
            try
            {
                string name = txtVenueNameNew.Text;
                string shortName = txtShortNameNew.Text;
                decimal geoLat = Decimal.Parse(txtGeoLatNew.Text);
                decimal geoLon = Decimal.Parse(txtGeoLonNew.Text);

                venue.Name = name;
                venue.ShortName = shortName;
                venue.GeoLocationLat = geoLat;
                venue.GeoLocationLon = geoLon;

                success = server.InsertVenue(venue);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }

            if (success)
            {
                venue = server.FindVenueByName(venue.Name);
                VenueVM venueVM = new VenueVM(venue, venueCollectionVM.CurrentArea.Area, venueCollectionVM, server);
                venueCollectionVM.Venues.Add(venueVM);
                venueCollectionVM.CurrentVenue = venueVM;
                dgVenues.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                dgVenues.ScrollIntoView(venueVM);
            }
        }


        private void UpdateVenue(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveVenue(object sender, RoutedEventArgs e)
        {
            VenueVM venueVM = ((FrameworkElement)sender).DataContext as VenueVM;

            try
            {
                //bool success = server.DeleteVenue(venueVM.Venue);

                //if (success)
                //{
                    //areaVM.VenueCollectionVM.Areas.Remove(areaVM);
                    //AreaVM currentArea = areaVM.VenueCollectionVM.Areas[0];
                    //areaVM.VenueCollectionVM.CurrentArea = currentArea;
                    //dgAreas.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    //dgAreas.ScrollIntoView(currentArea);
                    //server.DeleteArea(areaVM.Area);
                //}
            }
            catch (Exception exc)
            {
                // Inform User
            }
        }

        private void ShowMap(object sender, RoutedEventArgs e)
        {
            Object dc = this.DataContext;
            LocationWindow locationWindow = new LocationWindow();
            locationWindow.DataContext = dc;
            locationWindow.Show();
        }

        private void SetFocus(object sender, RoutedEventArgs e)
        {
            //((DataGrid)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            //((DataGrid)sender).ScrollIntoView()
        }
    }
}
