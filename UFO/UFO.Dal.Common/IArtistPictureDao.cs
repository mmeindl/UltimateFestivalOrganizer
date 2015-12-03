using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IArtistPictureDao
    {
        ArtistPicture FindByURL(string url);
        IList<ArtistPicture> FindAllByArtistId(int artistId);
        ArtistPicture FindProfilePictureByArtistId(int artistId);

        bool Insert(ArtistPicture artistPicture);
        bool Update(ArtistPicture artistPicture);
        bool Delete(ArtistPicture artistPicture);
    }
}
