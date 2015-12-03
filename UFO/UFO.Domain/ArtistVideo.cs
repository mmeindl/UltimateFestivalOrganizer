using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class ArtistVideo
    {
        public ArtistVideo()
        {
        }

        public ArtistVideo(string videoURL, int artistId, bool isPromoVideo = false, int id = 0)
        {
            this.Id = id;
            this.VideoURL = videoURL;
            this.ArtistId = artistId;
            this.IsPromoVideo = isPromoVideo;
        }

        public int ArtistId { get; set; }

        public int Id { get; set; }

        public bool IsPromoVideo { get; set; }

        public string VideoURL { get; }
    }
}
