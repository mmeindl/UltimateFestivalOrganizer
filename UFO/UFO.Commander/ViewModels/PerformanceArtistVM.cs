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
    public class PerformanceArtistVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Artist artist;
        private Country country;
        private Category category;

        private PerformanceVM performanceVM;

        public PerformanceArtistVM(Artist artist, Category category, Country country, PerformanceVM performanceVM, IUFOServer server)
        {
            this.artist = artist;
            this.category = category;
            this.country = country;
            this.performanceVM = performanceVM;
            this.server = server;
        }

        public string Country
        {
            get { return country.Name; }
        }

        public string Category
        {
            get { return category.Name; }
        }

        public string Name
        {
            get { return artist.Name; }
        }

        public string Color
        {
            get { return category.Color; }
        }

        public PerformanceVM PerformanceVM
        {
            get { return performanceVM; }
        }
    }
}
