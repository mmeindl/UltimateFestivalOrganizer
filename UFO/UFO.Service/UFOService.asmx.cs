using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UFO.Domain;
using UFO.Server;

namespace UFO.Service
{
    /// <summary>
    /// Summary description for UFOService
    /// </summary>
    [WebService(Namespace = "http://ufo.fh-hagenberg.at/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UFOService : System.Web.Services.WebService
    {
        private IUFOServer server = UFOServerFactory.GetUFOServer();

        // Category
        [WebMethod]
        public Category FindCategoryById(int id)
        {
            return server.FindCategoryById(id);
        }

        [WebMethod]
        public List<Category> FindAllCategories()
        {
            return new List<Category>(server.FindAllCategories());
        }

        // User
        [WebMethod]
        public bool AuthenticateUser(string usernername, string password, List<string> error)
        {
            return server.AuthenticateUser(usernername, password, error);
        }

        [WebMethod]
        public User FindUserByName(string name)
        {
            return server.FindUserByName(name);
        }

        // Artist
        [WebMethod]
        public List<Artist> FindAllArtists()
        {
            return new List<Artist>(server.FindAllArtists());
        }

        [WebMethod]
        public Artist FindArtistByName(string name)
        {
            return server.FindArtistByName(name);
        }

        [WebMethod]
        public Artist FindArtistById(int artistId)
        {
            return server.FindArtistById(artistId);
        }

        [WebMethod]
        public bool InsertArtist(Artist artist)
        {
            return server.InsertArtist(artist);
        }

        [WebMethod]
        public bool UpdateArtist(Artist artist)
        {
            return server.UpdateArtist(artist);
        }

        [WebMethod]
        public bool DeleteArtist(Artist artist)
        {
            return server.DeleteArtist(artist);
        }

        // Performance
        [WebMethod]
        public Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            return server.FindPerformanceByDateTimeAndArtistId(dateTime, artistId);
        }

        [WebMethod]
        public List<Performance> FindPerformancesByDate(DateTime date)
        {
            return new List<Performance>(server.FindPerformancesByDate(date));
        }

        [WebMethod]
        public List<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue)
        {
            return new List<Performance>(server.FindPerformancesByDateAndVenue(date, venue));
        }

        [WebMethod]
        public List<DateTime> GetPerformanceDates()
        {
            return new List<DateTime>(server.GetPerformanceDates());
        }

        [WebMethod]
        public bool InsertPerformance(Performance performance)
        {
            return server.InsertPerformance(performance);
        }

        [WebMethod]
        public bool UpdatePerformance(Performance performance)
        {
            return server.UpdatePerformance(performance);
        }

        [WebMethod]
        public bool DeletePerformance(Performance performance)
        {
            return server.DeletePerformance(performance);
        }

        [WebMethod]
        public List<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist)
        {
            return new List<Performance>(server.FindPerformancesByDateAndArtist(date, artist));
        }

        // Venue
        [WebMethod]
        public List<Venue> FindAllVenues()
        {
            return new List<Venue>(server.FindAllVenues());
        }

        [WebMethod]
        public Venue FindVenueById(int id)
        {
            return server.FindVenueById(id);
        }

        [WebMethod]
        public Venue FindVenueByName(string name)
        {
            return server.FindVenueByName(name);
        }

        [WebMethod]
        public List<Venue> FindVenuesByAreaId(int areaId)
        {
            return new List<Venue>(server.FindVenuesByAreaId(areaId));
        }

        [WebMethod]
        public bool InsertVenue(Venue venue)
        {
            return server.InsertVenue(venue);
        }

        [WebMethod]
        public bool UpdateVenue(Venue venue)
        {
            return server.UpdateVenue(venue);
        }

        [WebMethod]
        public bool DeleteVenue(Venue venue)
        {
            return server.DeleteVenue(venue);
        }

        // Area
        [WebMethod]
        public Area FindAreaByName(string name)
        {
            return server.FindAreaByName(name);
        }

