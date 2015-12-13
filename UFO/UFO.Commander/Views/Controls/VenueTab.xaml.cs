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
                    server.DeleteArea(areaVM.Area);
                }
            }
            catch (Exception exc)
            {
                // Inform User
            }
        }
    }
}
