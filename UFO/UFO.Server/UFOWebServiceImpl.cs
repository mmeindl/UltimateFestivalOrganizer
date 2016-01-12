using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UFO.Dal.Common;
using UFO.Domain;

namespace UFO.Server
{
    internal class UFOWebServiceImpl : IUFOServer
    {
        // Category
        public Category FindCategoryById(int id)
        {

        }

        public IEnumerable<Category> FindAllCategories()
        {

        }

        // User
        public bool AuthenticateUser(string usernername, string password, IList<string> error)
        {
            
        }

        public User FindUserByName(string name)
        {

        }

        // Artist
        public IEnumerable<Artist> FindAllArtists()
        {

        }

        public Artist FindArtistByName(string name)
        {

        }

        public Artist FindArtistById(int artistId)
        {

        }

        public bool InsertArtist(Artist artist)
        {

        }

        public bool UpdateArtist(Artist artist)
        {

        }

        public bool DeleteArtist(Artist artist)
        {

        }

        // Performance
        public Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {

        }

        public IEnumerable<Performance> FindPerformancesByDate(DateTime date)
        {

        }

        public IEnumerable<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue)
        {

        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {

        }

        public bool InsertPerformance(Performance performance)
        {

        }

        public bool UpdatePerformance(Performance performance)
        {

        }
        
        public bool DeletePerformance(Performance performance)
        {

        }

        public IEnumerable<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist)
        {

        }

        // Venue
        public IEnumerable<Venue> FindAllVenues()
        {

        }

        public Venue FindVenueById(int id)
        {

        }

        public Venue FindVenueByName(string name)
        {

        }

        public IEnumerable<Venue> FindVenuesByAreaId(int areaId)
        {

        }

        public bool InsertVenue(Venue venue)
        {

        }

        public bool UpdateVenue(Venue venue)
        {

        }

        public bool DeleteVenue(Venue venue)
        {

        }

        // Area
        public Area FindAreaByName(string name)
        {

        }

        public IList<Area> FindAllAreas()
        {

        }

        public bool InsertArea(Area area)
        {

        }

        public bool DeleteArea(Area area)
        {

        }

        // Country
        public Country FindCountryByAbbreviation(string abbreviation)
        {

        }

        public IList<Country> FindAllCountries()
        {

        }

        // ArtistPicture
        public ArtistPicture FindArtistPictureByURL(string url)
        {

        }

        public IList<ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {

        }

        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {

        }

        public bool InsertArtistPicture(ArtistPicture artistPicture)
        {

        }

        public bool UpdateArtistPicture(ArtistPicture artistPicture)
        {

        }

        public bool DeleteArtistPicture(ArtistPicture artistPicture)
        {

        }

        // ArtistVideo
        public ArtistVideo FindArtistVideoByURL(string url)
        {

        }

        public IList<ArtistVideo> FindAllVideosByArtistId(int artistId)
        {

        }

        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {

        }

        public bool InsertArtistVideo(ArtistVideo artistVideo)
        {

        }

        public bool UpdateArtistVideo(ArtistVideo artistVideo)
        {

        }

        public bool DeleteArtistVideo(ArtistVideo artistVideo)
        {

        }

        // PerformancePicture
        public PerformancePicture FindPerformancePictureByURL(string url)
        {

        }

        public IList<PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {

        }

        public bool InsertPerformancePicture(PerformancePicture performancePicture)
        {

        }

        public bool DeletePerformancePicture(PerformancePicture performancePicture)
        {

        }

        // PerformanceVideo
        public PerformanceVideo FindPerformanceVideoByURL(string url)
        {

        }

        public IList<PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {

        }

        public bool InsertPerformanceVideo(PerformanceVideo performanceVideo)
        {

        }

        public bool DeletePerformanceVideo(PerformanceVideo performanceVideo)
        {

        }

        // helpers
        public bool UpdateArtistMedia(Artist artist, ArtistPicture picture, ArtistVideo video)
        {
            bool result = true;

            result = UpdateArtist(artist);
            result = result & UpdateArtistPicture(picture);
            result = result & UpdateArtistVideo(video);

            return result;
        }

    }
}
