using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Server
{
    public interface IUFOServer
    {
        bool CheckLoginData(string username, string password);
        void InformAllArtists();

        // Artist
        Artist FindArtistById(int artistId);
        IEnumerable<Artist> FindAllArtists();

        bool InsertArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artists);

        // Venue
        Venue FindVenueById(int id);
        IEnumerable<Venue> FindAllVenues();

        bool InsertVenue(Venue venue);
        bool UpdateVenue(Venue venue);

        // User
        User findUserByName(string name);
        IEnumerable<User> findAllUsers();

        //Category
        Category FindCategoryById(int id);
        IEnumerable<Category> FindAllCategories();

        //Performance
        Performance FindPerformanceById(int id);
        IEnumerable<Performance> FindAllPerformances();

        bool InsertPerformance(Performance performance);
        bool UpdatePerformance(Performance performance);
        bool DeletePerformance(Performance performance);




    }
}
