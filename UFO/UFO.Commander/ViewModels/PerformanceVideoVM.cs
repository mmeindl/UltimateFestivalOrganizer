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
    public class PerformanceVideoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private PerformanceVideo performanceVideo;
        private PerformanceMediaCollectionVM performanceMediaCollectionVM;

        public PerformanceVideoVM(PerformanceVideo PerformanceVideo, PerformanceMediaCollectionVM performanceMediaCollectionVM, IUFOServer server)
        {
            this.performanceVideo = PerformanceVideo;
            this.server = server;
            this.performanceMediaCollectionVM = performanceMediaCollectionVM;
        }

        public int Id
        {
            get { return performanceVideo.Id; }
            set
            {
                if (performanceVideo.Id != value)
                {
                    performanceVideo.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public int PerformanceId
        {
            get { return performanceVideo.PerformanceId; }
            set
            {
                if (performanceVideo.PerformanceId != value)
                {
                    performanceVideo.PerformanceId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceId)));
                }
            }
        }

        public string VideoURL
        {
            get { return performanceVideo.VideoURL; }
        }

        public PerformanceMediaCollectionVM PerformanceMediaCollectionVM
        {
            get { return performanceMediaCollectionVM; }
        }

        public PerformanceVideo PerformanceVideo
        {
            get { return performanceVideo; }
        }
    }
}
