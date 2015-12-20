using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using UFO.Domain;
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

                if (GetPerformanceFromCell(cell) && selectedPerformanceVM.Performance.Id == 0 && performanceVMtoDrop != null)
                {
                    Performance p = performanceVMtoDrop.Performance;
                        p.DateTime =
                        performanceVMtoDrop.Performance.DateTime.Date +
                        new TimeSpan(selectedPerformanceVM.Performance.DateTime.Hour, 0, 0);

                    p.VenueId = selectedPerformanceVM.Performance.VenueId;

                    bool success = false;

                    try
                    {
                        success = server.UpdatePerformance(p);
                    }
                    catch (Exception exc)
                    {
                        // inform user?
                    }

                    if (success)
                    {
                        /*
                        DateTime curDate = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.CurrentDate;
                        Area curArea = performanceVMtoDrop.PerformanceRowVM.PerformanceCollectionVM.CurrentArea;

                        this.DataContext = new PerformanceCollectionVM(
                            UFOServerFactory.GetUFOServer());
*/
                        /*
                        PerformanceVM helperPerformance = selectedPerformanceVM;
                        
                        selectedPerformanceVM.Performance.ArtistId = performanceVMtoDrop.Performance.ArtistId;
                        selectedPerformanceVM.Performance.VenueId = performanceVMtoDrop.Performance.VenueId;
                        selectedPerformanceVM.Performance.Id = performanceVMtoDrop.Performance.Id;
                        selectedPerformanceVM.PerformanceArtistVM = performanceVMtoDrop.PerformanceArtistVM;
                        selectedPerformanceVM.PerformanceRowVM = performanceVMtoDrop.PerformanceRowVM;

                        performanceVMtoDrop.Performance.ArtistId = helperPerformance.Performance.ArtistId;
                        performanceVMtoDrop.Performance.VenueId = helperPerformance.Performance.VenueId;
                        performanceVMtoDrop.Performance.Id = helperPerformance.Performance.Id;
                        performanceVMtoDrop.PerformanceArtistVM = helperPerformance.PerformanceArtistVM;
                        performanceVMtoDrop.PerformanceRowVM = helperPerformance.PerformanceRowVM;

/*
                        /*
                        switch (selectedPerformanceVM.Performance.DateTime.Hour)
                        {
                            case 14:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[0] = performanceVMtoDrop;
                                break;
                            case 15:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[1] = performanceVMtoDrop;
                                break;
                            case 16:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[2] = performanceVMtoDrop;
                                break;
                            case 17:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[3] = performanceVMtoDrop;
                                break;
                            case 18:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[4] = performanceVMtoDrop;
                                break;
                            case 19:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[5] = performanceVMtoDrop;
                                break;
                            case 20:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[6] = performanceVMtoDrop;
                                break;
                            case 21:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[7] = performanceVMtoDrop;
                                break;
                            case 22:
                                selectedPerformanceVM.PerformanceRowVM.VenuePerformances[8] = performanceVMtoDrop;
                                break;
                            default:
                                break;
                        }

                        switch (performanceVMtoDrop.Performance.DateTime.Hour)
                        {
                            case 14:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[0] = 
                                    new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 14, 0, 0, 0), 0, 0),
                                                                      performanceVMtoDrop.PerformanceRowVM,
                                                                      server);
                                break;
                            case 15:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[1] =
                                    new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 15, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 16:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[2] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 16, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 17:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[3] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 17, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 18:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[4] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 18, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 19:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[5] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 19, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 20:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[6] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 20, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 21:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[7] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 21, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            case 22:
                                performanceVMtoDrop.PerformanceRowVM.VenuePerformances[8] = new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 22, 0, 0, 0), 0, 0), performanceVMtoDrop.PerformanceRowVM, server);
                                break;
                            default:
                                break;
                        }*/
                    }

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
