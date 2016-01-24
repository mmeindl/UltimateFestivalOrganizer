using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UFO.Dal.Common;
using UFO.Domain;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;

namespace UFO.UnitTest
{
    [TestClass]
    public class UFOTests
    {
        /* ----- CATEGORY ----- */

        [TestMethod]
        public void CategoryFindByIdTest()
        {
            
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao();

            Category category = categoryDao.FindById(1);

            Assert.IsNotNull(category);
            Assert.AreEqual(category.Name, "Akrobatik");
            Assert.IsNull(categoryDao.FindById(100));
        }

        [TestMethod]
        public void CategoryFindAllTest()
        {
            
            ICategoryDao categoryDao = DalFactory.CreateCategoryDao();

            Assert.AreEqual(categoryDao.FindAll().Count, 4);
        }


        /* ----- COUNTRY ----- */

        [TestMethod]
        public void CategoryFindByAbbreviationTest()
        {
            
            ICountryDao countryDao = DalFactory.CreateCountryDao();

            Country country = countryDao.FindByAbbreviation("alb");

            Assert.IsNotNull(country);
            Assert.AreEqual(country.Name, "Albania");
            Assert.IsNull(countryDao.FindByAbbreviation("xyz"));
        }

        [TestMethod]
        public void CountryFindAllTest()
        {
            
            ICountryDao countryDao = DalFactory.CreateCountryDao();

            Assert.AreEqual(countryDao.FindAll().Count, 31);
        }


        /* ----- ARTIST ----- */

        [TestMethod]
        public void ArtistFindByIdTest()
        {
            
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            Artist artist = artistDao.FindById(1);

            Assert.IsNotNull(artist);
            Assert.AreEqual(artist.Name, "Zelenia Cortez");
            Assert.IsNull(artistDao.FindById(100));
        }

        [TestMethod]
        public void ArtistFindByNameTest()
        {
            
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            Assert.IsNotNull(artistDao.FindByName("Zelenia Cortez"));
            Assert.IsNull(artistDao.FindByName("No Name"));
        }

        [TestMethod]
        public void ArtistFindAllTest()
        {
            
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            Assert.AreEqual(artistDao.FindAll().Count, 66);
        }


        [TestMethod]
        public void ArtistInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();

                string name = "Tanka";
                Artist artist = new Artist("aut", 4, name, "mail@tanka.at");

                Assert.IsTrue(artistDao.Insert(artist));
                Assert.IsNotNull(artistDao.FindByName(name));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ArtistInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();
                
                Artist artist = new Artist();

                artistDao.Insert(artist);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistUpdateTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();

                string name = "Manuel";
                Artist artist = artistDao.FindById(1);
                artist.Name = name;

                Assert.IsTrue(artistDao.Update(artist));

                artist = artistDao.FindByName(name);

                Assert.IsNotNull(artist);

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ArtistUpdateTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();

                Artist artist = artistDao.FindById(1);
                artist.CountryId = "abc";

                artistDao.Update(artist);

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ArtistUpdateTest3()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();

                Artist artist = artistDao.FindById(1);
                artist.Email = "keine Emailadresse";

                artistDao.Update(artist);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistDao artistDao = DalFactory.CreateArtistDao();
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                Artist artist = artistDao.FindById(1);

                Assert.IsTrue(artistDao.Delete(artist));

                Assert.IsTrue(artistDao.FindById(1).IsDeleted);

                Assert.AreEqual(performanceDao.FindAllInFutureByArtistId(1).Count, 0);

