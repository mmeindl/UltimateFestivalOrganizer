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
using UFO.Commander.Views.Controls;
using UFO.Server;

namespace UFO.Commander.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Performances.DataContext = new PerformanceCollectionVM(
                UFOServerFactory.GetUFOServer());

        }

        private void performances_Clicked(object sender, MouseButtonEventArgs e)
        {
            Performances.DataContext = new PerformanceCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

        private void artists_Clicked(object sender, MouseButtonEventArgs e)
        {
            Artists.DataContext = new ArtistCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

        private void venues_Clicked(object sender, MouseButtonEventArgs e)
        {
            Venues.DataContext = new VenueCollectionVM(
                UFOServerFactory.GetUFOServer());
        }
    }
}
