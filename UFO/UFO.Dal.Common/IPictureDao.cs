using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Dal.Common
{
    public interface IPictureDao
    {
        Picture FindByURL(string pictureURL);
        IList<Picture> FindAll();

        bool Insert(Picture picture);
        bool Delete(Picture picture);
    }
}
