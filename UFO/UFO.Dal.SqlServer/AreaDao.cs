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
    public class AreaDao : IAreaDao
    {
        const string SQL_FIND_ALL =
          @"SELECT *
            FROM Area";

        const string SQL_FIND_BY_ID =
          @"SELECT *
            FROM Area
            WHERE Area.Id = @id";

        const string SQL_FIND_BY_NAME =
          @"SELECT *
            FROM Area
            WHERE Area.Name = @name";

        const string SQL_INSERT =
          @"INSERT INTO Area
            VALUES (@name)";

        const string SQL_DELETE =
          @"DELETE FROM Area
            WHERE Id = @id";

        private IDatabase database;

        public AreaDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindAllCommand()
        {
            return database.CreateCommand(SQL_FIND_ALL);
        }

        public IList<Area> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Area> result = new List<Area>();
                while (reader.Read())
                    result.Add(new Area(
                        (string)reader["Name"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }

        public Area FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Area(
                        (string)reader["Name"],
                        (int)reader["Id"]);
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

        public Area FindByName(string name)
        {
            using (DbCommand command = CreateFindByNameCommand(name))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Area(
                        (string)reader["Name"],
                        (int)reader["Id"]);
                }
                else
                {
                return null;
            }
        }
        }

        private DbCommand CreateInsertCommand(string name)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(insertCommand, "name", DbType.AnsiString, name);
            return insertCommand;
        }

        public bool Insert(Area area)
        {
            using (DbCommand command = CreateInsertCommand(area.Name))
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

        public bool Delete(Area area)
        {
            using (DbCommand command = CreateDeleteCommand(area.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }
    }
}
