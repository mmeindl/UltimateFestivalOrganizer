using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IArtistVideoDao
    {
        ArtistVideo FindByURL(string url);
        IList<ArtistVideo> FindAllByArtistId(int artistId);
        ArtistVideo FindPromoVideoByArtistId(int artistId);

        bool Insert(ArtistVideo artistVideo);
        bool Update(ArtistVideo artistVideo);
        bool Delete(ArtistVideo artistVideo);
    }
}
