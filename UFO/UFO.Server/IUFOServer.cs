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
        void InformAllArtists();

        // Artist
        Artist FindArtistById(int artistId);
        Artist FindArtistByName(string name);
        IEnumerable<Artist> FindAllArtists();

        bool InsertArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artists);

        // ArtistPicture
        ArtistPicture FindArtistPictureByURL(string url);
        IList<ArtistPicture> FindAllPicturesByArtistId(int artistId);
        ArtistPicture FindProfilePictureByArtistId(int artistId);

        bool InsertArtistPicture(ArtistPicture artistPicture);
        bool UpdateArtistPicture(ArtistPicture artistPicture);
        bool DeleteArtistPicture(ArtistPicture artistPicture);

        // ArtistVideo
        ArtistVideo FindArtistVideoByURL(string url);
        IList<ArtistVideo> FindAllVideosByArtistId(int artistId);
        ArtistVideo FindPromoVideoByArtistId(int artistId);

        bool InsertArtistVideo(ArtistVideo artistVideo);
        bool UpdateArtistVideo(ArtistVideo artistVideo);
        bool DeleteArtistVideo(ArtistVideo artistVideo);

        // Venue
        Venue FindVenueById(int id);
        IEnumerable<Venue> FindAllVenues();

        bool InsertVenue(Venue venue);
        bool UpdateVenue(Venue venue);

        // User
        bool AuthenticateUser(string usernername, string password, IList<string> error);
        User FindUserByName(string name);
        IEnumerable<User> FindAllUsers();

        //Category
        Category FindCategoryById(int id);
        IEnumerable<Category> FindAllCategories();

        //Country
        Country FindCountryByAbbreviation(string abbreviation);
        IList<Country> FindAllCountries();

        //Performance
        Performance FindPerformanceById(int id);
        IEnumerable<Performance> FindAllPerformances();

        bool InsertPerformance(Performance performance);
        bool UpdatePerformance(Performance performance);
        bool DeletePerformance(Performance performance);

        // helpers
        bool UpdateArtistMedia(Artist artist, ArtistPicture picture, ArtistVideo video);


    }
}
