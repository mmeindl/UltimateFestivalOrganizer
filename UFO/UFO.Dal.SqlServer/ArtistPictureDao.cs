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
    public class ArtistPictureDao : IArtistPictureDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM ArtistPicture
            WHERE ArtistPicture.pictureURL = @url";

        const string SQL_FIND_PROFILEPICTURE_BY_ARTISTID =
          @"SELECT *
            FROM ArtistPicture
            WHERE ArtistPicture.artistId = @id
            AND ArtistPicture.isProfilePicture = 1";

        const string SQL_FIND_ALL_BY_ARTISTID =
          @"SELECT *
            FROM ArtistPicture
            WHERE ArtistPicture.artistId = @id";

        const string SQL_INSERT =
          @"INSERT INTO ArtistPicture
            VALUES (@url, @artistId, @isProfilePicture)";

        const string SQL_UPDATE =
          @"UPDATE ArtistPicture
            SET IsProfilePicture = @isProfilePicture
            WHERE Id = @id";

        const string SQL_UPDATE_PROFILEPICTURE_TO_FALSE =
          @"UPDATE ArtistPicture
            SET IsProfilePicture = 0
            WHERE IsProfilePicture = 1
            AND ArtistPicture.ArtistId = @artistId";

        const string SQL_DELETE =
          @"DELETE FROM ArtistPicture
            WHERE Id = @id";

        private IDatabase database;

        public ArtistPictureDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string url)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.AnsiString, url);
            return findByURLCommand;
        }

        public ArtistPicture FindByURL(string url)
        {
            using (DbCommand command = CreateFindByURLCommand(url))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new ArtistPicture(
                        (string)reader["PictureURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsProfilePicture"],
                        (int)reader["Id"]
                    );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindProfilPictureByArtistIdCommand(int artistId)
        {
            DbCommand findProfilPictureByArtistIdCommand = database.CreateCommand(SQL_FIND_PROFILEPICTURE_BY_ARTISTID);
            database.DefineParameter(findProfilPictureByArtistIdCommand, "id", DbType.Int32, artistId);
            return findProfilPictureByArtistIdCommand;
        }

        public ArtistPicture FindProfilePictureByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindProfilPictureByArtistIdCommand(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new ArtistPicture(
                        (string)reader["PictureURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsProfilePicture"],
                        (int)reader["Id"]
                    );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindAllByArtistIdCommand(int artistId)
        {
            DbCommand findAllByArtistIdCommand = database.CreateCommand(SQL_FIND_ALL_BY_ARTISTID);
            database.DefineParameter(findAllByArtistIdCommand, "id", DbType.Int32, artistId);
            return findAllByArtistIdCommand;
        }

        public IList<ArtistPicture> FindAllByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindAllByArtistIdCommand(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<ArtistPicture> result = new List<ArtistPicture>();
                while (reader.Read())
                    result.Add(new ArtistPicture(
                        (string)reader["PictureURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsProfilePicture"],
                        (int)reader["Id"]));
                return result;
            }
        }

        private DbCommand CreateUpdateProfilePictureToFalseCommand(int artistId)
        {
            DbCommand updateProfilePictureToFalseCommand = database.CreateCommand(SQL_UPDATE_PROFILEPICTURE_TO_FALSE);
            database.DefineParameter(updateProfilePictureToFalseCommand, "artistId", DbType.Int32, artistId);
            return updateProfilePictureToFalseCommand;
        }

        private DbCommand CreateInsertCommand(string url, int artistId, bool isProfilePicture)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.AnsiString, url);
            database.DefineParameter(insertCommand, "artistId", DbType.Int32, artistId);
            database.DefineParameter(insertCommand, "isProfilePicture", DbType.Boolean, isProfilePicture);
            return insertCommand;
        }

        public bool Insert(ArtistPicture artistPicture)
        {
            bool result = true;

            IPictureDao pictureDao = DalFactory.CreatePictureDao(database);

            string url = artistPicture.PictureURL;
            Picture picture = pictureDao.FindByURL(url);
            if (picture == null)
            {
                picture = new Picture(url);
                result = pictureDao.Insert(picture);
            }
           
            if (artistPicture.IsProfilePicture)
            {
                using (DbCommand command = CreateUpdateProfilePictureToFalseCommand(artistPicture.ArtistId))
                {
                    database.ExecuteNonQuery(command);
                }
            }

            using (DbCommand command = CreateInsertCommand(artistPicture.PictureURL, artistPicture.ArtistId,
                                                                        artistPicture.IsProfilePicture))
            {
                return database.ExecuteNonQuery(command) == 1 && result;
            }
        }

        private DbCommand CreateUpdateCommand(int id, bool isProfilePicture)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(updateCommand, "id", DbType.Int32, id);
            database.DefineParameter(updateCommand, "isProfilePicture", DbType.Boolean, isProfilePicture);
            return updateCommand;
        }

        public bool Update(ArtistPicture artistPicture)
        {
            if (artistPicture.IsProfilePicture)
            {
                using (DbCommand command = CreateUpdateProfilePictureToFalseCommand(artistPicture.ArtistId))
                {
                    database.ExecuteNonQuery(command);
                }
            }

            using (DbCommand command = CreateUpdateCommand(artistPicture.Id, artistPicture.IsProfilePicture))
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

        public bool Delete(ArtistPicture artistPicture)
        {
            bool result = true;
            string url = artistPicture.PictureURL;

            using (DbCommand command = CreateDeleteCommand(artistPicture.Id))
            {
                result = database.ExecuteNonQuery(command) == 1;
            }

            IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao(database);

            PerformancePicture performancePicture = performancePictureDao.FindByURL(url);

            if (performancePicture == null)
            {
                IPictureDao pictureDao = DalFactory.CreatePictureDao(database);

                Picture picture = pictureDao.FindByURL(url);
                result = pictureDao.Delete(picture) & result;
            }

            return result;
        }
    }
}
