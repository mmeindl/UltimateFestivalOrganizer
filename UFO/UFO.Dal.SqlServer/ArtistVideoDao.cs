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
    public class ArtistVideoDao : IArtistVideoDao
    {
        const string SQL_FIND_BY_URL =
          @"SELECT *
            FROM ArtistVideo
            WHERE ArtistVideo.videoURL = @url";

        const string SQL_FIND_PROMOVIDEO_BY_ARTISTID =
          @"SELECT *
            FROM ArtistVideo
            WHERE ArtistVideo.artistId = @id
            AND ArtistVideo.isPromoVideo = 1";

        const string SQL_FIND_ALL_BY_ARTISTID =
          @"SELECT *
            FROM ArtistVideo
            WHERE ArtistVideo.artistId = @id";

        const string SQL_INSERT =
          @"INSERT INTO ArtistVideo
            VALUES (@artistId, @url, @isPromoVideo)";

        const string SQL_UPDATE =
          @"UPDATE ArtistVideo
            SET IsPromoVideo = @isPromoVideo
            WHERE Id = @id";

        const string SQL_UPDATE_PROMOVIDEO_TO_FALSE =
          @"UPDATE ArtistVideo
            SET IsPromoVideo = 0
            WHERE IsPromoVideo = 1
            AND ArtistVideo.ArtistId = @artistId";

        const string SQL_DELETE =
          @"DELETE FROM ArtistVideo
            WHERE Id = @id";

        private IDatabase database;

        public ArtistVideoDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByURLCommand(string url)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_URL);
            database.DefineParameter(findByURLCommand, "url", DbType.String, url);
            return findByURLCommand;
        }

        public ArtistVideo FindByURL(string url)
        {
            using (DbCommand command = CreateFindByURLCommand(url))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new ArtistVideo(
                        (string)reader["VideoURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsPromoVideo"],
                        (int)reader["Id"]
                    );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindPromoVideoByArtistIdCommand(int artistId)
        {
            DbCommand findPromoVideoByArtistIdCommand = database.CreateCommand(SQL_FIND_PROMOVIDEO_BY_ARTISTID);
            database.DefineParameter(findPromoVideoByArtistIdCommand, "id", DbType.Int32, artistId);
            return findPromoVideoByArtistIdCommand;
        }

        public ArtistVideo FindPromoVideoByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindPromoVideoByArtistIdCommand(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new ArtistVideo(
                        (string)reader["VideoURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsPromoVideo"],
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

        public IList<ArtistVideo> FindAllByArtistId(int artistId)
        {
            using (DbCommand command = CreateFindAllByArtistIdCommand(artistId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<ArtistVideo> result = new List<ArtistVideo>();
                while (reader.Read())
                    result.Add(new ArtistVideo(
                        (string)reader["VideoURL"],
                        (int)reader["ArtistId"],
                        (bool)reader["IsPromoVideo"],
                        (int)reader["Id"]));
                return result;
            }
        }

        private DbCommand CreateUpdatePromoVideoToFalseCommand(int artistId)
        {
            DbCommand updatePromoVideoToFalseCommand = database.CreateCommand(SQL_UPDATE_PROMOVIDEO_TO_FALSE);
            database.DefineParameter(updatePromoVideoToFalseCommand, "artistId", DbType.Int32, artistId);
            return updatePromoVideoToFalseCommand;
        }

        private DbCommand CreateInsertCommand(string url, int artistId, bool isPromoVideo)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "url", DbType.String, url);
            database.DefineParameter(insertCommand, "artistId", DbType.Int32, artistId);
            database.DefineParameter(insertCommand, "isPromoVideo", DbType.Boolean, isPromoVideo);
            return insertCommand;
        }

        public bool Insert(ArtistVideo artistVideo)
        {
            bool result = true;

            IVideoDao videoDao = DalFactory.CreateVideoDao(database);

            string url = artistVideo.VideoURL;
            Video video = videoDao.FindByURL(url);
            if (video == null)
            {
                video = new Video(url);
                result = videoDao.Insert(video);
            }

            if (artistVideo.IsPromoVideo)
            {
                using (DbCommand command = CreateUpdatePromoVideoToFalseCommand(artistVideo.ArtistId))
                {
                    database.ExecuteNonQuery(command);
                }
            }


            using (DbCommand command = CreateInsertCommand(artistVideo.VideoURL, artistVideo.ArtistId,
                                                                        artistVideo.IsPromoVideo))
            {
                return database.ExecuteNonQuery(command) == 1 && result;
            }
        }

        private DbCommand CreateUpdateCommand(int id, bool isPromoVideo)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(updateCommand, "id", DbType.Int32, id);
            database.DefineParameter(updateCommand, "isPromoVideo", DbType.Boolean, isPromoVideo);
            return updateCommand;
        }

        public bool Update(ArtistVideo artistVideo)
        {
            if (artistVideo.IsPromoVideo)
            {
                using (DbCommand command = CreateUpdatePromoVideoToFalseCommand(artistVideo.ArtistId))
                {
                    database.ExecuteNonQuery(command);
                }
            }

            using (DbCommand command = CreateUpdateCommand(artistVideo.Id, artistVideo.IsPromoVideo))
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

        public bool Delete(ArtistVideo artistVideo)
        {
            bool result = true;
            string url = artistVideo.VideoURL;

            using (DbCommand command = CreateDeleteCommand(artistVideo.Id))
            {
                result = database.ExecuteNonQuery(command) == 1;
            }

            IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao(database);

            PerformanceVideo performanceVideo = performanceVideoDao.FindByURL(url);

            if (performanceVideo == null)
            {
                IVideoDao videoDao = DalFactory.CreateVideoDao(database);

                Video video = videoDao.FindByURL(url);
                result = videoDao.Delete(video) & result;
            }

            return result;
        }
    }
}
