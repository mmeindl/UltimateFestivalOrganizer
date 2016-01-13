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
    class PerformanceVideoDao : IPerformanceVideoDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM PerformanceVideo
            WHERE PerformanceVideo.VideoURL = @url";

        const string SQL_FIND_ALL_BY_PerformanceID =
          @"SELECT *
            FROM PerformanceVideo
            WHERE PerformanceVideo.PerformanceId = @id";

        const string SQL_INSERT =
          @"INSERT INTO PerformanceVideo
            VALUES (@performanceId, @url)";

        const string SQL_DELETE =
          @"DELETE FROM PerformanceVideo
            WHERE Id = @id";

        private IDatabase database;

        public PerformanceVideoDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string url)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.AnsiString, url);
            return findByURLCommand;
        }

        public PerformanceVideo FindByURL(string url)
        {
            using (DbCommand command = CreateFindByURLCommand(url))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new PerformanceVideo(
                        (string)reader["VideoURL"],
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

        public IList<PerformanceVideo> FindAllByPerformanceId(int performanceId)
        {
            using (DbCommand command = CreateFindAllByPerformanceIdCommand(performanceId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<PerformanceVideo> result = new List<PerformanceVideo>();
                while (reader.Read())
                    result.Add(new PerformanceVideo(
                        (string)reader["VideoURL"],
                        (int)reader["PerformanceId"],
                        (int)reader["Id"]));
                return result;
            }
        }

        private DbCommand CreateInsertCommand(string url, int performanceId)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.AnsiString, url);
            database.DefineParameter(insertCommand, "performanceId", DbType.Int32, performanceId);
            return insertCommand;
        }

        public bool Insert(PerformanceVideo performanceVideo)
        {
            bool result = true;

            IVideoDao videoDao = DalFactory.CreateVideoDao(database);

            string url = performanceVideo.VideoURL;
            Video video = videoDao.FindByURL(url);
            if (video == null)
            {
                video = new Video(url);
                result = videoDao.Insert(video);
            }


            using (DbCommand command = CreateInsertCommand(performanceVideo.VideoURL, performanceVideo.PerformanceId))
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

        public bool Delete(PerformanceVideo performanceVideo)
        {
            bool result = true;
            string url = performanceVideo.VideoURL;

            using (DbCommand command = CreateDeleteCommand(performanceVideo.Id))
            {
                result = database.ExecuteNonQuery(command) == 1;
            }

            IVideoDao VideoDao = DalFactory.CreateVideoDao(database);
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao(database);

            ArtistVideo artistVideo = artistVideoDao.FindByURL(url);

            if (artistVideo == null)
            {
                Video Video = VideoDao.FindByURL(url);
                result = VideoDao.Delete(Video) & result;
            }

            return result;
        }
    }
}
