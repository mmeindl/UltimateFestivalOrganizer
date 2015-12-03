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
    public class PerformancePictureDao : IPerformancePictureDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM PerformancePicture
            WHERE PerformancePicture.pictureURL = @url";

        const string SQL_FIND_ALL_BY_PerformanceID =
          @"SELECT *
            FROM PerformancePicture
            WHERE PerformancePicture.PerformanceId = @id";

        const string SQL_INSERT =
          @"INSERT INTO PerformancePicture
            VALUES (@url, @performanceId)";

        const string SQL_DELETE =
          @"DELETE FROM PerformancePicture
            WHERE Id = @id";

        private IDatabase database;

        public PerformancePictureDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string url)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.String, url);
            return findByURLCommand;
        }

        public PerformancePicture FindByURL(string url)
        {
            using (DbCommand command = CreateFindByURLCommand(url))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new PerformancePicture(
                        (string)reader["PictureURL"],
                        (int)reader["PerformanceId"],
                        (int)reader["Id"]
                    );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindAllByPerformanceIdCommand(int performanceId)
        {
            DbCommand findAllByPerformanceIdCommand = database.CreateCommand(SQL_FIND_ALL_BY_PerformanceID);
            database.DefineParameter(findAllByPerformanceIdCommand, "id", DbType.Int32, performanceId);
            return findAllByPerformanceIdCommand;
        }

        public IList<PerformancePicture> FindAllByPerformanceId(int performanceId)
        {
            using (DbCommand command = CreateFindAllByPerformanceIdCommand(performanceId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<PerformancePicture> result = new List<PerformancePicture>();
                while (reader.Read())
                    result.Add(new PerformancePicture(
                        (string)reader["PictureURL"],
                        (int)reader["PerformanceId"],
                        (int)reader["Id"]));
                return result;
            }
        }

        private DbCommand CreateInsertCommand(string url, int performanceId)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.String, url);
            database.DefineParameter(insertCommand, "performanceId", DbType.Int32, performanceId);
            return insertCommand;
        }

        public bool Insert(PerformancePicture performancePicture)
        {
            bool result = true;

            IPictureDao pictureDao = DalFactory.CreatePictureDao(database);

            string url = performancePicture.PictureURL;
            Picture picture = pictureDao.FindByURL(url);
            if (picture == null)
            {
                picture = new Picture(url);
                result = pictureDao.Insert(picture);
            }


            using (DbCommand command = CreateInsertCommand(performancePicture.PictureURL, performancePicture.PerformanceId))
            {
                return database.ExecuteNonQuery(command) == 1 && result;
            }
        }

        private DbCommand CreateDeleteCommand(int id)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(deleteCommand, "id", DbType.Int32, id);
            return deleteCommand;
        }

        public bool Delete(PerformancePicture performancePicture)
        {
            bool result = true;
            string url = performancePicture.PictureURL;

            using (DbCommand command = CreateDeleteCommand(performancePicture.Id))
            {
                result = database.ExecuteNonQuery(command) == 1;
            }

            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao(database);

            ArtistPicture artistPicture = artistPictureDao.FindByURL(url);

            if (artistPicture == null)
            {
                IPictureDao pictureDao = DalFactory.CreatePictureDao(database);

                Picture picture = pictureDao.FindByURL(url);
                result = pictureDao.Delete(picture) & result;
            }

            return result;
        }
    }
}
