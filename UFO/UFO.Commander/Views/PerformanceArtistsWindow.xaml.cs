using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace UFO.Commander.Views
{
    /// <summary>
    /// Interaction logic for PerformanceArtistsWindow.xaml
    /// </summary>
    public partial class PerformanceArtistsWindow : Window
    {
        private Artist selectedArtist;
        private IUFOServer server;

        public PerformanceArtistsWindow()
        {
            InitializeComponent();

            BLType type = (BLType)Enum.Parse(typeof(BLType), ConfigurationManager.AppSettings["BLType"]);

            server = UFOServerFactory.GetUFOServer(type);

            DataContext = new ArtistCollectionVM(server);
        }

        private void SelectArtist(object sender, RoutedEventArgs e)
        {
            selectedArtist = ((ArtistVM)dgArtists.SelectedItem).Artist;
            this.Close();
        }

        public Artist Artist
        {
            get { return selectedArtist; }
        }
    }
}
