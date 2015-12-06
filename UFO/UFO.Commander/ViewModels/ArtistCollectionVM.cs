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
    public class ArtistCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<ArtistVM> Artists { get; private set; }

        public IEnumerable<Category> Categories { get; private set; }

        public IEnumerable<Country> Countries { get; private set; }

        private ArtistVM currentArtist;

        public ArtistCollectionVM(IUFOServer server)
        {
            this.server = server;
            Artists = new ObservableCollection<ArtistVM>();
            LoadArtists();

            if (Artists.Count > 0)
            {
                CurrentArtist = Artists[0];
            }
            else
            {
                currentArtist = null;
            }

            LoadCategories();
            LoadCountries();
        }

        public ArtistVM CurrentArtist
        {
            get { return currentArtist; }
            set
            {
                if (currentArtist != value)
                {
                    currentArtist = value;

                    if (CurrentArtist.Pictures == null)
                    {
                        currentArtist.LoadPictures();
                    }

                    if (CurrentArtist.Videos == null)
                    {
                        currentArtist.LoadVideos();
                    }

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
                Artists.Add(new ArtistVM(artist, category, country, server));
            }
        }

        private void LoadCategories()
        {
            Categories = server.FindAllCategories();
        }

        private void LoadCountries()
        {
            Countries = server.FindAllCountries();
        }
    }
}
