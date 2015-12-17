using Swk5.MediaAnnotator.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.ViewModels
{
    public class PerformanceRowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Venue venue;

        public ObservableCollection<PerformanceVM> VenuePerformances;

        private PerformanceCollectionVM performanceCollectionVM;

        public PerformanceRowVM(IEnumerable<Performance> performances, Venue venue, PerformanceCollectionVM performanceCollectionVM, IUFOServer server)
        {
            this.venue = venue;
            this.performanceCollectionVM = performanceCollectionVM;
            this.VenuePerformances = new ObservableCollection<PerformanceVM>();
            this.server = server;

            //LoadPerformances(performances);
        }

        public Venue Venue
        {
            get { return venue; }
            set
            {
                if (venue != value)
                {
                    venue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Venue)));
                }
            }
        }

        public PerformanceCollectionVM PerformanceCollectionVM
        {
            get { return performanceCollectionVM; }
        }

        private async void LoadPerformances(IEnumerable<Performance> performances)
        {
            
            VenuePerformances.Clear();
            for (int i = 0; i < 9; ++i)
            {
                VenuePerformances.Add(new PerformanceVM(null, performanceCollectionVM, server));
            }

            IEnumerator<Performance> enumerator = performances.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                switch (enumerator.Current.DateTime.Hour)
                {
                    case 14:
                        VenuePerformances[0].Performance = enumerator.Current;
                        break;
                    case 15:
                        VenuePerformances[1].Performance = enumerator.Current;
                        break;
                    case 16:
                        VenuePerformances[2].Performance = enumerator.Current;
                        break;
                    case 17:
                        VenuePerformances[3].Performance = enumerator.Current;
                        break;
                    case 18:
                        VenuePerformances[4].Performance = enumerator.Current;
                        break;
                    case 19:
                        VenuePerformances[5].Performance = enumerator.Current;
                        break;
                    case 20:
                        VenuePerformances[6].Performance = enumerator.Current;
                        break;
                    case 21:
                        VenuePerformances[7].Performance = enumerator.Current;
                        break;
                    case 22:
                        VenuePerformances[8].Performance = enumerator.Current;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
