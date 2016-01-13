using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class PerformanceVideo
    {
        public PerformanceVideo()
        {
        }

        public PerformanceVideo(string videoURL, int performanceId, int id = 0)
        {
            this.Id = id;
            this.VideoURL = videoURL;
            this.PerformanceId = performanceId;
        }

        public int PerformanceId { get; set; }

        public int Id { get; set; }

        public string VideoURL { get; set; }
    }
}
