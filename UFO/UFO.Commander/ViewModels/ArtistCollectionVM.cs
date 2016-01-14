﻿using System;
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
                        if (CurrentArtist.ProfilePicture == null)
                        {
                            ArtistPicture pic = server.FindProfilePictureByArtistId(CurrentArtist.Id);

                            if (pic != null)
                                CurrentArtist.ProfilePicture = new ArtistPictureVM(pic, CurrentArtist, server);
                        }

                        if (CurrentArtist.PromoVideo == null)
                        {
                            ArtistVideo vid = server.FindPromoVideoByArtistId(CurrentArtist.Id);

                            if (vid != null)
                                CurrentArtist.PromoVideo = new ArtistVideoVM(vid, CurrentArtist, server);
                        }

                        if (CurrentArtist.Pictures.Count == 0)
                        {
                            currentArtist.LoadPictures();
                        }

                        if (CurrentArtist.Videos.Count == 0)
                        {
                            currentArtist.LoadVideos();
                        }
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
                Category category = Categories.FirstOrDefault(c => c.Id == enumerator.Current.CategoryId); //server.FindCategoryById(enumerator.Current.CategoryId);
                Country country = Countries.FirstOrDefault(c => c.Abbreviation == enumerator.Current.CountryId);  //server.FindCountryByAbbreviation(enumerator.Current.CountryId);
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
