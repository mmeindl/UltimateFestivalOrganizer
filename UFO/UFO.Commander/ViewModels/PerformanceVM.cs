using Swk5.MediaAnnotator.ViewModels;
using System;
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
    public class PerformanceVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Performance performance;
        private Artist artist;

        private PerformanceCollectionVM performanceCollectionVM;

        public PerformanceVM(Performance performance, PerformanceCollectionVM performanceCollectionVM, IUFOServer server)
        {
            this.performance = performance;
            this.performanceCollectionVM = performanceCollectionVM;
            this.artist = server.FindArtistById(performance.ArtistId);
            this.server = server;
        }


        public int Id
        {
            get { return performance.Id; }
            set
            {
                if (performance.Id != value)
                {
                    performance.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public Artist Artist
        {
            get { return artist; }
            set
            {
                if (artist != value)
                {
                    artist = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist)));
                }
            }
        }

        public Performance Performance
        {
            get { return performance; }
        }

        public PerformanceCollectionVM PerformanceCollectionVM
        {
            get { return performanceCollectionVM; }
        }
    }
}
