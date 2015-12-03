using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IPerformancePictureDao
    {
        PerformancePicture FindByURL(string url);
        IList<PerformancePicture> FindAllByPerformanceId(int performanceId);

        bool Insert(PerformancePicture performancePicture);
        bool Delete(PerformancePicture performancePicture);
    }
}