                scope.Dispose();
            }
        }


        /* ----- PERFORMANCE ----- */

        [TestMethod]
        public void PerformanceFindByIdTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Performance performance = performanceDao.FindById(1);

            Assert.IsNotNull(performance);
            Assert.AreEqual(performance.DateTime.ToString(), "22.07.2016 14:00:00");
            Assert.IsNull(performanceDao.FindById(1000));
        }

        [TestMethod]
        public void PerformanceFindAllTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Assert.AreEqual(performanceDao.FindAll().Count, 288);
        }

        [TestMethod]
        public void PerformanceFindAllByDateTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Assert.IsNotNull(performanceDao.FindAllByDate(new DateTime(2016, 07, 22)));

            Assert.AreEqual(performanceDao.FindAllByDate(new DateTime(2000, 01, 01)).Count, 0);
        }

        [TestMethod]
        public void PerformanceFindAllInFutureTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            IList<Performance> performances = performanceDao.FindAllInFuture();

            Assert.IsNotNull(performances);

            foreach (Performance performance in performances)
            {
                Assert.IsTrue(performance.DateTime > new DateTime());
            }
        }

        [TestMethod]
        public void PerformanceFindAllByArtistIdTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Assert.IsNotNull(performanceDao.FindAllByArtistId(1));

            Assert.AreEqual(performanceDao.FindAllByArtistId(100).Count, 0);
        }

        [TestMethod]
        public void PerformanceFindByDateAndArtistTest()
        {

            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();
            IArtistDao artistDao = DalFactory.CreateArtistDao();

            Assert.IsNotNull(performanceDao.FindByDateAndArtist(new DateTime(2016, 07, 22), artistDao.FindById(1)));
        }

        [TestMethod]
        public void PerformanceFindByDateAndVenueTest()
        {

            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            Assert.IsNotNull(performanceDao.FindByDateAndVenue(new DateTime(2016, 07, 22), venueDao.FindById(1)));
        }

        [TestMethod]
        public void PerformanceFindAllByDateTimeTest()
        {

            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Assert.IsNotNull(performanceDao.FindAllByDateTime(new DateTime(2016, 07, 22, 10, 0, 0)));
        }

        [TestMethod]
        public void PerformanceFindAllByVenueIdTest()
        {
            
            IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

            Assert.IsNotNull(performanceDao.FindAllByVenueId(1));

            Assert.AreEqual(performanceDao.FindAllByVenueId(100).Count, 0);
        }

        [TestMethod]
        public void PerformanceInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                Performance performance = new Performance(new DateTime(2016, 07, 23, 16, 00, 00), 40, 1);

                Assert.IsTrue(performanceDao.Insert(performance));

                Performance performance2 = new Performance(new DateTime(2016, 07, 22, 13, 00, 00), 1, 1);
                Performance performance3 = new Performance(new DateTime(2016, 07, 22, 14, 00, 00), 1, 1);
                Performance performance4 = new Performance(new DateTime(2016, 07, 22, 19, 00, 00), 1, 1);

                Assert.IsFalse(performanceDao.Insert(performance2));
                Assert.IsFalse(performanceDao.Insert(performance3));
                Assert.IsFalse(performanceDao.Insert(performance4));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void PerformanceInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                Performance performance = new Performance(new DateTime(2016, 07, 23, 16, 30, 00), 40, 1);

                performanceDao.Insert(performance);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void PerformanceUpdateTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                DateTime dateTime = new DateTime(2016, 07, 22, 10, 00, 00);

                Performance performance = performanceDao.FindById(1);
                performance.DateTime = dateTime;

                Assert.IsTrue(performanceDao.Update(performance));

                performance = performanceDao.FindById(1);

                Assert.AreEqual(performance.DateTime, dateTime);

                DateTime dateTime2 = new DateTime(2016, 07, 22, 11, 00, 00);

                /* Performance 57 and 1 have same Artist */
                Performance performance2 = performanceDao.FindById(57);
                performance2.DateTime = dateTime2;

                Assert.IsFalse(performanceDao.Update(performance2));

                performance2 = performanceDao.FindById(57);

                Assert.AreNotEqual(performance2.DateTime, dateTime2);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void PerformanceDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                Performance performance = performanceDao.FindById(1);

                Assert.IsNotNull(performance);
                Assert.IsTrue(performanceDao.Delete(performance));
                Assert.IsNull(performanceDao.FindById(1));

                scope.Dispose();
            }
        }

        [TestMethod]
        public void PerformanceDeleteAllInFutureByArtistIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceDao performanceDao = DalFactory.CreatePerformanceDao();

                Assert.IsTrue(performanceDao.DeleteAllInFutureByArtistId(1));

                IList<Performance> performances = performanceDao.FindAllInFuture();

                foreach (Performance performance in performances)
                {
                    Assert.AreNotEqual(performance.ArtistId, 1);
                }

                scope.Dispose();

            }
        }


        /* ----- Area ----- */

        [TestMethod]
        public void AreaFindByIdTest()
        {

            IAreaDao areaDao = DalFactory.CreateAreaDao();

            Assert.IsNotNull(areaDao.FindById(1));
            Assert.IsNull(areaDao.FindById(-1));
        }

        [TestMethod]
        public void AreaFindByNameTest()
        {
            
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            Assert.IsNotNull(areaDao.FindByName("Hauptplatz"));
        }

        [TestMethod]
        public void AreaFindAllTest()
        {
            
            IAreaDao areaDao = DalFactory.CreateAreaDao();

            Assert.AreEqual(areaDao.FindAll().Count, 5);
        }

        [TestMethod]
        public void AreaInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IAreaDao areaDao = DalFactory.CreateAreaDao();

                Area area = new Area("Dom");

                Assert.IsTrue(areaDao.Insert(area));

                scope.Dispose();
            }
        }

        [TestMethod]
        public void AreaDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                IAreaDao areaDao = DalFactory.CreateAreaDao();

                Area area = new Area("Test");

                areaDao.Insert(area);
                area = areaDao.FindByName("Test");

                Assert.IsTrue(areaDao.Delete(area));

                Assert.IsNull(areaDao.FindByName("Test"));

                scope.Dispose();
            }
        }

        /* ----- VENUE ----- */

        [TestMethod]
        public void VenueFindByIdTest()
        {
            
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            Venue venue = venueDao.FindById(1);

            Assert.IsNotNull(venue);
            Assert.AreEqual(venue.Name, "Broken Arrow");
            Assert.IsNull(venueDao.FindById(1000));
        }

        [TestMethod]
        public void VenueFindByAreaIdTest()
        {

            IVenueDao venueDao = DalFactory.CreateVenueDao();

            IList<Venue> venue = venueDao.FindByAreaId(1);

            Assert.IsNotNull(venue);
        }

        [TestMethod]
        public void VenueFindAllTest()
        {
            
            IVenueDao venueDao = DalFactory.CreateVenueDao();

            Assert.AreEqual(venueDao.FindAll().Count, 40);
        }

        [TestMethod]
        public void VenueFindByNameTest()
        {

            IVenueDao venueDao = DalFactory.CreateVenueDao();

            Venue venue = venueDao.FindByName("Broken Arrow");

            Assert.IsNotNull(venue);

            venue = venueDao.FindByName("noVenue");

            Assert.IsNull(venue);
        }

        [TestMethod]
        public void VenueInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IVenueDao venueDao = DalFactory.CreateVenueDao();

                decimal lat = (Decimal) 48.995997;
                decimal lon = (Decimal) 14.984131;

                Venue venue = new Venue(1, "Tanka Tanzt", "H1", lon, lat);

                Assert.IsTrue(venueDao.Insert(venue));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void VenueInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IVenueDao venueDao = DalFactory.CreateVenueDao();

                Venue venue = new Venue();

                venueDao.Insert(venue);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void VenueUpdateTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IVenueDao venueDao = DalFactory.CreateVenueDao();

                string shortName = "H2";
                Venue venue = venueDao.FindById(1);
                venue.ShortName = shortName;

                Assert.IsTrue(venueDao.Update(venue));

                venue = venueDao.FindById(1);

                Assert.IsTrue(venue.ShortName == shortName);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void VenueDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IVenueDao venueDao = DalFactory.CreateVenueDao();

                decimal lat = (Decimal)48.995997;
                decimal lon = (Decimal)14.984131;

                Venue venue = new Venue(1, "Tanka Tanzt", "H1", lon, lat);

                venueDao.Insert(venue);

                venue = venueDao.FindByName("Tanka Tanzt");

                Assert.IsTrue(venueDao.Delete(venue));

                Assert.IsNull(venueDao.FindByName("Tanka Tanzt"));

                scope.Dispose();
            }
        }


        /* ----- ROLE ----- */

        [TestMethod]
        public void RoleFindByIdTest()
        {
            
            IRoleDao roleDao = DalFactory.CreateRoleDao();

            Role role = roleDao.FindById(1);

            Assert.IsNotNull(role);
            Assert.AreEqual(role.Title, "administrator");
            Assert.IsNull(roleDao.FindById(1000));
        }


        /* ----- USER ----- */

        [TestMethod]
        public void UserFindByIdTest()
        {
            
            IUserDao userDao = DalFactory.CreateUserDao();

            User user = userDao.FindById(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Username, "TaMaAdmin");
            Assert.IsNull(userDao.FindById(1000));
        }

        [TestMethod]
        public void UserFindByUsernameTest()
        {
            
            IUserDao userDao = DalFactory.CreateUserDao();

            User user = userDao.FindByUsername("TaMaAdmin");

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, 1);
            Assert.IsNull(userDao.FindByUsername("kein Username"));
        }

        [TestMethod]
        public void UserFindAllTest()
        {
            
            IUserDao userDao = DalFactory.CreateUserDao();

            Assert.AreEqual(userDao.FindAll().Count, 2);
        }

        /* ----- PICTURE ----- */

        [TestMethod]
        public void PictureFindByURLTest()
        {
            
            IPictureDao pictureDao = DalFactory.CreatePictureDao();

            Assert.IsNotNull(pictureDao.FindByURL("https://placekitten.com/150/200"));
            Assert.IsNull(pictureDao.FindByURL("noURL"));
        }

        [TestMethod]
        public void PictureFindAllTest()
        {
            
            IPictureDao pictureDao = DalFactory.CreatePictureDao();

            Assert.AreEqual(pictureDao.FindAll().Count, 8);
        }


        /* ----- VIDEO ----- */

        [TestMethod]
        public void VideoFindByURLTest()
        {
            
            IVideoDao videoDao = DalFactory.CreateVideoDao();

            Assert.IsNotNull(videoDao.FindByURL("https://youtu.be/3O1_3zBUKM8"));
            Assert.IsNull(videoDao.FindByURL("noURL"));
        }

        [TestMethod]
        public void VideoFindAllTest()
        {
            
            IVideoDao videoDao = DalFactory.CreateVideoDao();

            Assert.AreEqual(videoDao.FindAll().Count, 12);
        }


        /* ----- ARTISTPICTURE ----- */

        [TestMethod]
        public void ArtistPictureFindByURLTest()
        {
            
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            Assert.IsNotNull(artistPictureDao.FindByURL("https://placekitten.com/150/200"));
            Assert.IsNull(artistPictureDao.FindByURL("noURL"));
        }
        
        [TestMethod]
        public void ArtistPictureFindProfilePictureByArtistIdTest()
        {
            
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            /* Artist 1 has ProfilePicture */
            ArtistPicture profilePicture1 = artistPictureDao.FindProfilePictureByArtistId(1);

            Assert.IsNotNull(profilePicture1);
            Assert.IsTrue(profilePicture1.IsProfilePicture);

            /*Artist 66 has no ProfilePicture */
            ArtistPicture profilePicture2 = artistPictureDao.FindProfilePictureByArtistId(66);

            Assert.IsNull(profilePicture2);
        }
        
        [TestMethod]
        public void ArtistPictureFindAllByArtistIdTest()
        {
            
            IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

            /* Artist 1 has Picture */
            Assert.IsNotNull(artistPictureDao.FindAllByArtistId(1));

            /* Artist 3 has no Picture */
            Assert.IsTrue(artistPictureDao.FindAllByArtistId(3).Count == 0);
        }

        [TestMethod]
        public void ArtistPictureInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

                string url = "https://placekitten.com/150/1100";
                ArtistPicture artistPicture = new ArtistPicture(url, 10);

                Assert.IsTrue(artistPictureDao.Insert(artistPicture));
                Assert.IsNotNull(artistPictureDao.FindByURL(url));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ArtistPictureInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

                string url = "keine URL";
                ArtistPicture artistPicture = new ArtistPicture(url, 10);

                artistPictureDao.Insert(artistPicture);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistPictureUpdateTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

                ArtistPicture profilePicture = artistPictureDao.FindProfilePictureByArtistId(1);
                profilePicture.IsProfilePicture = false;

                Assert.IsTrue(artistPictureDao.Update(profilePicture));
                Assert.IsNull(artistPictureDao.FindProfilePictureByArtistId(1));

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistPictureDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistPictureDao artistPictureDao = DalFactory.CreateArtistPictureDao();

                /* Artist 1 has a ProfilePicture which is also in Performances */
                ArtistPicture profilePicture1 = artistPictureDao.FindProfilePictureByArtistId(1);

                Assert.IsNotNull(profilePicture1);
                Assert.IsTrue(artistPictureDao.Delete(profilePicture1));

                /* Artist 20 has a ProfilePicture which is not in Performances */
                ArtistPicture profilePicture2 = artistPictureDao.FindProfilePictureByArtistId(20);

                Assert.IsNotNull(profilePicture2);
                Assert.IsTrue(artistPictureDao.Delete(profilePicture2));
                Assert.IsNull(artistPictureDao.FindProfilePictureByArtistId(20));

                scope.Dispose();
            }
        }


        /* ----- ARTISTVIDEO ----- */

        [TestMethod]
        public void ArtistVideoFindByURLTest()
        {
            
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            Assert.IsNotNull(artistVideoDao.FindByURL("https://youtu.be/YQHsXMglC9A"));
            Assert.IsNull(artistVideoDao.FindByURL("noURL"));
        }

        [TestMethod]
        public void ArtistVideoFindPromoVideoByArtistIdTest()
        {

            
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            /* Artist 1 has PromoVideo */
            ArtistVideo promoVideo1 = artistVideoDao.FindPromoVideoByArtistId(1);

            Assert.IsNotNull(promoVideo1);
            Assert.IsTrue(promoVideo1.IsPromoVideo);

            /* Artist 66 has no PromoVideo */
            ArtistVideo promoVideo2 = artistVideoDao.FindPromoVideoByArtistId(66);

            Assert.IsNull(promoVideo2);
        }

        [TestMethod]
        public void ArtistVideoFindAllByArtistIdTest()
        {
            
            IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

            /* Artist 1 has Video */
            Assert.IsNotNull(artistVideoDao.FindAllByArtistId(1));

            /* Artist 3 has no Video */
            Assert.IsTrue(artistVideoDao.FindAllByArtistId(3).Count == 0);
        }

        [TestMethod]
        public void ArtistVideoInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

                string url = "https://youtu.be/YQHslC9AskksEdfsdf";
                ArtistVideo artistVideo = new ArtistVideo(url, 10);

                Assert.IsTrue(artistVideoDao.Insert(artistVideo));
                Assert.IsNotNull(artistVideoDao.FindByURL(url));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ArtistVideoInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

                string url = "no URL";
                ArtistVideo artistVideo = new ArtistVideo(url, 10);

                artistVideoDao.Insert(artistVideo);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistVideoUpdateTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

                ArtistVideo promoVideo = artistVideoDao.FindPromoVideoByArtistId(1);
                promoVideo.IsPromoVideo = false;

                Assert.IsTrue(artistVideoDao.Update(promoVideo));
                Assert.IsNull(artistVideoDao.FindPromoVideoByArtistId(1));

                scope.Dispose();
            }
        }

        [TestMethod]
        public void ArtistVideoDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IArtistVideoDao artistVideoDao = DalFactory.CreateArtistVideoDao();

                /* Artist 1 has a PromoVideo which is also in Performances */
                ArtistVideo promoVideo1 = artistVideoDao.FindPromoVideoByArtistId(1);

                Assert.IsNotNull(promoVideo1);
                Assert.IsTrue(artistVideoDao.Delete(promoVideo1));

                /* Artist 20 has a PromoVideo which is not in Performances */
                ArtistVideo promoVideo2 = artistVideoDao.FindPromoVideoByArtistId(20);

                Assert.IsNotNull(promoVideo2);
                Assert.IsTrue(artistVideoDao.Delete(promoVideo2));
                Assert.IsNull(artistVideoDao.FindPromoVideoByArtistId(20));

                scope.Dispose();
            }
        }


        /* ----- PERFORMANCEPICTURE ----- */

        [TestMethod]
        public void PerformancePictureFindByURLTest()
        {
            
            IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();

            Assert.IsNotNull(performancePictureDao.FindByURL("https://placekitten.com/150/200"));
            Assert.IsNull(performancePictureDao.FindByURL("noURL"));
        }

        [TestMethod]
        public void PerformancePictureFindAllByPerformanceIdTest()
        {
            
            IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();

            /* Performance 1 has Picture */
            Assert.IsNotNull(performancePictureDao.FindAllByPerformanceId(1));

            /* Performance 3 has no Picture */
            Assert.IsTrue(performancePictureDao.FindAllByPerformanceId(3).Count == 0);
        }

        [TestMethod]
        public void PerformancePictureInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();

                string url = "https://placekitten.com/150/2100";

                PerformancePicture performancePicture = new PerformancePicture(url, 10);

                Assert.IsTrue(performancePictureDao.Insert(performancePicture));
                Assert.IsNotNull(performancePictureDao.FindByURL(url));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void PerformancePictureInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();

                string url = "no URL";

                PerformancePicture performancePicture = new PerformancePicture(url, 10);

                performancePictureDao.Insert(performancePicture);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void PerformancePictureDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformancePictureDao performancePictureDao = DalFactory.CreatePerformancePictureDao();

                string url1 = "https://placekitten.com/150/210";
                string url2 = "https://placekitten.com/650/600";

                /* Performance with url1 has a Picture which is also in Artitsts */
                PerformancePicture performancePicture1 = performancePictureDao.FindByURL(url1);

                Assert.IsNotNull(performancePicture1);
                Assert.IsTrue(performancePictureDao.Delete(performancePicture1));

                /* Performance with url2 has a ProfilePicture which is not in Artists */
                PerformancePicture performancePicture2 = performancePictureDao.FindByURL(url2);

                Assert.IsNotNull(performancePicture2);
                Assert.IsTrue(performancePictureDao.Delete(performancePicture2));
                Assert.IsNull(performancePictureDao.FindByURL(url2));

                scope.Dispose();
            }
        }


        /* ----- PERFORMANCEVIDEO ----- */

        [TestMethod]
        public void PerformanceVideoFindByURLTest()
        {
            
            IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            Assert.IsNotNull(performanceVideoDao.FindByURL("https://youtu.be/YQHsXMglC9A"));
            Assert.IsNull(performanceVideoDao.FindByURL("noURL"));
        }

        [TestMethod]
        public void PerformanceVideoFindAllByPerformanceIdTest()
        {
            
            IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

            /* Performance 1 has Video */
            Assert.IsNotNull(performanceVideoDao.FindAllByPerformanceId(1));

            /* Performance 3 has no Video */
            Assert.IsTrue(performanceVideoDao.FindAllByPerformanceId(3).Count == 0);
        }

        [TestMethod]
        public void PerformanceVideoInsertTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

                string url = "https://youtu.be/YQHslC9AskksEdfsdf";
                PerformanceVideo PerformanceVideo = new PerformanceVideo(url, 10);

                Assert.IsTrue(performanceVideoDao.Insert(PerformanceVideo));
                Assert.IsNotNull(performanceVideoDao.FindByURL(url));

                scope.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void PerformanceVideoInsertTest2()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

                string url = "no URL";
                PerformanceVideo PerformanceVideo = new PerformanceVideo(url, 10);

                performanceVideoDao.Insert(PerformanceVideo);

                scope.Dispose();
            }
        }

        [TestMethod]
        public void PerformanceVideoDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                IPerformanceVideoDao performanceVideoDao = DalFactory.CreatePerformanceVideoDao();

                string url1 = "https://youtu.be/3O1_3zBUKM8";
                string url2 = "https://youtu.be/YQHsXMglC9ASCD12345";

                /* Performance with url1 has a Video which is also in Artists */
                PerformanceVideo performanceVideo1 = performanceVideoDao.FindByURL(url1);

                Assert.IsNotNull(performanceVideo1);
                Assert.IsTrue(performanceVideoDao.Delete(performanceVideo1));

                /* Performance url2 has a Video which is not in Artists */
                PerformanceVideo performanceVideo2 = performanceVideoDao.FindByURL(url2);

                Assert.IsNotNull(performanceVideo2);
                Assert.IsTrue(performanceVideoDao.Delete(performanceVideo2));
                Assert.IsNull(performanceVideoDao.FindByURL(url2));

                scope.Dispose();
            }
        }


    }
}
