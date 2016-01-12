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

            LoadPerformances(performances);
        }

        public string VenueName
        {
            get { return venue.Name; }
        }

        public string VenueShortName
        {
            get { return venue.ShortName; }
        }

        public PerformanceArtistVM PerformanceArtist14
        {
            get
            {
                return VenuePerformances[0].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[0].PerformanceArtistVM != value)
                {
                    VenuePerformances[0].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist15
        {
            get
            {
                return VenuePerformances[1].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[1].PerformanceArtistVM != value)
                {
                    VenuePerformances[1].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }
        
        public PerformanceArtistVM PerformanceArtist16
        {
            get
            {
                return VenuePerformances[2].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[2].PerformanceArtistVM != value)
                {
                    VenuePerformances[2].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist17
        {
            get
            {
                return VenuePerformances[3].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[3].PerformanceArtistVM != value)
                {
                    VenuePerformances[3].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist18
        {
            get
            {
                return VenuePerformances[4].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[4].PerformanceArtistVM != value)
                {
                    VenuePerformances[4].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist19
        {
            get
            {
                return VenuePerformances[5].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[5].PerformanceArtistVM != value)
                {
                    VenuePerformances[5].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist20
        {
            get
            {
                return VenuePerformances[6].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[6].PerformanceArtistVM != value)
                {
                    VenuePerformances[6].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist21
        {
            get
            {
                return VenuePerformances[7].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[7].PerformanceArtistVM != value)
                {
                    VenuePerformances[7].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtist22
        {
            get
            {
                return VenuePerformances[8].PerformanceArtistVM;
            }
            set
            {
                if (VenuePerformances[8].PerformanceArtistVM != value)
                {
                    VenuePerformances[8].PerformanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VenuePerformances)));
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
                VenuePerformances.Add(new PerformanceVM(new Performance(new DateTime(2000, 1, 1, 14 + i, 0, 0, 0), venue.Id, 0), this, server));
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
