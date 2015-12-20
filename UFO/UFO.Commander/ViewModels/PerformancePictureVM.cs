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
    public class PerformancePictureVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private PerformancePicture performancePicture;
        private PerformanceMediaCollectionVM performanceMediaCollectionVM;

        public PerformancePictureVM(PerformancePicture performancePicture, PerformanceMediaCollectionVM performanceMediaCollectionVM, IUFOServer server)
        {
            this.performancePicture = performancePicture;
            this.server = server;
            this.performanceMediaCollectionVM = performanceMediaCollectionVM;
        }

        public int Id
        {
            get { return performancePicture.Id; }
            set
            {
                if (performancePicture.Id != value)
                {
                    performancePicture.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public int PerformanceId
        {
            get { return performancePicture.PerformanceId; }
            set
            {
                if (performancePicture.PerformanceId != value)
                {
                    performancePicture.PerformanceId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceId)));
                }
            }
        }

        public string PictureURL
        {
            get { return performancePicture.PictureURL; }
        }

        public PerformanceMediaCollectionVM PerformanceMediaCollectionVM
        {
            get { return performanceMediaCollectionVM; }
        }

        public PerformancePicture PerformancePicture
        {
            get { return performancePicture; }
        }


    }
}
