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
        }

        private void RemoveArea(object sender, RoutedEventArgs e)
        {
        }
    }
}
