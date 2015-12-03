using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{

    [Serializable]
    public class Venue
    {
        public Venue()
        {
        }

        public Venue(int areaId, string name, string shortName, decimal geoLocationLat, decimal geoLocationLon, int id = 0)
        {
            this.Id = id;
            this.AreaId = areaId;
            this.Name = name;
            this.ShortName = shortName;
            this.GeoLocationLat = geoLocationLat;
            this.GeoLocationLon = geoLocationLon;
        }

        public int Id { get; set; }

        public int AreaId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public decimal GeoLocationLat { get; set; }

        public decimal GeoLocationLon { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
