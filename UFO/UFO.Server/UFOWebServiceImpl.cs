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
        public IEnumerable<Domain.Artist> FindAllArtists()
        {
            throw new NotImplementedException();
        }

        public Domain.Artist FindArtistByName(string name)
        {
            throw new NotImplementedException();
        }

        public Domain.Artist FindArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtist(Domain.Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtist(Domain.Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtist(Domain.Artist artist)
        {
            throw new NotImplementedException();
        }

        // Performance
        public Domain.Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndVenue(DateTime date, Domain.Venue venue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformance(Domain.Performance performance)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePerformance(Domain.Performance performance)
        {
            throw new NotImplementedException();
        }
        
        public bool DeletePerformance(Domain.Performance performance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndArtist(DateTime date, Domain.Artist artist)
        {
            throw new NotImplementedException();
        }

        // Venue
        public IEnumerable<Domain.Venue> FindAllVenues()
        {
            throw new NotImplementedException();
        }

        public Domain.Venue FindVenueById(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Venue FindVenueByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Venue> FindVenuesByAreaId(int areaId)
        {
            throw new NotImplementedException();
        }

        public bool InsertVenue(Domain.Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVenue(Domain.Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVenue(Domain.Venue venue)
        {
            throw new NotImplementedException();
        }

        // Area
        public Domain.Area FindAreaByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Area> FindAllAreas()
        {
            throw new NotImplementedException();
        }

        public bool InsertArea(Domain.Area area)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArea(Domain.Area area)
        {
            throw new NotImplementedException();
        }

        // Country
        public Domain.Country FindCountryByAbbreviation(string abbreviation)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Country> FindAllCountries()
        {
            throw new NotImplementedException();
        }

        // ArtistPicture
        public Domain.ArtistPicture FindArtistPictureByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public Domain.ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtistPicture(Domain.ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtistPicture(Domain.ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtistPicture(Domain.ArtistPicture artistPicture)
        {
            throw new NotImplementedException();
        }

        // ArtistVideo
        public Domain.ArtistVideo FindArtistVideoByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public Domain.ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool InsertArtistVideo(Domain.ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtistVideo(Domain.ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtistVideo(Domain.ArtistVideo artistVideo)
        {
            throw new NotImplementedException();
        }

        // PerformancePicture
        public Domain.PerformancePicture FindPerformancePictureByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformancePicture(Domain.PerformancePicture performancePicture)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerformancePicture(Domain.PerformancePicture performancePicture)
        {
            throw new NotImplementedException();
        }

        // PerformanceVideo
        public Domain.PerformanceVideo FindPerformanceVideoByURL(string url)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformanceVideo(Domain.PerformanceVideo performanceVideo)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerformanceVideo(Domain.PerformanceVideo performanceVideo)
        {
            throw new NotImplementedException();
        }

        // helpers
        public bool UpdateArtistMedia(Domain.Artist artist, Domain.ArtistPicture picture, Domain.ArtistVideo video)
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
