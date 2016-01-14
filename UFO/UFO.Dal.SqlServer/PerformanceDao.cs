using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Dal.Common;
using UFO.Domain;

namespace UFO.Dal.SqlServer
{
    class PerformanceDao : IPerformanceDao
    {
        const string SQL_FIND_BY_ID =
            @"SELECT *
                FROM Performance
                WHERE Performance.id = @id";

        const string SQL_FIND_BY_DATETIME_AND_ARTISTID =
            @"SELECT *
                FROM Performance
                WHERE Performance.DateTime = @dateTime
                AND Performance.ArtistId = @artistId";

        const string SQL_FIND_ALL =
            @"SELECT *
                FROM Performance";

        const string SQL_FIND_ALL_BY_DATE =
            @"SELECT *
                FROM Performance
                WHERE DATEDIFF(DAY ,dateTime, @date) = 0";

        const string SQL_FIND_ALL_BY_DATE_AND_VENUE =
            @"SELECT *
                FROM Performance
                WHERE DATEDIFF(DAY ,dateTime, @date) = 0
                AND Performance.VenueId = @venueId";

        const string SQL_FIND_ALL_BY_DATE_AND_ARTIST =
            @"SELECT *
                FROM Performance
                WHERE DATEDIFF(DAY ,dateTime, @date) = 0
                AND Performance.ArtistId = @artistId";

        const string SQL_FIND_ALL_IN_FUTURE =
            @"SELECT *
                FROM Performance
                WHERE DATEDIFF(MI ,GETDATE(), dateTime) > 0";

        const string SQL_FIND_ALL_BY_ARTISTID =
            @"SELECT *
                FROM Performance
                WHERE Performance.ArtistId = @artistId";

        const string SQL_FIND_ALL_BY_VENUEID =
            @"SELECT *
                FROM Performance
                WHERE Performance.VenueId = @venueId";

        const string SQL_FIND_ALL_IN_FUTURE_BY_ARTIST_ID =
            @"SELECT *
                FROM Performance
                WHERE DATEDIFF(MI ,GETDATE(), dateTime) > 0
                AND Performance.ArtistId = @id";

        const string SQL_GET_PERFORMANCE_DATES =
            @"SELECT CONVERT(DATE, Performance.DateTime) AS DateTime
                FROM Performance
                GROUP BY CONVERT(DATE, Performance.DateTime)";

        const string SQL_INSERT =
            @"INSERT INTO Performance
                VALUES (@dateTime, @venueId, @artistId)";

        const string SQL_UPDATE =
            @"UPDATE Performance 
                SET DateTime = @dateTime, VenueId = @venueId, ArtistId = @artistId
                WHERE Id = @id";

        const string SQL_DELETE =
            @"DELETE FROM Performance
                WHERE Id = @id";


        private IDatabase database;

        public PerformanceDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }

        public Performance FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindByDateTimeAndArtistIdCommand(DateTime dateTime, int artistId)
        {
            DbCommand findByDateTimeAndArtistIdCommand = database.CreateCommand(SQL_FIND_BY_DATETIME_AND_ARTISTID);
            database.DefineParameter(findByDateTimeAndArtistIdCommand, "artistId", DbType.Int32, artistId);
            database.DefineParameter(findByDateTimeAndArtistIdCommand, "dateTime", DbType.Int32, dateTime);
            return findByDateTimeAndArtistIdCommand;
        }

