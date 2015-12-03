using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Performance
    {
        public Performance()
        {
        }

        public Performance(DateTime dateTime, int venueId, int artistId, int id = 0)
        {
            this.Id = id;
            this.VenueId = venueId;
            this.ArtistId = artistId;
            this.DateTime = dateTime;
        }

        public int Id { get; set; }

        public int VenueId { get; set; }

        public int ArtistId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
