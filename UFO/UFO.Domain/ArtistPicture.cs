using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class ArtistPicture
    {
        public ArtistPicture()
        {
        }

        public ArtistPicture(string pictureURL, int artistId, bool isProfilePicture = false, int id = 0)
        {
            this.Id = id;
            this.PictureURL = pictureURL;
            this.ArtistId = artistId;
            this.IsProfilePicture = isProfilePicture;
        }

        public int ArtistId { get; set; }

        public int Id { get; set; }

        public bool IsProfilePicture { get; set; }

        public string PictureURL { get; set; }
    }
}
