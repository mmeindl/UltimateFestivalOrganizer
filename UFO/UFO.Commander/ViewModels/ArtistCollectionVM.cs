using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

            LoadCategories();
            LoadCountries();
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

                    if (currentArtist != null)
                    {
                        if (CurrentArtist.Pictures.Count == 0)
                        {
                            currentArtist.LoadPictures();
                        }

                        CurrentArtist.ProfilePicture = currentArtist.Pictures.FirstOrDefault(p => p.IsProfilePicture == true);

                        if (CurrentArtist.Videos.Count == 0)
                        {
                            currentArtist.LoadVideos();
                        }

                        CurrentArtist.PromoVideo = currentArtist.Videos.FirstOrDefault(v => v.IsPromoVideo == true);
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArtist)));
                }
            }
        }

        private async void LoadArtists()
        {
            Artists.Clear();

            IEnumerable<Artist> artists = server.FindAllArtists();

            IEnumerator<Artist> enumerator = artists.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Category category = Categories.FirstOrDefault(c => c.Id == enumerator.Current.CategoryId);
                Country country = Countries.FirstOrDefault(c => c.Abbreviation == enumerator.Current.CountryId);
                Artists.Add(new ArtistVM(enumerator.Current, category, country, this, server));
            }

            if (Artists.Count > 0)
            {
                CurrentArtist = Artists[0];
            }
            else
            {
                currentArtist = null;
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
