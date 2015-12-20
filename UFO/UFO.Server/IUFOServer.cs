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

        //Area
        Area FindAreaById(int id);
        Area FindAreaByName(string name);
        IList<Area> FindAllAreas();

        bool InsertArea(Area area);
        bool UpdateArea(Area area);
        bool DeleteArea(Area area);

        // Venue
        Venue FindVenueById(int id);
        Venue FindVenueByName(string name);
        IEnumerable<Venue> FindVenuesByAreaId(int areaId);
        IEnumerable<Venue> FindAllVenues();

        bool InsertVenue(Venue venue);
        bool UpdateVenue(Venue venue);
        bool DeleteVenue(Venue venue);

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
        Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId);
        IEnumerable<Performance> FindAllPerformances();
        IEnumerable<Performance> FindPerformancesByDate(DateTime date);
        IEnumerable<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue);
        IEnumerable<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist);

        IEnumerable<DateTime> GetPerformanceDates();

        bool InsertPerformance(Performance performance);
        bool UpdatePerformance(Performance performance);
        bool DeletePerformance(Performance performance);

        // PerformancePicture
        PerformancePicture FindPerformancePictureByURL(string url);
        IList<PerformancePicture> FindAllPicturesByPerformanceId(int PerformanceId);

        bool InsertPerformancePicture(PerformancePicture PerformancePicture);
        bool DeletePerformancePicture(PerformancePicture PerformancePicture);

        // PerformanceVideo
        PerformanceVideo FindPerformanceVideoByURL(string url);
        IList<PerformanceVideo> FindAllVideosByPerformanceId(int PerformanceId);

        bool InsertPerformanceVideo(PerformanceVideo PerformanceVideo);
        bool DeletePerformanceVideo(PerformanceVideo PerformanceVideo);

        // helpers
        bool UpdateArtistMedia(Artist artist, ArtistPicture picture, ArtistVideo video);


    }
}
