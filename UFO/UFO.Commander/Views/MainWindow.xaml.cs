using System;
using System.Collections.Generic;
using System.Configuration;
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
using UFO.Commander.Views.Controls;
using UFO.Server;

namespace UFO.Commander.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUFOServer server;

        public MainWindow()
        {
            InitializeComponent();

            BLType type = (BLType)Enum.Parse(typeof(BLType), ConfigurationManager.AppSettings["BLType"]);

            server = UFOServerFactory.GetUFOServer(type);

            Performances.DataContext = new PerformanceCollectionVM(server);

        }

        // refresh context on tab switch
        private void performances_Clicked(object sender, MouseButtonEventArgs e)
        {
            Performances.DataContext = new PerformanceCollectionVM(server);
        }

        private void artists_Clicked(object sender, MouseButtonEventArgs e)
        {
            Artists.DataContext = new ArtistCollectionVM(server);
        }

        private void venues_Clicked(object sender, MouseButtonEventArgs e)
        {
            Venues.DataContext = new VenueCollectionVM(server);
        }
    }
}
