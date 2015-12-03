using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IVenueDao
    {
        Venue FindById(int id);
        IList<Venue> FindAll();

        bool Insert(Venue venue);
        bool Update(Venue venue);
    }
}
