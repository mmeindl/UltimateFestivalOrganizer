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
    class CountryDao : ICountryDao
    {
        const string SQL_FIND_BY_ABBREVIATION =
            @"SELECT *
                FROM Country
                WHERE Country.abbreviation = @abbreviation";

        const string SQL_FIND_ALL =
            @"SELECT *
              FROM Country";

        private IDatabase database;

        public CountryDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateFindByAbbreviationCommand(string abbreviation)
        {
            DbCommand findByAbbreviationCommand = database.CreateCommand(SQL_FIND_BY_ABBREVIATION);
            database.DefineParameter(findByAbbreviationCommand, "abbreviation", DbType.AnsiString, abbreviation);
            return findByAbbreviationCommand;
        }

        public Country FindByAbbreviation(string abbreviation)
        {
            using (DbCommand command = CreateFindByAbbreviationCommand(abbreviation))
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Country(
                        (string)reader["abbreviation"],
                        (string)reader["name"]);
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

        public IList<Country> FindAll()
        {
            using (DbCommand command = CreateFindAllCommand())
            using (IDataReader reader = database.ExecuteReader(command))
            {
                IList<Country> result = new List<Country>();
                while (reader.Read())
                    result.Add(new Country(
                        (string)reader["abbreviation"],
                        (string)reader["name"])
                        );
                return result;
            }
        }

    }
}
