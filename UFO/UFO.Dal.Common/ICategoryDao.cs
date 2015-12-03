using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface ICategoryDao
    {
        Category FindById(int id);
        IList<Category> FindAll();
    }
}
