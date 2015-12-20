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
    public class AreaVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Area area;

        private VenueCollectionVM venueCollectionVM;

        public AreaVM(Area area, VenueCollectionVM venueCollectionVM, IUFOServer server)
        {
            this.area = area;
            this.venueCollectionVM = venueCollectionVM;
            this.server = server;
        }


        public int Id
        {
            get { return area.Id; }
            set
            {
                if (area.Id != value)
                {
                    area.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public string Name
        {
            get { return area.Name; }
            set
            {
                if (area.Name != value)
                {
                    area.Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public Area Area
        {
            get { return area; }
        }

        public VenueCollectionVM VenueCollectionVM
        {
            get { return venueCollectionVM; }
        }
    }
}
