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
    class UserDao : IUserDao
    {
        const string SQL_FIND_BY_ID =
            @"SELECT *
                FROM [User]
                WHERE [User].[Id] = @id";

        const string SQL_FIND_BY_USERNAME =
            @"SELECT *
                FROM [User]
                WHERE [User].Username = @username";

        const string SQL_FIND_ALL =
            @"SELECT *
                FROM [User]";

        private IDatabase database;

        public UserDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }


        public User FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new User(
                        (string)reader["Username"],
                        (string)reader["Password"],
                        (string)reader["Email"],
                        (int)reader["RoleId"],
                        (int)reader["Id"]
                        );
                }
                else
                {
                    return null;
                }
            }
        }

        private DbCommand CreateFindByUserameCommand(string username)
        {
            DbCommand findByUserameCommand = database.CreateCommand(SQL_FIND_BY_USERNAME);
            database.DefineParameter(findByUserameCommand, "username", DbType.String, username);
            return findByUserameCommand;
        }

        public User FindByUsername(string username)
        {
            using (DbCommand command = CreateFindByUserameCommand(username))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new User(
                        (string)reader["Username"],
                        (string)reader["Password"],
                        (string)reader["Email"],
                        (int)reader["RoleId"],
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
            DbCommand findAllCommand = database.CreateCommand(SQL_FIND_ALL);
            return findAllCommand;
        }

        public IList<User> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<User> result = new List<User>();
                while (reader.Read())
                    result.Add(new User(
                        (string)reader["Username"],
                        (string)reader["Password"],
                        (string)reader["Email"],
                        (int)reader["RoleId"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }
    }
}
