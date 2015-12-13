﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Dal.Common;
using UFO.Domain;

namespace UFO.Server
{
    internal class UFOServerImpl : IUFOServer
    {
        private IDatabase database;

        public UFOServerImpl()
        {
            database = DalFactory.CreateDatabase();
        }

        // Category
        public Category FindCategoryById(int id)
        {
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao(database);

            return categoryDao.FindById(id);
        }

        public IEnumerable<Category> FindAllCategories()
        {
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao(database);

            return categoryDao.FindAll();
        }

        // User
        public User findUserByName(string name)
        {
            IUserDao userDao = DalFactory.CreateUserDao(database);

            return userDao.FindByUsername(name);
        }

        public IEnumerable<User> findAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool CheckLoginData(string username, string password)
        {
            throw new NotImplementedException();
        }

        // Artist
        public IEnumerable<Artist> FindAllArtists()
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.FindAll();
        }

        public Artist FindArtistByName(string name)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.FindByName(name);
        }

        public Artist FindArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public void InformAllArtists()
        {
            throw new NotImplementedException();
        }

        public bool InsertArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.Insert(artist);
        }

        public bool UpdateArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.Update(artist);
        }

        public bool DeleteArtist(Artist artist)
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.Delete(artist);
        }

        // Performance
        public IEnumerable<Performance> FindAllPerformances()
        {
            throw new NotImplementedException();
        }

        public Performance FindPerformanceById(int id)
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

        // Venue
        public IEnumerable<Venue> FindAllVenues()
        {
            throw new NotImplementedException();
        }

        public Venue FindVenueById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venue> FindVenuesByAreaId(int areaId)
        {
            IVenueDao venueDao = DalFactory.CreateVenueDao(database);

            return venueDao.FindByAreaId(areaId);
        }

        public bool InsertVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        // Area
        public Area FindAreaById(int id)
        {
            throw new NotImplementedException();
        }

        public Area FindAreaByName(string name)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao(database);

            return areaDao.FindByName(name);
        }

        public IList<Area> FindAllAreas()
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao(database);

            return areaDao.FindAll();
        }

        public bool InsertArea(Area area)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao(database);

            return areaDao.Insert(area);
        }

        public bool UpdateArea(Area area)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArea(Area area)
        {
            IAreaDao areaDao = DalFactory.CreateAreaDao(database);

            return areaDao.Delete(area);
        }

        // Country
        public Country FindCountryByAbbreviation(string abbreviation)
        {
            ICountryDao countryDao = DalFactory.CreateCountryDao(database);

            return countryDao.FindByAbbreviation(abbreviation);
        }

        public IList<Country> FindAllCountries()
        {
            ICountryDao countryDao = DalFactory.CreateCountryDao(database);

            return countryDao.FindAll();
        }

        // ArtistPicture
        public ArtistPicture FindArtistPictureByURL(string url)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.FindByURL(url);
        }

        public IList<ArtistPicture> FindAllPicturesByArtistId(int artistId)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.FindAllByArtistId(artistId);
        }

        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.FindProfilePictureByArtistId(artistId);
        }

        public bool InsertArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.Insert(artistPicture);
        }

        public bool UpdateArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.Update(artistPicture);
        }

        public bool DeleteArtistPicture(ArtistPicture artistPicture)
        {
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            return artistPictureDao.Delete(artistPicture);
        }

        // ArtistVideo
        public ArtistVideo FindArtistVideoByURL(string url)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.FindByURL(url);
        }

        public IList<ArtistVideo> FindAllVideosByArtistId(int artistId)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.FindAllByArtistId(artistId);
        }

        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.FindPromoVideoByArtistId(artistId);
        }

        public bool InsertArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.Insert(artistVideo);
        }

        public bool UpdateArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.Update(artistVideo);
        }

        public bool DeleteArtistVideo(ArtistVideo artistVideo)
        {
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            return artistVideoDao.Delete(artistVideo);
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
