using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface ICountryDao
    {
        Country FindByAbbreviation(string abbreviation);
        IList<Country> FindAll();
    }
}
