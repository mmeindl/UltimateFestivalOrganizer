using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.ViewModels
{
    public class VenueCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<VenueVM> Venues { get; private set; }
        public ObservableCollection<AreaVM> Areas { get; private set; }

        private VenueVM currentVenue;
        private AreaVM currentArea;

        public VenueCollectionVM(IUFOServer server)
        {
            this.server = server;
            Venues = new ObservableCollection<VenueVM>();
            Areas = new ObservableCollection<AreaVM>();
            LoadAreas();
        }

        public VenueVM CurrentVenue
        {
            get { return currentVenue; }
            set
            {
                if (currentVenue != value)
                {
                    currentVenue = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentVenue)));
                }
            }
        }

        public AreaVM CurrentArea
        {
            get { return currentArea; }
            set
            {
                if (currentArea != value)
                {
                    currentArea = value;

                    if (currentArea != null)
                    {
                        LoadVenues();
                    }

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArea)));
                }
            }
        }

        private async void LoadAreas()
        {
            Areas.Clear();
            IEnumerable<Area> areas = server.FindAllAreas();

            IEnumerator<Area> enumerator = areas.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Areas.Add(new AreaVM(enumerator.Current, this, server));
            }

            if (Areas.Count > 0)
            {
                CurrentArea = Areas[0];
            }
            else
            {
                currentArea = null;
            }
        }

        private async void LoadVenues()
        {
            Venues.Clear();
            IEnumerable<Venue> venues = server.FindVenuesByAreaId(currentArea.Id);

            IEnumerator<Venue> enumerator = venues.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Venues.Add(new VenueVM(enumerator.Current, currentArea.Area, this, server));
            }

            if (Venues.Count > 0)
            {
                CurrentVenue = Venues[0];
            }
            else
            {
                currentVenue = null;
            }
        }
    }
}
