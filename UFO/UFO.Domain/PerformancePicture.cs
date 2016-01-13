using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class PerformancePicture
    {
        public PerformancePicture()
        {
        }

        public PerformancePicture(string pictureURL, int performanceId, int id = 0)
        {
            this.Id = id;
            this.PictureURL = pictureURL;
            this.PerformanceId = performanceId;
        }

        public int PerformanceId { get; set; }

        public int Id { get; set; }

        public string PictureURL { get; set; }
    }
}
