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
    public class PictureDao : IPictureDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM Picture
            WHERE Picture.URL = @url";

        const string SQL_FIND_ALL =
            @"SELECT *
              FROM Picture";

        const string SQL_INSERT =
          @"INSERT INTO Picture
            VALUES (@url)";

        const string SQL_DELETE =
          @"DELETE FROM Picture
            WHERE URL = @url";

        private IDatabase database;

        public PictureDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string pictureURL)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.AnsiString, pictureURL);
            return findByURLCommand;
        }

        public Picture FindByURL(string pictureURL)
        {
            using (DbCommand command = CreateFindByURLCommand(pictureURL))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Picture((string)reader["URL"]);
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

        public IList<Picture> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Picture> result = new List<Picture>();
                while (reader.Read())
                    result.Add(new Picture((string)reader["URL"]));
                return result;
            }
        }

        private DbCommand CreateInsertCommand(string url)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.AnsiString, url);
            return insertCommand;
        }

        public bool Insert(Picture picture)
        {
            using (DbCommand command = CreateInsertCommand(picture.URL))
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

        public bool Delete(Picture picture)
        {
            using (DbCommand command = CreateDeleteCommand(picture.URL))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }
    }
}
