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
        private UFOService service;

        public UFOWebServiceImpl()
        {
            service = new UFOService();
        }

        // Category
        public Domain.Category FindCategoryById(int id)
        {
            return mapCategory(service.FindCategoryById(id));
        }

        public IEnumerable<Domain.Category> FindAllCategories()
        {
            return service.FindAllCategories()
                .Select(c => mapCategory(c))
                .ToList();
        }

        // User
        public bool AuthenticateUser(string usernername, string password, IList<string> error)
        {
            return service.AuthenticateUser(usernername, password, error.ToArray());
        }

        public Domain.User FindUserByName(string name)
        {
            return mapUser(service.FindUserByName(name));
        }

        // Artist
        public IEnumerable<Domain.Artist> FindAllArtists()
        {
            return service.FindAllArtists()
                .Select(a => mapArtist(a))
                .ToList();
        }

        public Domain.Artist FindArtistByName(string name)
        {
            return mapArtist(service.FindArtistByName(name));
        }

        public Domain.Artist FindArtistById(int artistId)
        {
            return mapArtist(service.FindArtistById(artistId));
        }

        public bool InsertArtist(Domain.Artist artist)
        {
            return service.InsertArtist(mapArtist(artist));
        }

        public bool UpdateArtist(Domain.Artist artist)
        {
            return service.UpdateArtist(mapArtist(artist));
        }

        public bool DeleteArtist(Domain.Artist artist)
        {
            return service.DeleteArtist(mapArtist(artist));
        }

        // Performance
        public IEnumerable<Domain.Performance> FindAllPerformances()
        {
            return service.FindAllPerformances()
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public Domain.Performance FindPerformanceById(int id)
        {
            return mapPerformance(service.FindPerformanceById(id));
        }

        public Domain.Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            return mapPerformance(service.FindPerformanceByDateTimeAndArtistId(dateTime, artistId));
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDate(DateTime date)
        {
            return service.FindPerformancesByDate(date)
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateTime(DateTime date)
        {
            return service.FindPerformancesByDate(date)
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndVenue(DateTime date, Domain.Venue venue)
        {
            return service.FindPerformancesByDateAndVenue(date, mapVenue(venue))
                .Select(p => mapPerformance(p))
                .ToList();
        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {
            return service.GetPerformanceDates();
        }

        public bool InsertPerformance(Domain.Performance performance)
        {
            return service.InsertPerformance(mapPerformance(performance));
        }

        public bool UpdatePerformance(Domain.Performance performance)
        {
            return service.UpdatePerformance(mapPerformance(performance));
        }

        public bool DeletePerformance(Domain.Performance performance)
        {
            return service.DeletePerformance(mapPerformance(performance));
        }

        public IEnumerable<Domain.Performance> FindPerformancesByDateAndArtist(DateTime date, Domain.Artist artist)
        {
            return service.FindPerformancesByDateAndArtist(date, mapArtist(artist))
                .Select(p => mapPerformance(p))
                .ToList();
        }

        // Venue
        public IEnumerable<Domain.Venue> FindAllVenues()
        {
            return service.FindAllVenues()
                .Select(v => mapVenue(v))
                .ToList();
        }

        public Domain.Venue FindVenueById(int id)
        {
            return mapVenue(service.FindVenueById(id));
        }

        public Domain.Venue FindVenueByName(string name)
        {
            return mapVenue(service.FindVenueByName(name));
        }

        public IEnumerable<Domain.Venue> FindVenuesByAreaId(int areaId)
        {
            return service.FindVenuesByAreaId(areaId)
                .Select(v => mapVenue(v))
                .ToList();
        }

        public bool InsertVenue(Domain.Venue venue)
        {
            return service.InsertVenue(mapVenue(venue));
        }

        public bool UpdateVenue(Domain.Venue venue)
        {
            return service.UpdateVenue(mapVenue(venue));
        }

        public bool DeleteVenue(Domain.Venue venue)
        {
            return service.DeleteVenue(mapVenue(venue));
        }

        // Area
        public Domain.Area FindAreaByName(string name)
        {
            return mapArea(service.FindAreaByName(name));
        }

        public IEnumerable<Domain.Area> FindAllAreas()
        {
            return service.FindAllAreas()
                .Select(a => mapArea(a))
                .ToList();
        }

        public bool InsertArea(Domain.Area area)
        {
            return service.InsertArea(mapArea(area));
        }

        public bool DeleteArea(Domain.Area area)
        {
            return service.DeleteArea(mapArea(area));
        }
        
        // Country
        public Domain.Country FindCountryByAbbreviation(string abbreviation)
        {
            return mapCountry(service.FindCountryByAbbreviation(abbreviation));
        }

        public IEnumerable<Domain.Country> FindAllCountries()
        {
            return service.FindAllCountries()
                .Select(c => mapCountry(c))
                .ToList();
        }

        // ArtistPicture
        public Domain.ArtistPicture FindArtistPictureByURL(string url)
        {

            return mapArtistPicture(service.FindArtistPictureByURL(url));
        }

        public IEnumerable<Domain.ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            return service.FindAllPicturesByArtistId(artistId)
                .Select(p => mapArtistPicture(p))
                .ToList();
        }

        public Domain.ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            UFOWebService.ArtistPicture p = service.FindProfilePictureByArtistId(artistId);

            if (p != null)
            {
                return mapArtistPicture(p);
            }
            else
            {
                return null;
            }
        }

        public bool InsertArtistPicture(Domain.ArtistPicture artistPicture)
        {
            return service.InsertArtistPicture(mapArtistPicture(artistPicture));
        }

        public bool UpdateArtistPicture(Domain.ArtistPicture artistPicture)
        {
            return service.UpdateArtistPicture(mapArtistPicture(artistPicture));
        }

        public bool DeleteArtistPicture(Domain.ArtistPicture artistPicture)
        {
            return service.DeleteArtistPicture(mapArtistPicture(artistPicture));
        }

        // ArtistVideo
        public Domain.ArtistVideo FindArtistVideoByURL(string url)
        {
            return mapArtistVideo(service.FindArtistVideoByURL(url));
        }

        public IEnumerable<Domain.ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            return service.FindAllVideosByArtistId(artistId)
                .Select(v => mapArtistVideo(v))
                .ToList();
        }

        public Domain.ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            UFOWebService.ArtistVideo v = service.FindPromoVideoByArtistId(artistId);

            if (v != null)
            {
                return mapArtistVideo(v);
            }
            else
            {
                return null;
            }
        }

        public bool InsertArtistVideo(Domain.ArtistVideo artistVideo)
        {
            return service.InsertArtistVideo(mapArtistVideo(artistVideo));
        }

        public bool UpdateArtistVideo(Domain.ArtistVideo artistVideo)
        {
            return service.UpdateArtistVideo(mapArtistVideo(artistVideo));
        }

        public bool DeleteArtistVideo(Domain.ArtistVideo artistVideo)
        {
            return service.DeleteArtistVideo(mapArtistVideo(artistVideo));
        }

        // PerformancePicture
        public Domain.PerformancePicture FindPerformancePictureByURL(string url)
        {
            return mapPerformancePicture(service.FindPerformancePictureByURL(url));
        }

        public IEnumerable<Domain.PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {
            return service.FindAllPicturesByPerformanceId(performanceId)
                .Select(p => mapPerformancePicture(p))
                .ToList();
        }

        public bool InsertPerformancePicture(Domain.PerformancePicture performancePicture)
        {
            return service.InsertPerformancePicture(mapPerformancePicture(performancePicture));
        }

        public bool DeletePerformancePicture(Domain.PerformancePicture performancePicture)
        {
            return service.DeletePerformancePicture(mapPerformancePicture(performancePicture));
        }

        // PerformanceVideo
        public Domain.PerformanceVideo FindPerformanceVideoByURL(string url)
        {
            return mapPerformanceVideo(service.FindPerformanceVideoByURL(url));
        }

        public IEnumerable<Domain.PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {
            return service.FindAllVideosByPerformanceId(performanceId)
                .Select(v => mapPerformanceVideo(v))
                .ToList();
        }

        public bool InsertPerformanceVideo(Domain.PerformanceVideo performanceVideo)
        {
            return service.InsertPerformanceVideo(mapPerformanceVideo(performanceVideo));
        }

        public bool DeletePerformanceVideo(Domain.PerformanceVideo performanceVideo)
        {
            return service.DeletePerformanceVideo(mapPerformanceVideo(performanceVideo));
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

        // mapper
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


        private Domain.ArtistPicture mapArtistPicture(UFOWebService.ArtistPicture a)
        {
            return new Domain.ArtistPicture(a.PictureURL, a.ArtistId, a.IsProfilePicture, a.Id);
        }

        private UFOWebService.ArtistPicture mapArtistPicture(Domain.ArtistPicture a)
        {
            UFOWebService.ArtistPicture artistPicture = new UFOWebService.ArtistPicture();
            artistPicture.PictureURL = a.PictureURL;
            artistPicture.ArtistId = a.ArtistId;
            artistPicture.IsProfilePicture = a.IsProfilePicture;
            artistPicture.Id = a.Id;

            return artistPicture;
        }

        private Domain.ArtistVideo mapArtistVideo(UFOWebService.ArtistVideo a)
        {
            return new Domain.ArtistVideo(a.VideoURL, a.ArtistId, a.IsPromoVideo, a.Id);
        }

        private UFOWebService.ArtistVideo mapArtistVideo(Domain.ArtistVideo a)
        {
            UFOWebService.ArtistVideo artistVideo = new UFOWebService.ArtistVideo();
            artistVideo.VideoURL = a.VideoURL;
            artistVideo.ArtistId = a.ArtistId;
            artistVideo.IsPromoVideo = a.IsPromoVideo;
            artistVideo.Id = a.Id;

            return artistVideo;
        }

        private Domain.PerformancePicture mapPerformancePicture(UFOWebService.PerformancePicture p)
        {
            return new Domain.PerformancePicture(p.PictureURL, p.PerformanceId, p.Id);
        }

        private UFOWebService.PerformancePicture mapPerformancePicture(Domain.PerformancePicture p)
        {
            UFOWebService.PerformancePicture PerformancePicture = new UFOWebService.PerformancePicture();
            PerformancePicture.PictureURL = p.PictureURL;
            PerformancePicture.PerformanceId = p.PerformanceId;
            PerformancePicture.Id = p.Id;

            return PerformancePicture;
        }

        private Domain.PerformanceVideo mapPerformanceVideo(UFOWebService.PerformanceVideo p)
        {
            return new Domain.PerformanceVideo(p.VideoURL, p.PerformanceId, p.Id);
        }

        private UFOWebService.PerformanceVideo mapPerformanceVideo(Domain.PerformanceVideo p)
        {
            UFOWebService.PerformanceVideo PerformanceVideo = new UFOWebService.PerformanceVideo();
            PerformanceVideo.VideoURL = p.VideoURL;
            PerformanceVideo.PerformanceId = p.PerformanceId;
            PerformanceVideo.Id = p.Id;

            return PerformanceVideo;
        }

    }
}