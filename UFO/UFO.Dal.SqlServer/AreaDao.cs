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
                        (string)reader["Name"])
                    );
                return result;
            }
        }
    }
}
