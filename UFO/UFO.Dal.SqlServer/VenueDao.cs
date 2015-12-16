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
    class VenueDao : IVenueDao
    {

        const string SQL_FIND_BY_ID =
            @"SELECT *
                FROM Venue
                WHERE Venue.id = @id";

        const string SQL_FIND_BY_NAME =
            @"SELECT *
                FROM Venue
                WHERE Venue.name = @name";

        const string SQL_FIND_BY_AREAID =
            @"SELECT *
                FROM Venue
                WHERE Venue.areaId = @areaId";

        const string SQL_FIND_ALL =
            @"SELECT *
                FROM Venue";

        const string SQL_INSERT =
            @"INSERT INTO Venue
                VALUES (@name, @shortName, @geoLocationLat, @geoLocationLon, @areaId)";

        const string SQL_UPDATE =
            @"UPDATE Venue
            SET Name = @name, ShortName = @shortName, GeoLocationLat = @GeoLocationLat, GeoLocationLon = @geoLocationLon, AreaId = @areaId
                WHERE Venue.Id = @id";

        const string SQL_DELETE =
            @"DELETE FROM Venue
                WHERE Venue.Id = @id";


        private IDatabase database;

        public VenueDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }

        public Venue FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Venue(
                        (int)reader["areaId"],
                        (string)reader["name"],
                        (string)reader["shortName"],
                        (decimal)reader["geoLocationLat"],
                        (decimal)reader["geoLocationLon"],
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
            database.DefineParameter(findByNameCommand, "name", DbType.Int32, name);
            return findByNameCommand;
        }

        public Venue FindByName(string name)
        {
            using (DbCommand command = CreateFindByNameCommand(name))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Venue(
                        (int)reader["areaId"],
                        (string)reader["name"],
                        (string)reader["shortName"],
                        (decimal)reader["geoLocationLat"],
                        (decimal)reader["geoLocationLon"],
                        (int)reader["Id"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindByAreaIdCommand(int areaId)
        {
            DbCommand findByAreaIdCommand = database.CreateCommand(SQL_FIND_BY_AREAID);
            database.DefineParameter(findByAreaIdCommand, "areaId", DbType.Int32, areaId);
            return findByAreaIdCommand;
        }

        public IList<Venue> FindByAreaId(int areaId)
        {
            using (DbCommand command = CreateFindByAreaIdCommand(areaId))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Venue> result = new List<Venue>();
                while (reader.Read())
                    result.Add(new Venue(
                        (int)reader["areaId"],
                        (string)reader["name"],
                        (string)reader["shortName"],
                        (decimal)reader["geoLocationLat"],
                        (decimal)reader["geoLocationLon"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindAllCommand()
        {
            return database.CreateCommand(SQL_FIND_ALL);
        }

        public IList<Venue> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Venue> result = new List<Venue>();
                while (reader.Read())
                    result.Add(new Venue(
                        (int)reader["areaId"],
                        (string)reader["name"],
                        (string)reader["shortName"],
                        (decimal)reader["geoLocationLat"],
                        (decimal)reader["geoLocationLon"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateInsertCommand(int areaId, string name, string shortName, decimal geoLocationLat, decimal geoLocationLon)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "name", DbType.String, name);
            database.DefineParameter(insertCommand, "areaId", DbType.String, areaId);
            database.DefineParameter(insertCommand, "shortName", DbType.String, shortName);
            database.DefineParameter(insertCommand, "geoLocationLat", DbType.Double, geoLocationLat);
            database.DefineParameter(insertCommand, "geoLocationLon", DbType.Double, geoLocationLon);
            return insertCommand;
        }

        public bool Insert(Venue venue)
        {
            using (DbCommand command = CreateInsertCommand(venue.AreaId, venue.Name, venue.ShortName, venue.GeoLocationLat, venue.GeoLocationLon))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        private DbCommand CreateUpdateCommand(int id, int areaId, string name, string shortName, decimal geoLocationLat, decimal geoLocationLon)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(updateCommand, "id", DbType.Int32, id);
            database.DefineParameter(updateCommand, "name", DbType.String, name);
            database.DefineParameter(updateCommand, "areaId", DbType.String, areaId);
            database.DefineParameter(updateCommand, "shortName", DbType.String, shortName);
            database.DefineParameter(updateCommand, "geoLocationLat", DbType.Double, geoLocationLat);
            database.DefineParameter(updateCommand, "geoLocationLon", DbType.Double, geoLocationLon);
            return updateCommand;
        }

        public bool Update(Venue venue)
        {
            using (DbCommand command = CreateUpdateCommand(venue.Id, venue.AreaId, venue.Name, venue.ShortName, venue.GeoLocationLat, venue.GeoLocationLon))
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

        public bool Delete(Venue venue)
        {
            using (DbCommand command = CreateDeleteCommand(venue.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }
    }
}
