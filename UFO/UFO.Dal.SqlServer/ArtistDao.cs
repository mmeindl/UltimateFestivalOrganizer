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
    public class ArtistDao : IArtistDao
    {
        const string SQL_FIND_BY_ID =
          @"SELECT *
            FROM Artist
            WHERE Artist.id = @id";

        const string SQL_FIND_BY_NAME =
          @"SELECT *
            FROM Artist
            WHERE Artist.name = @name";

        const string SQL_FIND_ALL =
          @"SELECT *
            FROM Artist
            WHERE Artist.isDeleted = 0";

        const string SQL_INSERT =
          @"INSERT INTO Artist
            VALUES (@name, @abbreviation, @email, @categoryId, @websiteURL, @isDeleted)";

        const string SQL_UPDATE =
          @"UPDATE Artist 
            SET Name = @name, CountryId = @abbreviation, Email = @email,
                CategoryId = @categoryId, WebsiteURL = @websiteURL, IsDeleted = @isDeleted
            WHERE Id = @id";

        const string SQL_DELETE =
          @"DELETE FROM Artist
            WHERE Id = @id";

        private IDatabase database;

        public ArtistDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }

        public Artist FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Artist(
                        (string)reader["CountryId"],
                        (int)reader["CategoryId"],
                        (string)reader["Name"],
                        (string)reader["Email"],
                        (string)reader["WebsiteURL"],
                        (bool)reader["IsDeleted"],
                        (int)reader["Id"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindByNameCommand(string name)
        {
            DbCommand findByNameCommand = database.CreateCommand(SQL_FIND_BY_NAME);
            database.DefineParameter(findByNameCommand, "name", DbType.AnsiString, name);
            return findByNameCommand;
        }

        public Artist FindByName(string name)
        {
            using (DbCommand command = CreateFindByNameCommand(name))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Artist(
                        (string)reader["CountryId"],
                        (int)reader["CategoryId"],
                        (string)reader["Name"],
                        (string)reader["Email"],
                        (string)reader["WebsiteURL"],
                        (bool)reader["IsDeleted"],
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

        public IList<Artist> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Artist> result = new List<Artist>();
                while (reader.Read())
                    result.Add(new Artist(
                        (string)reader["CountryId"],
                        (int)reader["CategoryId"],
                        (string)reader["Name"],
                        (string)reader["Email"],
                        (string)reader["WebsiteURL"],
                        (bool)reader["IsDeleted"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateInsertCommand(string name, string abbreviation, string email,
                                              int categoryId, string websiteURL, bool isDeleted)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "name", DbType.AnsiString, name);
            database.DefineParameter(insertCommand, "abbreviation", DbType.AnsiString, abbreviation);
            database.DefineParameter(insertCommand, "email", DbType.AnsiString, email);
            database.DefineParameter(insertCommand, "categoryId", DbType.Int32, categoryId);
            database.DefineParameter(insertCommand, "websiteURL", DbType.AnsiString, websiteURL);
            database.DefineParameter(insertCommand, "isDeleted", DbType.Byte, isDeleted);
            return insertCommand;
        }

        public bool Insert(Artist artist)
        {
            using (DbCommand command = CreateInsertCommand(artist.Name, artist.CountryId, artist.Email,
                                                           artist.CategoryId, artist.WebsiteURL, artist.IsDeleted))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        private DbCommand CreateUpdateCommand(int id, string abbreviation, int categoryId, string name,
                                              string email, string websiteURL, bool isDeleted)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(updateCommand, "id", DbType.Int32, id);
            database.DefineParameter(updateCommand, "abbreviation", DbType.AnsiString, abbreviation);
            database.DefineParameter(updateCommand, "categoryId", DbType.Int32, categoryId);
            database.DefineParameter(updateCommand, "name", DbType.AnsiString, name);
            database.DefineParameter(updateCommand, "email", DbType.AnsiString, email);
            database.DefineParameter(updateCommand, "websiteURL", DbType.AnsiString, websiteURL);
            database.DefineParameter(updateCommand, "isDeleted", DbType.Byte, isDeleted);
            return updateCommand;
        }

        public bool Update(Artist artist)
        {
            using (DbCommand command = CreateUpdateCommand(artist.Id, artist.CountryId, artist.CategoryId,
                                                           artist.Name, artist.Email, artist.WebsiteURL, artist.IsDeleted))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public bool Delete(Artist artist)
        {
            IDatabase database = DalFactory.CreateDatabase();
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao(database);

            artist.IsDeleted = true;

            bool result = Update(artist);

            if (result)
            {
                return performanceDao.DeleteAllInFutureByArtistId(artist.Id);
            }

            return result;
        }
    }
}
