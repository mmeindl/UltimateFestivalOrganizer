using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IArtistDao
    {
        Artist FindById(int id);
        Artist FindByName(string name);
        IList<Artist> FindAll();
        bool Insert(Artist artist);
        bool Update(Artist artist);
        bool Delete(Artist artist);
    }
}
