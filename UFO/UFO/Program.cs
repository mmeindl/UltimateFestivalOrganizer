using System;
using System.Configuration;
using System.Data.SqlClient;
using UFO.Domain;
using UFO.Dal.Common;
using System.Transactions;
//using UFO.Dal.SqlServer;

namespace UFO.Test
{
    public static class Program
    {

        private static void Main(string[] args)
        {
            IDatabase database = DalFactory.CreateDatabase();
            IArtistDao artistDao = DalFactory.CreateArtistDao(database);

            Console.WriteLine(artistDao.FindById(1).ToString());

            Console.ReadLine();
        }
    }
}
