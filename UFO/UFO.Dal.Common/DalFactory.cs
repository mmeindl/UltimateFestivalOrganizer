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

        public static IArtistDao CreateArtistDao()
        {
            return CreateDao<IArtistDao>(CreateDatabase(), "ArtistDao");
        }

        public static IArtistPictureDao CreateArtistPictureDao()
        {
            return CreateDao<IArtistPictureDao>(CreateDatabase(), "ArtistPictureDao");
        }

        public static IArtistVideoDao CreateArtistVideoDao()
        {
            return CreateDao<IArtistVideoDao>(CreateDatabase(), "ArtistVideoDao");
        }

        public static ICategoryDao CreateCategoryDao()
        {
            return CreateDao<ICategoryDao>(CreateDatabase(), "CategoryDao");
        }

        public static ICountryDao CreateCountryDao()
        {
            return CreateDao<ICountryDao>(CreateDatabase(), "CountryDao");
        }

        public static IPictureDao CreatePictureDao()
        {
            return CreateDao<IPictureDao>(CreateDatabase(), "PictureDao");
        }

        public static IVideoDao CreateVideoDao()
        {
            return CreateDao<IVideoDao>(CreateDatabase(), "VideoDao");
        }

        public static IPerformanceDao CreatePerformanceDao()
        {
            return CreateDao<IPerformanceDao>(CreateDatabase(), "PerformanceDao");
        }

        public static IPerformancePictureDao CreatePerformancePictureDao()
        {
            return CreateDao<IPerformancePictureDao>(CreateDatabase(), "PerformancePictureDao");
        }

        public static IPerformanceVideoDao CreatePerformanceVideoDao()
        {
            return CreateDao<IPerformanceVideoDao>(CreateDatabase(), "PerformanceVideoDao");
        }

        public static IRoleDao CreateRoleDao()
        {
            return CreateDao<IRoleDao>(CreateDatabase(), "RoleDao");
        }

        public static IUserDao CreateUserDao()
        {
            return CreateDao<IUserDao>(CreateDatabase(), "UserDao");
        }

        public static IAreaDao CreateAreaDao()
        {
            return CreateDao<IAreaDao>(CreateDatabase(), "AreaDao");
        }

        public static IVenueDao CreateVenueDao()
        {
            return CreateDao<IVenueDao>(CreateDatabase(), "VenueDao");
        }
    }
}
