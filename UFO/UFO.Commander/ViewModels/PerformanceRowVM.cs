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

        public Artist PerformanceArtist14
        {
            get
            {
                return VenuePerformances[0].Artist;
            }
            set
            {
                if (VenuePerformances[0].Artist != value)
                {
                    VenuePerformances[0].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist14)));
                }
            }
        }

        public Artist PerformanceArtist15
        {
            get
            {
                return VenuePerformances[1].Artist;
            }
            set
            {
                if (VenuePerformances[1].Artist != value)
                {
                    VenuePerformances[1].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist15)));
                }
            }
        }
        
        public Artist PerformanceArtist16
        {
            get
            {
                return VenuePerformances[2].Artist;
            }
            set
            {
                if (VenuePerformances[2].Artist != value)
                {
                    VenuePerformances[2].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist16)));
                }
            }
        }

        public Artist PerformanceArtist17
        {
            get
            {
                return VenuePerformances[3].Artist;
            }
            set
            {
                if (VenuePerformances[3].Artist != value)
                {
                    VenuePerformances[3].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist17)));
                }
            }
        }

        public Artist PerformanceArtist18
        {
            get
            {
                return VenuePerformances[4].Artist;
            }
            set
            {
                if (VenuePerformances[4].Artist != value)
                {
                    VenuePerformances[4].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist18)));
                }
            }
        }

        public Artist PerformanceArtist19
        {
            get
            {
                return VenuePerformances[5].Artist;
            }
            set
            {
                if (VenuePerformances[5].Artist != value)
                {
                    VenuePerformances[5].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist19)));
                }
            }
        }

        public Artist PerformanceArtist20
        {
            get
            {
                return VenuePerformances[6].Artist;
            }
            set
            {
                if (VenuePerformances[6].Artist != value)
                {
                    VenuePerformances[6].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist20)));
                }
            }
        }

        public Artist PerformanceArtist21
        {
            get
            {
                return VenuePerformances[7].Artist;
            }
            set
            {
                if (VenuePerformances[7].Artist != value)
                {
                    VenuePerformances[7].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist21)));
                }
            }
        }

        public Artist PerformanceArtist22
        {
            get
            {
                return VenuePerformances[8].Artist;
            }
            set
            {
                if (VenuePerformances[8].Artist != value)
                {
                    VenuePerformances[8].Artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtist22)));
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
