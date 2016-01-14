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
    internal class UFOServerImpl : IUFOServer
    {
        // Category
        public Category FindCategoryById(int id)
        {
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao();

            return categoryDao.FindById(id);
        }

        public IEnumerable<Category> FindAllCategories()
        {
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao();

            return categoryDao.FindAll();
        }

        // User
        public bool AuthenticateUser(string usernername, string password, IList<string> error)
        {
            IUserDao userDao = DalFactory.CreateUserDao();
            User user = userDao.FindByUsername(usernername);
            if (user == null)
            {
                error.Add("User does not exist");
                return false;
            }


            if (user.RoleId != 1)
            {
                error.Add("User has no Administration Rights");
                return false;
            }


            /*
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHash = Convert.ToBase64String(hashBytes);
            */

            string savedPasswordHash = user.Password;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    error.Add("Incorrect password");
                    return false;
                }
            }

            return true;     
        }

        public User FindUserByName(string name)
        {
            IUserDao userDao = DalFactory.CreateUserDao();

            return userDao.FindByUsername(name);
        }

        // Artist
        public IEnumerable<Artist> FindAllArtists()
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.FindAll();
        }

        public Artist FindArtistByName(string name)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.FindByName(name);
        }

        public Artist FindArtistById(int artistId)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.FindById(artistId);
        }

        public bool InsertArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.Insert(artist);
        }

        public bool UpdateArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.Update(artist);
        }

        public bool DeleteArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            return artistDao.Delete(artist);
        }

        // Performance
        public Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.FindByDateTimeAndArtistId(dateTime, artistId);
        }

        public IEnumerable<Performance> FindPerformancesByDate(DateTime date)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.FindAllByDate(date);
        }

        public IEnumerable<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.FindByDateAndVenue(date, venue);
        }

        public IEnumerable<DateTime> GetPerformanceDates()
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.GetPerformanceDates();
        }

        public bool InsertPerformance(Performance performance)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.Insert(performance);
        }

        public bool UpdatePerformance(Performance performance)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.Update(performance);
        }
        
        public bool DeletePerformance(Performance performance)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.Delete(performance);
        }

        public IEnumerable<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist)
        {
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            return performanceDao.FindByDateAndArtist(date, artist);
        }

        // Venue
        public IEnumerable<Venue> FindAllVenues()
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.FindAll();
        }

        public Venue FindVenueById(int id)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.FindById(id);
        }

        public Venue FindVenueByName(string name)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.FindByName(name);
        }

        public IEnumerable<Venue> FindVenuesByAreaId(int areaId)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.FindByAreaId(areaId);
        }

        public bool InsertVenue(Venue venue)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.Insert(venue);
        }

        public bool UpdateVenue(Venue venue)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.Update(venue);
        }

        public bool DeleteVenue(Venue venue)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            return venueDao.Delete(venue);
        }

        // Area
        public Area FindAreaByName(string name)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            return areaDao.FindByName(name);
        }

        public IList<Area> FindAllAreas()
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            return areaDao.FindAll();
        }

        public bool InsertArea(Area area)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            return areaDao.Insert(area);
        }

        public bool DeleteArea(Area area)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            return areaDao.Delete(area);
        }

        // Country
        public Country FindCountryByAbbreviation(string abbreviation)
        {
            ICountryDao countryDao = DalFactory.CreateCountryDao();

            return countryDao.FindByAbbreviation(abbreviation);
        }

        public IList<Country> FindAllCountries()
        {
            ICountryDao countryDao = DalFactory.CreateCountryDao();

            return countryDao.FindAll();
        }

        // ArtistPicture
        public ArtistPicture FindArtistPictureByURL(string url)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.FindByURL(url);
        }

        public IList<ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.FindAllByArtistId(artistId);
        }

        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.FindProfilePictureByArtistId(artistId);
        }

        public bool InsertArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.Insert(artistPicture);
        }

        public bool UpdateArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.Update(artistPicture);
        }

        public bool DeleteArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            return artistPictureDao.Delete(artistPicture);
        }

        // ArtistVideo
        public ArtistVideo FindArtistVideoByURL(string url)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.FindByURL(url);
        }

        public IList<ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.FindAllByArtistId(artistId);
        }

        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.FindPromoVideoByArtistId(artistId);
        }

        public bool InsertArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.Insert(artistVideo);
        }

        public bool UpdateArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.Update(artistVideo);
        }

        public bool DeleteArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            return artistVideoDao.Delete(artistVideo);
        }

        // PerformancePicture
        public PerformancePicture FindPerformancePictureByURL(string url)
        {
            IPerformancePictureDao PerformancePictureDao = DalFactory.CreatePerformancePictureDao();

            return PerformancePictureDao.FindByURL(url);
        }

        public IList<PerformancePicture> FindAllPicturesByPerformanceId(int performanceId)
        {
            IPerformancePictureDao PerformancePictureDao = DalFactory.CreatePerformancePictureDao();

            return PerformancePictureDao.FindAllByPerformanceId(performanceId);
        }

        public bool InsertPerformancePicture(PerformancePicture performancePicture)
        {
            IPerformancePictureDao PerformancePictureDao = DalFactory.CreatePerformancePictureDao();

            return PerformancePictureDao.Insert(performancePicture);
        }

        public bool DeletePerformancePicture(PerformancePicture performancePicture)
        {
            IPerformancePictureDao PerformancePictureDao = DalFactory.CreatePerformancePictureDao();

            return PerformancePictureDao.Delete(performancePicture);
        }

        // PerformanceVideo
        public PerformanceVideo FindPerformanceVideoByURL(string url)
        {
            IPerformanceVideoDao PerformanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            return PerformanceVideoDao.FindByURL(url);
        }

        public IList<PerformanceVideo> FindAllVideosByPerformanceId(int performanceId)
        {
            IPerformanceVideoDao PerformanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            return PerformanceVideoDao.FindAllByPerformanceId(performanceId);
        }

        public bool InsertPerformanceVideo(PerformanceVideo performanceVideo)
        {
            IPerformanceVideoDao PerformanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            return PerformanceVideoDao.Insert(performanceVideo);
        }

        public bool DeletePerformanceVideo(PerformanceVideo performanceVideo)
        {
            IPerformanceVideoDao PerformanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            return PerformanceVideoDao.Delete(performanceVideo);
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
