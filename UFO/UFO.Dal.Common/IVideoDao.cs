using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IVideoDao
    {
        Video FindByURL(string videoURL);
        IList<Video> FindAll();

        bool Insert(Video video);
        bool Delete(Video video);
    }
}
