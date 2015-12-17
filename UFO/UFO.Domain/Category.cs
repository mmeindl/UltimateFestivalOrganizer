using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Category
    {
        public Category()
        {
        }

        public Category(string name, string color, int id = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Color = color;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
