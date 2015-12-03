using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Artist
    {
        public Artist()
        {
        }

        public Artist(string countryId, int categoryId, string name, string email,
                      string websiteURL = "", bool isDeleted = false, int id = 0)
        {
            this.Id = id;
            this.CountryId = countryId;
            this.CategoryId = categoryId;
            this.Name = name;
            this.Email = email;
            this.WebsiteURL = websiteURL;
            this.IsDeleted = isDeleted;
        }

        public int Id { get; set; }

        public string CountryId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string WebsiteURL { get; set; }

        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
