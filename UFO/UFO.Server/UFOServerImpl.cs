using System;
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

        public bool CheckLoginData(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtist(Artist artists)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerformance(Performance performance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artist> FindAllArtists()
        {
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            return artistDao.FindAll();
        }

        public Category FindCategoryById(int id)
        {
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao(database);

            return categoryDao.FindById(id);
        }

        public IEnumerable<Category> FindAllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Performance> FindAllPerformances()
        {
            throw new NotImplementedException();
        }

        public User findUserByName(string name)
        {
            IDatabase database = DalFactory.CreateDatabase();
            IUserDao userDao = DalFactory.CreateUserDao(database);

            return userDao.FindByUsername(name);
        }

        public IEnumerable<User> findAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venue> FindAllVenues()
        {
            throw new NotImplementedException();
        }

        public Artist FindArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public Performance FindPerformanceById(int id)
        {
            throw new NotImplementedException();
        }

        public Venue FindVenueById(int id)
        {
            throw new NotImplementedException();
        }

        public void InformAllArtists()
        {
            throw new NotImplementedException();
        }

        public bool InsertArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerformance(Performance performance)
        {
            throw new NotImplementedException();
        }

        public bool InsertVenue(Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePerformance(Performance performance)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVenue(Venue venue)
        {
            throw new NotImplementedException();
        }
    }
}