        public Performance FindByDateTimeAndArtistId(DateTime dateTime, int artistId)
        {
            using (DbCommand command = CreateFindByDateTimeAndArtistIdCommand(dateTime, artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindAllCommand()
        {
            return database.CreateCommand(SQL_FIND_ALL);
        }

        public IList<Performance> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllByDate(DateTime date)
        {
            DbCommand findAllByDate = database.CreateCommand(SQL_FIND_ALL_BY_DATE);
            database.DefineParameter(findAllByDate, "date", DbType.DateTime, date);
            return findAllByDate;
        }

        public IList<Performance> FindAllByDate(DateTime date)
        {
            using (DbCommand command = CreateFindAllByDate(date))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllByDateAndVenue(DateTime date, int venueId)
        {
            DbCommand findAllByDateAndVenue = database.CreateCommand(SQL_FIND_ALL_BY_DATE_AND_VENUE);
            database.DefineParameter(findAllByDateAndVenue, "date", DbType.DateTime, date);
            database.DefineParameter(findAllByDateAndVenue, "venueId", DbType.Int32, venueId);
            return findAllByDateAndVenue;
        }

        public IList<Performance> FindByDateAndVenue(DateTime date, Venue venue)
        {
            using (DbCommand command = CreateFindAllByDateAndVenue(date, venue.Id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllByDateAndArtist(DateTime date, int artistId)
        {
            DbCommand findAllByDateAndArtist = database.CreateCommand(SQL_FIND_ALL_BY_DATE_AND_ARTIST);
            database.DefineParameter(findAllByDateAndArtist, "date", DbType.DateTime, date);
            database.DefineParameter(findAllByDateAndArtist, "artistId", DbType.Int32, artistId);
            return findAllByDateAndArtist;
        }

        public IList<Performance> FindByDateAndArtist(DateTime date, Artist artist)
        {
            using (DbCommand command = CreateFindAllByDateAndArtist(date, artist.Id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllInFuture()
        {
            return database.CreateCommand(SQL_FIND_ALL_IN_FUTURE);
        }

        public IList<Performance> FindAllInFuture()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }

        }

        private DbCommand CreateFindAllByArtistId(int artistId)
        {
            DbCommand findAllByArtistId = database.CreateCommand(SQL_FIND_ALL_BY_ARTISTID);
            database.DefineParameter(findAllByArtistId, "artistId", DbType.Int32, artistId);
            return findAllByArtistId;
        }

        public IList<Performance> FindAllByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindAllByArtistId(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllByVenueId(int venueId)
        {
            DbCommand findAllByVenueId = database.CreateCommand(SQL_FIND_ALL_BY_VENUEID);
            database.DefineParameter(findAllByVenueId, "venueId", DbType.Int32, venueId);
            return findAllByVenueId;
        }

        public IList<Performance> FindAllByVenueId(int venueId)
        {
            using (DbCommand command = CreateFindAllByVenueId(venueId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();
                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllInFutureByArtistIdCommand(int artistId)
        {
            DbCommand findAllInFutureByArtistIdCommand = database.CreateCommand(SQL_FIND_ALL_IN_FUTURE_BY_ARTIST_ID);
            database.DefineParameter(findAllInFutureByArtistIdCommand, "id", DbType.Int32, artistId);

            return findAllInFutureByArtistIdCommand;
        }

        public IList<Performance> FindAllInFutureByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindAllInFutureByArtistIdCommand(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Performance> result = new List<Performance>();

                while (reader.Read())
                    result.Add(new Performance(
                        (DateTime)reader["dateTime"],
                        (int)reader["venueId"],
                        (int)reader["artistId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand GetPerformanceDatesCommand()
        {
            return database.CreateCommand(SQL_GET_PERFORMANCE_DATES);
        }

        public IList<DateTime> GetPerformanceDates()
        {
            using (DbCommand command = GetPerformanceDatesCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<DateTime> result = new List<DateTime>();
                while (reader.Read())
                    result.Add((DateTime)reader["dateTime"]
                    );
                return result;
            }
        }

        private DbCommand CreateInsertCommand(DateTime dateTime, int venueId, int artistId)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "dateTime", DbType.DateTime, dateTime);
            database.DefineParameter(insertCommand, "venueId", DbType.Int32, venueId);
            database.DefineParameter(insertCommand, "artistId", DbType.Int32, artistId);
            return insertCommand;
        }

        public bool Insert(Performance performance)
        {
            if (!ValidatePerformance(performance))
            {
                return false;
            }

            using (DbCommand command = CreateInsertCommand(performance.DateTime, performance.VenueId, performance.ArtistId))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        private DbCommand CreateUpdateCommand(int id, DateTime dateTime, int venueId, int artistId)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(updateCommand, "id", DbType.Int32, id);
            database.DefineParameter(updateCommand, "dateTime", DbType.DateTime, dateTime);
            database.DefineParameter(updateCommand, "venueId", DbType.Int32, venueId);
            database.DefineParameter(updateCommand, "artistId", DbType.Int32, artistId);

            return updateCommand;
        }

        public bool Update(Performance performance)
        {
            if (!ValidatePerformance(performance))
            {
                return false;
            }

            using (DbCommand command = CreateUpdateCommand(performance.Id, performance.DateTime, performance.VenueId, performance.ArtistId))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        private DbCommand CreateDeleteCommand(int id)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(deleteCommand, "id", DbType.Int32, id);

            return deleteCommand;
        }

        public bool Delete(Performance performance)
        {
            IDatabase database = DalFactory.CreateDatabase();
            IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();
            IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            bool result = true;

            IList<PerformancePicture>  performancePictures = performancePictureDao.FindAllByPerformanceId(performance.Id);

            foreach (PerformancePicture pp in performancePictures)
            {
                result = performancePictureDao.Delete(pp);
            }

            IList<PerformanceVideo> performanceVideos = performanceVideoDao.FindAllByPerformanceId(performance.Id);

            foreach (PerformanceVideo pv in performanceVideos)
            {
                result = performanceVideoDao.Delete(pv) && result;
            }

            using (DbCommand command = CreateDeleteCommand(performance.Id))
            {
                result = database.ExecuteNonQuery(command) == 1;
            }

            return result;
        }

        

        public bool DeleteAllInFutureByArtistId(int artistId)
        {
            IDatabase database = DalFactory.CreateDatabase();
            IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();
            IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            bool result = true;

            IList<Performance> performances = FindAllInFutureByArtistId(artistId);

            foreach (Performance p in performances)
            {
                IList<PerformancePicture> performancePictures = performancePictureDao.FindAllByPerformanceId(p.Id);

                foreach (PerformancePicture pp in performancePictures)
                {
                    result = performancePictureDao.Delete(pp);
                }

                IList<PerformanceVideo> performanceVideos = performanceVideoDao.FindAllByPerformanceId(p.Id);

                foreach (PerformanceVideo pv in performanceVideos)
                {
                    result = performanceVideoDao.Delete(pv) && result;
                }

                result = Delete(p) && result;
            }

            return result;
        }




        /* ----- VALIDATION ----- */

        private bool ValidatePerformance(Performance performance)
        {
            DateTime dt = performance.DateTime;

            IList<Performance> performances = FindAllByVenueId(performance.VenueId);

            foreach (Performance p in performances)
            {
                if (p.DateTime == dt)
                {
                    return false;
                }
            }

            performances = FindAllByArtistId(performance.ArtistId);

            foreach (Performance p in performances)
            {
                if (p.DateTime <= dt.AddHours(1) && p.DateTime >= dt.AddHours(-1) &&
                    p.Id != performance.Id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
