using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private IUFOServer server;
        private PerformanceVM selectedPerformanceVM;

        private bool isDragging;

        public PerformancesTab()
        {
            server = UFOServerFactory.GetUFOServer();

            InitializeComponent();

            isDragging = false;
        }

        private void drag_performance(object sender, MouseButtonEventArgs e)
        {
            PerformanceCollectionVM performanceCollectionVM = ((FrameworkElement)sender).DataContext as PerformanceCollectionVM;

            DataGridCell cell = GetCell((DependencyObject)e.OriginalSource);

            GetPerformanceFromCell(cell);

            Cursor = Cursors.Cross;

            isDragging = true;
        }

        private void drop_performance(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                PerformanceVM performanceVMtoDrop = selectedPerformanceVM;

                DataGridCell cell = GetCell((DependencyObject)e.OriginalSource);

                if (GetPerformanceFromCell(cell) && selectedPerformanceVM.Performance == null)
                {
                    PerformanceVM helperVM = selectedPerformanceVM;
                    selectedPerformanceVM = performanceVMtoDrop;
                    performanceVMtoDrop = helperVM;
                }

                Cursor = Cursors.Arrow;

                isDragging = false;
            }
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





        // Helpers

        private DataGridCell GetCell(DependencyObject dep)
        {
            while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            return dep as DataGridCell;
        }

        private DataGridRow GetRow(DependencyObject cell)
        {
            while ((cell != null) && !(cell is DataGridRow))
            {
                cell = VisualTreeHelper.GetParent(cell);
            }

            return cell as DataGridRow;
        }

        private int GetRowIndex(DataGridRow row)
        {
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);

            return index;
        }

        private bool GetPerformanceFromCell(DataGridCell cell)
        {
            const int STARTCOLIDX = 2;
            
            if (cell == null)
            {
                return false;
            }

            int colIndex = cell.Column.DisplayIndex;
            DataGridRow row = GetRow(cell);

            if (colIndex >= STARTCOLIDX)
            {
                PerformanceRowVM performanceRowVM = (PerformanceRowVM)row.Item;
                selectedPerformanceVM = performanceRowVM.VenuePerformances.ElementAt(colIndex - STARTCOLIDX);
                return true;
            }

            selectedPerformanceVM = null;
            return false;
        }
    }
}
