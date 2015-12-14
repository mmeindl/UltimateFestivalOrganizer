using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IAreaDao
    {
        Area FindById(int id);
        Area FindByName(string name);
        IList<Area> FindAll();
        bool Insert(Area area);
        bool Update(Area area);
        bool Delete(Area area);
    }
}
