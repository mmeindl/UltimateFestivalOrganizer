using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Area
    {
        public Area()
        {
        }

        public Area(string name, int id = 0)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
