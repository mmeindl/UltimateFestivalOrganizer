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
    public class ArtistVideoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private ArtistVideo artistVideo;
        private ArtistVM artist;

        public ArtistVideoVM(ArtistVideo artistVideo, ArtistVM artist, IUFOServer server)
        {
            this.artistVideo = artistVideo;
            this.server = server;
            this.artist = artist;
        }

        public int Id
        {
            get { return artistVideo.Id; }
            set
            {
                if (artistVideo.Id != value)
                {
                    artistVideo.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public int ArtistId
        {
            get { return artistVideo.ArtistId; }
            set
            {
                if (artistVideo.ArtistId != value)
                {
                    artistVideo.ArtistId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArtistId)));
                }
            }
        }

        public bool IsProfileVideo
        {
            get { return artistVideo.IsPromoVideo; }
            set
            {
                if (artistVideo.IsPromoVideo != value)
                {
                    artistVideo.IsPromoVideo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsProfileVideo)));
                }
            }
        }

        public string VideoURL
        {
            get { return artistVideo.VideoURL; }
        }

        public ArtistVM Artist
        {
            get { return artist; }
        }


    }
}