        [WebMethod]
        public List<Area> FindAllAreas()
        {
            return new List<Area>(server.FindAllAreas());
        }

        [WebMethod]
        public bool InsertArea(Area area)
        {
            return server.InsertArea(area);
        }

        [WebMethod]
        public bool DeleteArea(Area area)
        {
            return server.DeleteArea(area);
        }

        // Country
        [WebMethod]
        public Country FindCountryByAbbreviation(string abbreviation)
        {
            return server.FindCountryByAbbreviation(abbreviation);
        }

        [WebMethod]
        public List<Country> FindAllCountries()
        {
            return new List<Country>(server.FindAllCountries());
        }

        // ArtistPicture
        [WebMethod]
        public ArtistPicture FindArtistPictureByURL(string url)
        {
            return server.FindArtistPictureByURL(url);
        }

        [WebMethod]
        public List<ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            return new List<ArtistPicture>(server.FindAllPicturesByArtistId(artistId));
        }

        [WebMethod]
        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            return server.FindProfilePictureByArtistId(artistId);
        }

        [WebMethod]
        public bool InsertArtistPicture(ArtistPicture artistPicture)
        {
            return server.InsertArtistPicture(artistPicture);
        }

        [WebMethod]
        public bool UpdateArtistPicture(ArtistPicture artistPicture)
        {
            return server.UpdateArtistPicture(artistPicture);
        }

        [WebMethod]
        public bool DeleteArtistPicture(ArtistPicture artistPicture)
        {
            return server.DeleteArtistPicture(artistPicture);
        }

        // ArtistVideo
        [WebMethod]
        public ArtistVideo FindArtistVideoByURL(string url)
        {
            return server.FindArtistVideoByURL(url);
        }

        [WebMethod]
        public List<ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            return new List<ArtistVideo>(server.FindAllVideosByArtistId(artistId));
        }

        [WebMethod]
        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            return server.FindPromoVideoByArtistId(artistId);
        }

        [WebMethod]
        public bool InsertArtistVideo(ArtistVideo artistVideo)
        {
            return server.InsertArtistVideo(artistVideo);
        }

        [WebMethod]
        public bool UpdateArtistVideo(ArtistVideo artistVideo)
        {
            return server.UpdateArtistVideo(artistVideo);
        }

        [WebMethod]
        public bool DeleteArtistVideo(ArtistVideo artistVideo)
        {
            return server.DeleteArtistVideo(artistVideo);
        }

        // PerformancePicture
        [WebMethod]
        public PerformancePicture FindPerformancePictureByURL(string url)
        {
            return server.FindPerformancePictureByURL(url);
        }

        [WebMethod]
        public List<PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {
            return new List<PerformancePicture>(server.FindAllPicturesByPerformanceId(performanceId));
        }

        [WebMethod]
        public bool InsertPerformancePicture(PerformancePicture performancePicture)
        {
            return server.InsertPerformancePicture(performancePicture);
        }

        [WebMethod]
        public bool DeletePerformancePicture(PerformancePicture performancePicture)
        {
            return server.DeletePerformancePicture(performancePicture);
        }

        // PerformanceVideo
        [WebMethod]
        public PerformanceVideo FindPerformanceVideoByURL(string url)
        {
            return server.FindPerformanceVideoByURL(url);
        }

        [WebMethod]
        public List<PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {
            return new List<PerformanceVideo>(server.FindAllVideosByPerformanceId(performanceId));
        }

        [WebMethod]
        public bool InsertPerformanceVideo(PerformanceVideo performanceVideo)
        {
            return server.InsertPerformanceVideo(performanceVideo);
        }

        [WebMethod]
        public bool DeletePerformanceVideo(PerformanceVideo performanceVideo)
        {
            return server.DeletePerformanceVideo(performanceVideo);
        }

        // helpers
        [WebMethod]
        public bool UpdateArtistMedia(Artist artist, ArtistPicture picture, ArtistVideo video)
        {
            return server.UpdateArtistMedia(artist, picture, video);
        }
    }
}
