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
    public class VenueVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Venue venue;
        private Area area;

        private VenueCollectionVM venueCollectionVM;

        public VenueVM(Venue venue, Area area, VenueCollectionVM venueCollectionVM, IUFOServer server)
        {
            this.venue = venue;
            this.area = area;
            this.venueCollectionVM = venueCollectionVM;
            this.server = server;
        }


        public int Id
        {
            get { return venue.Id; }
            set
            {
                if (venue.Id != value)
                {
                    venue.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public Area Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Area)));
                }
            }
        }

        public string Name
        {
            get { return venue.Name; }
            set
            {
                if (venue.Name != value)
                {
                    venue.Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string ShortName
        {
            get { return venue.ShortName; }
            set
            {
                if (venue.ShortName != value)
                {
                    venue.ShortName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShortName)));
                }
            }
        }

        public decimal GeoLocationLat
        {
            get { return venue.GeoLocationLat; }
            set
            {
                if (venue.GeoLocationLat != value)
                {
                    venue.GeoLocationLat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GeoLocationLat)));
                }
            }
        }

        public decimal GeoLocationLon
        {
            get { return venue.GeoLocationLon; }
            set
            {
                if (venue.GeoLocationLon != value)
                {
                    venue.GeoLocationLon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GeoLocationLon)));
                }
            }
        }

        public Venue Venue
        {
            get { return venue; }
        }

        public VenueCollectionVM VenueCollectionVM
        {
            get { return venueCollectionVM; }
        }
    }
}
