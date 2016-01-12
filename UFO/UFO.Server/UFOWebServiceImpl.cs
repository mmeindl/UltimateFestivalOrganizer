using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UFO.Dal.Common;
using UFO.Domain;
using UFO.Server.UFOWebService;

namespace UFO.Server
{
    internal class UFOWebServiceImpl : IUFOServer
    {
        // Category
        public Domain.Category FindCategoryById(int id)
        {
            var service = new UFOService();

            return mapCategory(service.FindCategoryById(id));
        }

        public IEnumerable<Domain.Category> FindAllCategories()
        {
            var service = new UFOService();

            return service.FindAllCategories()
                .Select(c => mapCategory(c))
                .ToList();
        }

        // User
        public bool AuthenticateUser(string usernername, string password, IList<string> error)
        {
            var service = new UFOService();

            return service.AuthenticateUser(usernername, password, error.ToArray());
        }

        public Domain.User FindUserByName(string name)
        {
            var service = new UFOService();

            return mapUser(service.FindUserByName(name));
        }

        // Artist
        public IEnumerable<Artist> FindAllArtists()
        {
            throw new NotImplementedException();
        }

        public Artist FindArtistByName(string name)
        {
            throw new NotImplementedException();
        }

        public Artist FindArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        // Performance
        public Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Performance> FindPerformancesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformance(Performance performance)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePerformance(Performance performance)
        {
            throw new NotImplementedException();
        }
        
        public bool DeletePerformance(Performance performance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist)
        {
            throw new NotImplementedException();
        }

        // Venue
        public IEnumerable<Venue> FindAllVenues()
        {
            throw new NotImplementedException();
        }

        public Venue FindVenueById(int id)
        {
            throw new NotImplementedException();
        }

        public Venue FindVenueByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venue> FindVenuesByAreaId(int areaId)
        {
            throw new NotImplementedException();
        }

        public bool InsertVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        // Area
        public Area FindAreaByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Area> FindAllAreas()
        {
            throw new NotImplementedException();
        }

        public bool InsertArea(Area area)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArea(Area area)
        {
            throw new NotImplementedException();
        }

        // Country
        public Country FindCountryByAbbreviation(string abbreviation)
        {
            throw new NotImplementedException();
        }

        public IList<Country> FindAllCountries()
        {
            throw new NotImplementedException();
        }

        // ArtistPicture
        public ArtistPicture FindArtistPictureByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtistPicture(ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtistPicture(ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtistPicture(ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        // ArtistVideo
        public ArtistVideo FindArtistVideoByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtistVideo(ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtistVideo(ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtistVideo(ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        // PerformancePicture
        public PerformancePicture FindPerformancePictureByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformancePicture(PerformancePicture performancePicture)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerformancePicture(PerformancePicture performancePicture)
        {
            throw new NotImplementedException();
        }

        // PerformanceVideo
        public PerformanceVideo FindPerformanceVideoByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformanceVideo(PerformanceVideo performanceVideo)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerformanceVideo(PerformanceVideo performanceVideo)
        {
            throw new NotImplementedException();
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

        private Domain.Category mapCategory(UFOWebService.Category c)
        {
            return new Domain.Category(c.Name, c.Color, c.Id);
        }

        private Domain.User mapUser(UFOWebService.User u)
        {
            return new Domain.User(u.Username, u.Password, u.Email, u.RoleId, u.Id);
        }

    }
}
