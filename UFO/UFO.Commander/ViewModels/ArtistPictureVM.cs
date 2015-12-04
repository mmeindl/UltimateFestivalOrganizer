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
    public class ArtistPictureVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private ArtistPicture artistPicture;

        public ArtistPictureVM(ArtistPicture artistPicture, IUFOServer server)
        {
            this.artistPicture = artistPicture;
            this.server = server;
        }

        public int Id
        {
            get { return artistPicture.Id; }
            set
            {
                if (artistPicture.Id != value)
                {
                    artistPicture.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public int ArtistId
        {
            get { return artistPicture.ArtistId; }
            set
            {
                if (artistPicture.ArtistId != value)
                {
                    artistPicture.ArtistId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtistId)));
                }
            }
        }

        public bool IsProfilePicture
        {
            get { return artistPicture.IsProfilePicture; }
            set
            {
                if (artistPicture.IsProfilePicture != value)
                {
                    artistPicture.IsProfilePicture = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsProfilePicture)));
                }
            }
        }

        public string PictureURL
        {
            get { return artistPicture.PictureURL; }
        }


    }
}
