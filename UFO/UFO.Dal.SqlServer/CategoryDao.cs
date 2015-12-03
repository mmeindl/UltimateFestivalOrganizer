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
    class CategoryDao : ICategoryDao
    {
        const string SQL_FIND_BY_ID =
            @"SELECT *
                FROM Category
                WHERE Category.id = @id";

        const string SQL_FIND_ALL =
            @"SELECT *
                FROM Category";

        private IDatabase database;

        public CategoryDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByIdCommand(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "id", DbType.Int32, id);
            return findByIdCommand;
        }

        public Category FindById(int id)
        {
            using (DbCommand command = CreateFindByIdCommand(id))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Category(
                        (string)reader["name"],
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

        public IList<Category> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Category> result = new List<Category>();
                while (reader.Read())
                    result.Add(new Category(
                        (string)reader["name"],
                        (int)reader["Id"])
                    );
                return result;
            }
        }

        
    }
}
