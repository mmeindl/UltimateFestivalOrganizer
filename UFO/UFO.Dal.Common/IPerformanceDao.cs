using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IPerformanceDao
    {
        Performance FindById(int id);
        IList<Performance> FindAll();
        IList<Performance> FindAllByDate(DateTime date);
        IList<Performance> FindAllInFuture();
        IList<Performance> FindAllByArtistId(int artistId);
        IList<Performance> FindAllByVenueId(int venueId);
        IList<Performance> FindAllInFutureByArtistId(int artistId);

        bool Insert(Performance performance);
        bool Update(Performance performance);
        bool Delete(Performance performance);
        bool DeleteAllInFutureByArtistId(int artistId);

    }
}
