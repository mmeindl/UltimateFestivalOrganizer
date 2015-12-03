using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.ViewModels
{
    public class ArtistVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Artist artist;
        private string country;
        private string category;

        public ArtistVM(Artist artist, string category, string country, IUFOServer server)
        {
            this.artist = artist;
            this.category = category;
            this.country = country;
            this.server = server;
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

        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(country)));
                }
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(category)));
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

    }
}
