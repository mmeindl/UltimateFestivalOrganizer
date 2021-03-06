﻿using System;
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
    public class ArtistVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Artist artist;
        private ArtistCollectionVM artistCollection;
        private Country country;
        private Category category;
        private ArtistPictureVM profilePicture;
        private ArtistVideoVM promoVideo;

        public ObservableCollection<ArtistPictureVM> Pictures { get; private set; }
        public ObservableCollection<ArtistVideoVM> Videos { get; private set; }

        public ArtistVM(Artist artist, Category category, Country country, ArtistCollectionVM artistCollection, IUFOServer server)
        {
            this.artist = artist;
            this.category = category;
            this.country = country;
            this.artistCollection = artistCollection;
            this.server = server;
            this.profilePicture = null;
            this.promoVideo = null;
            this.Pictures = new ObservableCollection<ArtistPictureVM>();
            this.Videos = new ObservableCollection<ArtistVideoVM>();
        }


        public int Id
        {
            get { return artist.Id; }
            set
            {
                if (artist.Id != value)
                {
                    artist.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public Country Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    artist.CountryId = country.Abbreviation;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Country)));
                }
            }
        }

        public Category Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    artist.CategoryId = category.Id;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
                }
            }
        }

        public string Name
        {
            get { return artist.Name; }
            set
            {
                if (artist.Name != value)
                {
                    artist.Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string Email
        {
            get { return artist.Email; }
            set
            {
                if (artist.Email != value)
                {
                    artist.Email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string WebsiteURL
        {
            get { return artist.WebsiteURL; }
            set
            {
                if (artist.WebsiteURL != value)
                {
                    artist.WebsiteURL = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WebsiteURL)));
                }
            }
        }

        public ArtistPictureVM ProfilePicture
        {
            get { return profilePicture; }
            set
            {
                if (profilePicture != value)
                {
                    profilePicture = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfilePicture)));
                }
            }
        }

        public ArtistVideoVM PromoVideo
        {
            get { return promoVideo; }
            set
            {
                if (promoVideo != value)
                {
                    promoVideo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PromoVideo)));
                }
            }
        }

        public bool IsDeleted
        {
            get { return artist.IsDeleted; }
            set
            {
                if (artist.IsDeleted != value)
                {
                    artist.IsDeleted = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDeleted)));
                }
            }
        }

        public Artist Artist
        {
            get { return artist; }
        }

        public ArtistCollectionVM ArtistCollection
        {
            get { return artistCollection; }
        }

        public void LoadPictures()
        {
            Pictures.Clear();
            IEnumerable<ArtistPicture> pictures = server.FindAllPicturesByArtistId(Id);

            foreach (ArtistPicture picture in pictures)
            {
                Pictures.Add(new ArtistPictureVM(picture, this, server));
            }
        }

        public void LoadVideos()
        {
            Videos.Clear();
            IEnumerable<ArtistVideo> videos = server.FindAllVideosByArtistId(Id);
            
            foreach (ArtistVideo video in videos)
            {
                Videos.Add(new ArtistVideoVM(video, this, server));
            }
        }
    }
}
