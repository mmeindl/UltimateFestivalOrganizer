using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IUserDao
    {
        User FindById(int id);
        User FindByUsername(string username);
        IList<User> FindAll();
    }
}
