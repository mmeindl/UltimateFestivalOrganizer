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

            if (Areas.Count > 0)
            {
                CurrentArea = Areas[0];
                LoadVenues();

                if (Venues.Count > 0)
                {
                    CurrentVenue = Venues[0];
                }
                else
                {
                    currentVenue = null;
                }
            }
            else
            {
                currentArea = null;
            }
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

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArea)));
                }
            }
        }

        private void LoadAreas()
        {
            Areas.Clear();
            IEnumerable<Area> areas = server.FindAllAreas();

            foreach (Area area in areas)
            {
                Areas.Add(new AreaVM(area, server));
            }
        }

        private void LoadVenues()
        {
            Venues.Clear();
            IEnumerable<Venue> venues = server.FindVenuesByAreaId(currentArea.Id);

            foreach (Venue venue in venues)
            {
                Venues.Add(new VenueVM(venue, currentArea.Area, server));
            }
        }
    }
}
