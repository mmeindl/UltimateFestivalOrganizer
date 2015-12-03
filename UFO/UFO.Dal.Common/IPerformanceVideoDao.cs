using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IPerformanceVideoDao
    {
        PerformanceVideo FindByURL(string url);
        IList<PerformanceVideo> FindAllByPerformanceId(int performanceId);

        bool Insert(PerformanceVideo performanceVideo);
        bool Delete(PerformanceVideo performanceVideo);
    }
}
