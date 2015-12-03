using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Country
    {
        public Country()
        {
        }

        public Country(string abbreviation, string name)
        {
            this.Abbreviation = abbreviation;
            this.Name = name;
        }

        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
