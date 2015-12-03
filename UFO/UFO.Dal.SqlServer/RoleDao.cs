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
    class RoleDao : IRoleDao
    {
        const string SQL_FIND_BY_ID =
            @"SELECT *
                FROM Role
                WHERE Role.Id = @id";

        private IDatabase database;

        public RoleDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindBIdCommand(int id)
        {
            DbCommand findByURLCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByURLCommand, "id", DbType.Int32, id);
            return findByURLCommand;
        }

        public Role FindById(int id)
        {
            using (DbCommand command = CreateFindBIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Role(
                        (int)reader["Id"],
                        (string)reader["title"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
