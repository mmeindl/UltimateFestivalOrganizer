using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Video
    {
        public Video()
        {
        }

        public Video(string url)
        {
            this.URL = url;
        }

        public string URL { get; set; }
    }
}
