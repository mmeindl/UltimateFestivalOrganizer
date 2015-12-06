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
    public class ArtistPictureCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<ArtistPictureVM> ArtistPictures { get; private set; }

        public ArtistPictureCollectionVM(int artistId, IUFOServer server)
        {
            this.server = server;
            ArtistPictures = new ObservableCollection<ArtistPictureVM>();
            LoadArtistPictures(artistId);
        }

        private void LoadArtistPictures(int artistId)
        {
            ArtistPictures.Clear();
            IEnumerable<ArtistPicture> artistPictures = server.FindAllPicturesByArtistId(artistId);

            foreach (ArtistPicture artistPicture in artistPictures)
            {
                ArtistPictures.Add(new ArtistPictureVM(artistPicture, server));
            }
        }

    }
}
