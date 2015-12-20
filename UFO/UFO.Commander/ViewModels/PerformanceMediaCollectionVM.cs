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
    public class PerformanceMediaCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<PerformancePictureVM> Pictures { get; private set; }
        public ObservableCollection<PerformanceVideoVM> Videos { get; private set; }

        private Artist artist;
        private Venue venue;
        private Performance performance;

        public PerformanceMediaCollectionVM(Performance performance, IUFOServer server)
        {
            this.performance = performance;
            this.server = server;

            this.Pictures = new ObservableCollection<PerformancePictureVM>();
            this.Videos = new ObservableCollection<PerformanceVideoVM>();

            artist = server.FindArtistById(performance.ArtistId);
            venue = server.FindVenueById(performance.VenueId);

            LoadPictures();
            LoadVideos();
        }

        public string PerformanceTitle
        {
            get { return "Artist: " + artist.Name + ", Venue: " + venue.Name; }
        }

        public Artist Artist
        {
            get { return artist; }
        }

        public Venue Venue
        {
            get { return venue; }
        }

        public Performance Performance
        {
            get { return performance; }
        }

        public async void LoadPictures()
        {
            Pictures.Clear();
            IEnumerable<PerformancePicture> pictures = server.FindAllPicturesByPerformanceId(performance.Id);

            IEnumerator<PerformancePicture> enumerator = pictures.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Pictures.Add(new PerformancePictureVM(enumerator.Current, this, server));
            }
        }

        public async void LoadVideos()
        {
            Videos.Clear();
            IEnumerable<PerformanceVideo> videos = server.FindAllVideosByPerformanceId(performance.Id);

            IEnumerator<PerformanceVideo> enumerator = videos.GetEnumerator();
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Videos.Add(new PerformanceVideoVM(enumerator.Current, this, server));
            }
        }
    }
}
