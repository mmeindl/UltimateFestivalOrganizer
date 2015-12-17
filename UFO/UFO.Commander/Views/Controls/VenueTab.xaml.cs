using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            newMap.Focus();
            newMap.ViewChangeOnFrame += NewMap_ViewChangeOnFrame;
            //editMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewEditMap_ViewChangeOnFrame);
        }

        private void NewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            Map map = sender as Map;
            if (map != null)
            {
                Location mapCenter = map.Center;

                txtGeoLatNew.Text = string.Format(CultureInfo.InvariantCulture,
                  "{0:F5}", mapCenter.Latitude);
                txtGeoLonNew.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);
            }
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
                MessageBoxResult result = MessageBox.Show(exc.Message, "Confirmation");
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
                decimal geoLat = decimal.Parse(txtGeoLatNew.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                decimal geoLon = decimal.Parse(txtGeoLonNew.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                venue.Name = name;
                venue.ShortName = shortName;
                venue.GeoLocationLat = geoLat;
                venue.GeoLocationLon = geoLon;
                venue.AreaId = venueCollectionVM.CurrentArea.Id;

                success = server.InsertVenue(venue);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
                MessageBoxResult result = MessageBox.Show(exc.Message, "Confirmation");
            }

            if (success)
            {
                venue = server.FindVenueByName(venue.Name);
                VenueVM venueVM = new VenueVM(venue, venueCollectionVM.CurrentArea.Area, venueCollectionVM, server);
                venueCollectionVM.Venues.Add(venueVM);
                venueCollectionVM.CurrentVenue = venueVM;
                dgVenues.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                dgVenues.ScrollIntoView(venueVM);

                txtVenueNameNew.Clear();
                txtShortNameNew.Clear();
                txtGeoLatNew.Clear();
                txtGeoLonNew.Clear();
            }
        }


        private void UpdateVenue(object sender, RoutedEventArgs e)
        {
            VenueCollectionVM venueCollectionVM = ((FrameworkElement)sender).DataContext as VenueCollectionVM;
            VenueVM venueVM = venueCollectionVM.CurrentVenue;

            string oldName = venueVM.Name;
            string oldShortName = venueVM.ShortName;
            decimal oldGeoLat = venueVM.GeoLocationLat;
            decimal oldGeoLon = venueVM.GeoLocationLon;

            bool success = false;
            try
            {
                string name = txtVenueName.Text;
                string shortName = txtShortName.Text;

                decimal geoLat = decimal.Parse(txtGeoLat.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                decimal geoLon = decimal.Parse(txtGeoLon.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                if (editMapPane.Visibility == Visibility.Visible && txtGeoLonEditMap.Text != "" && txtGeoLatEditMap.Text != "")
                {
                    geoLat = decimal.Parse(txtGeoLatEditMap.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    geoLon = decimal.Parse(txtGeoLonEditMap.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                }


                venueVM.Name = name;
                venueVM.ShortName = shortName;
                venueVM.GeoLocationLat = geoLat;
                venueVM.GeoLocationLon = geoLon;

                success = server.UpdateVenue(venueVM.Venue);                
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
                MessageBoxResult result = MessageBox.Show(exc.Message, "Confirmation");
            }

            if (!success)
            {
                venueVM.Name = oldName;
                venueVM.ShortName = oldShortName;
                venueVM.GeoLocationLat = oldGeoLat;
                venueVM.GeoLocationLon = oldGeoLon;
            } else
            {
                editMapPane.Visibility = Visibility.Collapsed;
                editLocationGrid.Visibility = Visibility.Visible;
            }
        }

        private void RemoveVenue(object sender, RoutedEventArgs e)
        {
            VenueVM venueVM = ((FrameworkElement)sender).DataContext as VenueVM;

            try
            {
                bool success = server.DeleteVenue(venueVM.Venue);

                if (success)
                {
                    venueVM.VenueCollectionVM.Venues.Remove(venueVM);
                    VenueVM currentVenue = venueVM.VenueCollectionVM.Venues[0];
                    venueVM.VenueCollectionVM.CurrentVenue = currentVenue;
                    dgAreas.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    dgAreas.ScrollIntoView(currentVenue);
                    server.DeleteVenue(venueVM.Venue);
                }
            }
            catch (Exception exc)
            {
                // Inform User
                MessageBoxResult result = MessageBox.Show(exc.Message, "Confirmation");
            }
        }

        private void ShowEditMap(object sender, RoutedEventArgs e)
        {
            try
            {            
                editMapPane.Visibility = Visibility.Visible;
                editLocationGrid.Visibility = Visibility.Collapsed;
                Location center = new Location();
                string lat = txtGeoLat.Text.Substring(0, 7).Replace('.', ',');
                string lon = txtGeoLon.Text.Substring(0, 7).Replace('.', ',');
                center.Latitude = Convert.ToDouble(lat);
                center.Longitude = Convert.ToDouble(lon);

                editMap.Center = center;
                editMap.Focus();
                editMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewEditMap_ViewChangeOnFrame);
            }
            catch (Exception exc)
            {
                // Inform User
                MessageBoxResult result = MessageBox.Show(exc.Message, "Confirmation");
            }

        }

        private void viewEditMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            Map map = sender as Map;
            if (map != null)
            {
                Location mapCenter = map.Center;

                txtGeoLatEditMap.Text = string.Format(CultureInfo.InvariantCulture,
                  "{0:F5}", mapCenter.Latitude);
                txtGeoLonEditMap.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);
            }
        }

        private void txtVenueName_TextChanged(object sender, TextChangedEventArgs e)
        {
            editMapPane.Visibility = Visibility.Collapsed;
            editLocationGrid.Visibility = Visibility.Visible;

        }


    }
}
