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
    class ArtistCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<ArtistVM> Artists { get; private set; }

        public ArtistCollectionVM(IUFOServer server)
        {
            this.server = server;
            Artists = new ObservableCollection<ArtistVM>();
            LoadArtists();
        }

        private void LoadArtists()
        {
            Artists.Clear();
            IEnumerable<Artist> artists = server.FindAllArtists();

            foreach (Artist artist in artists)
            {
                Category category = server.FindCategoryById(artist.CategoryId);
                Country country = server.FindCountryByAbbreviation(artist.CountryId);
                Artists.Add(new ArtistVM(artist, category.Name, country.Name, server));
            }
        }
    }
}
