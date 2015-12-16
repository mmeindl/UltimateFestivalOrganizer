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
            
        }

        private void performances_Clicked(object sender, MouseButtonEventArgs e)
        {
            DataContext = new PerformanceCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

        private void artists_Clicked(object sender, MouseButtonEventArgs e)
        {
            DataContext = new ArtistCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

        private void venues_Clicked(object sender, MouseButtonEventArgs e)
        {
            DataContext = new VenueCollectionVM(
                UFOServerFactory.GetUFOServer());
        }

    }
}
