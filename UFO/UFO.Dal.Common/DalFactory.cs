using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Dal.Common
{
    public static class DalFactory
    {
        private static readonly string assemblyName;
        private static readonly Assembly assembly;

        static DalFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DalAssembly"];
            assembly = Assembly.Load(assemblyName);
        }

        public static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        public static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type databaseClass = assembly.GetType(databaseClassName);

            return Activator.CreateInstance(
                databaseClass,
                new object[] { connectionString }) as IDatabase;
        }

        private static TInterface CreateDao<TInterface>(IDatabase database, string typeName)
        {
            Type daoType = assembly.GetType(assemblyName + "." + typeName);
            return (TInterface) Activator.CreateInstance(daoType, new object[] { database });
        }

        public static IArtistDao CreateArtistDao(IDatabase database)
        {
            return CreateDao<IArtistDao>(database, "ArtistDao");
        }

        public static IArtistPictureDao CreateArtistPictureDao(IDatabase database)
        {
            return CreateDao<IArtistPictureDao>(database, "ArtistPictureDao");
        }

        public static IArtistVideoDao CreateArtistVideoDao(IDatabase database)
        {
            return CreateDao<IArtistVideoDao>(database, "ArtistVideoDao");
        }

        public static ICategoryDao CreateCategoryDao(IDatabase database)
        {
            return CreateDao<ICategoryDao>(database, "CategoryDao");
        }

        public static ICountryDao CreateCountryDao(IDatabase database)
        {
            return CreateDao<ICountryDao>(database, "CountryDao");
        }

        public static IPictureDao CreatePictureDao(IDatabase database)
        {
            return CreateDao<IPictureDao>(database, "PictureDao");
        }

        public static IVideoDao CreateVideoDao(IDatabase database)
        {
            return CreateDao<IVideoDao>(database, "VideoDao");
        }

        public static IPerformanceDao CreatePerformanceDao(IDatabase database)
        {
            return CreateDao<IPerformanceDao>(database, "PerformanceDao");
        }

        public static IPerformancePictureDao CreatePerformancePictureDao(IDatabase database)
        {
            return CreateDao<IPerformancePictureDao>(database, "PerformancePictureDao");
        }

        public static IPerformanceVideoDao CreatePerformanceVideoDao(IDatabase database)
        {
            return CreateDao<IPerformanceVideoDao>(database, "PerformanceVideoDao");
        }

        public static IRoleDao CreateRoleDao(IDatabase database)
        {
            return CreateDao<IRoleDao>(database, "RoleDao");
        }

        public static IUserDao CreateUserDao(IDatabase database)
        {
            return CreateDao<IUserDao>(database, "UserDao");
        }

        public static IAreaDao CreateAreaDao(IDatabase database)
        {
            return CreateDao<IAreaDao>(database, "AreaDao");
        }

        public static IVenueDao CreateVenueDao(IDatabase database)
        {
            return CreateDao<IVenueDao>(database, "VenueDao");
        }
    }
}
