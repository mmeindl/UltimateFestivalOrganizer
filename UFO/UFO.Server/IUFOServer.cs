﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;

namespace UFO.Server
{
    public interface IUFOServer
    {
        // Artist
        Artist FindArtistById(int artistId);
        Artist FindArtistByName(string name);
        IEnumerable<Artist> FindAllArtists();

        bool InsertArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artists);

        // ArtistPicture
        ArtistPicture FindArtistPictureByURL(string url);
        IEnumerable<ArtistPicture> FindAllPicturesByArtistId(int artistId);
        ArtistPicture FindProfilePictureByArtistId(int artistId);

        bool InsertArtistPicture(ArtistPicture artistPicture);
        bool UpdateArtistPicture(ArtistPicture artistPicture);
        bool DeleteArtistPicture(ArtistPicture artistPicture);

        // ArtistVideo
        ArtistVideo FindArtistVideoByURL(string url);
        IEnumerable<ArtistVideo> FindAllVideosByArtistId(int artistId);
        ArtistVideo FindPromoVideoByArtistId(int artistId);

        bool InsertArtistVideo(ArtistVideo artistVideo);
        bool UpdateArtistVideo(ArtistVideo artistVideo);
        bool DeleteArtistVideo(ArtistVideo artistVideo);

        //Area
        Area FindAreaByName(string name);
        IEnumerable<Area> FindAllAreas();

        bool InsertArea(Area area);
        bool DeleteArea(Area area);

        // Venue
        Venue FindVenueById(int id);
        Venue FindVenueByName(string name);
        IEnumerable<Venue> FindVenuesByAreaId(int areaId);
        IEnumerable<Venue> FindAllVenues();

        bool InsertVenue(Venue venue);
        bool UpdateVenue(Venue venue);
        bool DeleteVenue(Venue venue);

        // User
        bool AuthenticateUser(string usernername, string password, IList<string> error);
        User FindUserByName(string name);

        //Category
        Category FindCategoryById(int id);
        IEnumerable<Category> FindAllCategories();

        //Country
        Country FindCountryByAbbreviation(string abbreviation);
        IEnumerable<Country> FindAllCountries();

        //Performance
        IEnumerable<Performance> FindAllPerformances();
        Performance FindPerformanceById(int id);
        Performance FindPerformanceByDateTimeAndArtistId(DateTime dateTime, int artistId);
        IEnumerable<Performance> FindPerformancesByDate(DateTime date);
        IEnumerable<Performance> FindPerformancesByDateTime(DateTime date);
        IEnumerable<Performance> FindPerformancesByDateAndVenue(DateTime date, Venue venue);
        IEnumerable<Performance> FindPerformancesByDateAndArtist(DateTime date, Artist artist);

        IEnumerable<DateTime> GetPerformanceDates();

        bool InsertPerformance(Performance performance);
        bool UpdatePerformance(Performance performance);
        bool DeletePerformance(Performance performance);

        // PerformancePicture
        PerformancePicture FindPerformancePictureByURL(string url);
        IEnumerable<PerformancePicture> FindAllPicturesByPerformanceId(int performanceId);

        bool InsertPerformancePicture(PerformancePicture performancePicture);
        bool DeletePerformancePicture(PerformancePicture performancePicture);

        // PerformanceVideo
        PerformanceVideo FindPerformanceVideoByURL(string url);
        IEnumerable<PerformanceVideo> FindAllVideosByPerformanceId(int performanceId);

        bool InsertPerformanceVideo(PerformanceVideo performanceVideo);
        bool DeletePerformanceVideo(PerformanceVideo performanceVideo);

        // helpers
        bool UpdateArtistMedia(Artist artist, ArtistPicture picture, ArtistVideo video);


    }
}
