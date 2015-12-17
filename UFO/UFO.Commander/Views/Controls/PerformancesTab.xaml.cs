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

namespace UFO.Commander.Views.Controls
{
    /// <summary>
    /// Interaktionslogik für PerformancesTab.xaml
    /// </summary>
    public partial class PerformancesTab : UserControl
    {
        IUFOServer server;

        public PerformancesTab()
        {
            server = UFOServerFactory.GetUFOServer();

            InitializeComponent();
            /*DataContext = new PerformanceCollectionVM(
                server);*/
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
    }
}
