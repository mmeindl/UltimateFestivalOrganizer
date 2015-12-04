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

        private ArtistVM currentArtist;

        public ArtistCollectionVM(IUFOServer server)
        {
            this.server = server;
            Artists = new ObservableCollection<ArtistVM>();
            currentArtist = null;
            LoadArtists();
        }

        public ArtistVM CurrentArtist
        {
            get { return currentArtist; }
            set
            {
                if (currentArtist != value)
                {
                    currentArtist = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(CurrentArtist)));
                }
            }
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
