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
    public class VideoDao : IVideoDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM Video
            WHERE Video.URL = @url";

        const string SQL_FIND_ALL =
            @"SELECT *
              FROM Video";

        const string SQL_INSERT =
          @"INSERT INTO Video
            VALUES (@url)";

        const string SQL_DELETE =
          @"DELETE FROM Video
            WHERE URL = @url";

        private IDatabase database;

        public VideoDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string videoURL)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.AnsiString, videoURL);
            return findByURLCommand;
        }

        public Video FindByURL(string videoURL)
        {
            using (DbCommand command = CreateFindByURLCommand(videoURL))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Video((string)reader["URL"]);
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

        public IList<Video> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Video> result = new List<Video>();
                while (reader.Read())
                    result.Add(new Video((string)reader["URL"]));
                return result;
            }
        }

        private DbCommand CreateInsertCommand(string url)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.AnsiString, url);
            return insertCommand;
        }

        public bool Insert(Video video)
        {
            using (DbCommand command = CreateInsertCommand(video.URL))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        private DbCommand CreateDeleteCommand(string url)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(deleteCommand, "url", DbType.AnsiString, url);
            return deleteCommand;
        }

        public bool Delete(Video video)
        {
            using (DbCommand command = CreateDeleteCommand(video.URL))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }
    }
}
