using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;

namespace UFO.Commander.Views
{
    /// <summary>
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        public LocationWindow()
        {
            InitializeComponent();
            myMap.Focus();
            myMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewMap_ViewChangeOnFrame);
        }

        private void viewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            Map map = sender as Map;
            if (map != null)
            {
                Location mapCenter = map.Center;

                txtLatitude.Text = string.Format(CultureInfo.InvariantCulture,
                  "{0:F5}", mapCenter.Latitude);
                txtLongitude.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);
            }
        }

        private void PutLocation(object sender, RoutedEventArgs e)
        {
            decimal longitude = Convert.ToDecimal(txtLongitude.Text);
            decimal latitude = Convert.ToDecimal(txtLatitude.Text);
        }
    }
}
