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
            var service = new UFOService();

            return service.FindAllArtists()
                .Select(a => mapArtist(a))
                .ToList();
        }

        public Domain.Artist FindArtistByName(string name)
        {
            var service = new UFOService();

            return mapArtist(service.FindArtistByName(name));
        }

        public Domain.Artist FindArtistById(int artistId)
        {
            var service = new UFOService();

            return mapArtist(service.FindArtistById(artistId));
        }

        public bool InsertArtist(Domain.Artist artist)
        {
            var service = new UFOService();

            return service.InsertArtist(mapArtist(artist));
        }

        public bool UpdateArtist(Domain.Artist artist)
        {
            var service = new UFOService();

            return service.UpdateArtist(mapArtist(artist));
        }

        public bool DeleteArtist(Domain.Artist artist)
        {
            var service = new UFOService();

            return service.DeleteArtist(mapArtist(artist));
        }

        // Performance
        public Domain.Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            var service = new UFOService();

            return mapPerformance(service.FindPerformanceByDateTimeAndArtistId(dateTime, artistId));
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDate(DateTime date)
        {
            var service = new UFOService();

            return service.FindPerformancesByDate(date)
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndVenue(DateTime date, Domain.Venue venue)
        {
            var service = new UFOService();

            return service.FindPerformancesByDateAndVenue(date, mapVenue(venue))
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {
            var service = new UFOService();

            return service.GetPerformanceDates();
        }

        public bool InsertPerformance(Domain.Performance performance)
        {
            var service = new UFOService();

            return service.InsertPerformance(mapPerformance(performance));
        }

        public bool UpdatePerformance(Domain.Performance performance)
        {
            var service = new UFOService();

            return service.UpdatePerformance(mapPerformance(performance));
        }

        public bool DeletePerformance(Domain.Performance performance)
        {
            var service = new UFOService();

            return service.DeletePerformance(mapPerformance(performance));
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndArtist(DateTime date, Domain.Artist artist)
        {
            var service = new UFOService();

            return service.FindPerformancesByDateAndArtist(date, mapArtist(artist))
                .Select(p => mapPerformance(p))
                .ToList();
        }

        // Venue
        public IEnumerable<Domain.Venue> FindAllVenues()
        {
            var service = new UFOService();

            return service.FindAllVenues()
                .Select(v => mapVenue(v))
                .ToList();
        }

        public Domain.Venue FindVenueById(int id)
        {
            var service = new UFOService();

            return mapVenue(service.FindVenueById(id));
        }

        public Domain.Venue FindVenueByName(string name)
        {
            var service = new UFOService();

            return mapVenue(service.FindVenueByName(name));
        }

        public IEnumerable<Domain.Venue> FindVenuesByAreaId(int areaId)
        {
            var service = new UFOService();

            return service.FindVenuesByAreaId(areaId)
                .Select(v => mapVenue(v))
                .ToList();
        }

        public bool InsertVenue(Domain.Venue venue)
        {
            var service = new UFOService();

            return service.InsertVenue(mapVenue(venue));
        }

        public bool UpdateVenue(Domain.Venue venue)
        {
            var service = new UFOService();

            return service.UpdateVenue(mapVenue(venue));
        }

        public bool DeleteVenue(Domain.Venue venue)
        {
            var service = new UFOService();

            return service.DeleteVenue(mapVenue(venue));
        }

        // Area
        public Domain.Area FindAreaByName(string name)
        {
            var service = new UFOService();

            return mapArea(service.FindAreaByName(name));
        }

        public IList<Domain.Area> FindAllAreas()
        {
            var service = new UFOService();

            return service.FindAllAreas()
                .Select(a => mapArea(a))
                .ToList();
        }

        public bool InsertArea(Domain.Area area)
        {
            var service = new UFOService();

            return service.InsertArea(mapArea(area));
        }

        public bool DeleteArea(Domain.Area area)
        {
            var service = new UFOService();

            return service.DeleteArea(mapArea(area));
        }

        // Country
        public Domain.Country FindCountryByAbbreviation(string abbreviation)
        {
            var service = new UFOService();

            return mapCountry(service.FindCountryByAbbreviation(abbreviation));
        }

        public IList<Domain.Country> FindAllCountries()
        {
            var service = new UFOService();

            return service.FindAllCountries()
                .Select(c => mapCountry(c))
                .ToList();
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

        private Domain.Artist mapArtist(UFOWebService.Artist a)
        {
            return new Domain.Artist(a.CountryId, a.CategoryId, a.Name, a.Email, a.WebsiteURL, a.IsDeleted, a.Id);
        }

        private UFOWebService.Artist mapArtist(Domain.Artist a)
        {
            UFOWebService.Artist artist = new UFOWebService.Artist();
            artist.CountryId = a.CountryId;
            artist.CategoryId = a.CategoryId;
            artist.Name = a.Name;
            artist.Email = a.Email;
            artist.WebsiteURL = a.WebsiteURL;
            artist.IsDeleted = a.IsDeleted;
            artist.Id = a.Id;

            return artist;
        }

        private Domain.Performance mapPerformance(UFOWebService.Performance p)
        {
            return new Domain.Performance(p.DateTime, p.VenueId, p.ArtistId, p.Id);
        }

        private UFOWebService.Performance mapPerformance(Domain.Performance p)
        {
            UFOWebService.Performance performance = new UFOWebService.Performance();
            performance.Id = p.Id;
            performance.VenueId = p.VenueId;
            performance.ArtistId = p.ArtistId;
            performance.DateTime = p.DateTime;

            return performance;
        }

        private Domain.Venue mapVenue(UFOWebService.Venue v)
        {
            return new Domain.Venue(v.AreaId, v.Name, v.ShortName, v.GeoLocationLat, v.GeoLocationLon, v.Id);
        }

        private UFOWebService.Venue mapVenue(Domain.Venue v)
        {
            UFOWebService.Venue venue = new UFOWebService.Venue();
            venue.Id = v.Id;
            venue.AreaId = v.AreaId;
            venue.Name = v.Name;
            venue.ShortName = v.ShortName;
            venue.GeoLocationLat = v.GeoLocationLat;
            venue.GeoLocationLon = v.GeoLocationLon;

            return venue;
        }

        private Domain.Area mapArea(UFOWebService.Area a)
        {
            return new Domain.Area(a.Name, a.Id);
        }

        private UFOWebService.Area mapArea(Domain.Area a)
        {
            UFOWebService.Area area = new UFOWebService.Area();
            area.Id = a.Id;
            area.Name = a.Name;

            return area;
        }

        private Domain.Country mapCountry(UFOWebService.Country c)
        {
            return new Domain.Country(c.Abbreviation, c.Name);
        }

        private UFOWebService.Country mapCountry(Domain.Country c)
        {
            UFOWebService.Country country = new UFOWebService.Country();
            country.Abbreviation = c.Abbreviation;
            country.Name = c.Name;

            return country;
        }
        private Domain.PerformanceVideo mapPerformanceVideo(UFOWebService.PerformanceVideo pv)
        {
            return new Domain.PerformanceVideo();
        }


    }
}
