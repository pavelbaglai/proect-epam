using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Music_Store.Controllers;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Services;
using Music_Store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Options;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Music_Store_Tests
{
    public class SearchControllerTest
    {
        Mock<ISearchService> _search;
        DbContextOptions _options;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

            _options = options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Artists.Add(
                    new Artist
                    {
                        Stagename = "TestArtist",
                        Fullname = "Example Test Artist",
                        DebutYear = 1970,
                        ImagePath = $"./artistImage.png"
                    });
                context.Songs.Add(
                    new Song
                    {
                        ArtistID = 1,
                        PublisherID = 1,
                        Name = "TestSong",
                        ImagePath = $"./songImage.png",
                        ReleaseDate = new DateTime(1970, 1, 1),
                        RuntimeInSec = 300,
                        Price = 1.99f,
                    });
                context.Albums.Add(
                    new Album
                    {
                        PublisherID = 1,
                        ArtistID = 1,
                        Name = "TestAlbum",
                        ImagePath = $"./albumImage.png",
                        PublishDate = new DateTime(1970, 1, 1),
                        Price = 19.99f,
                    });
                context.SaveChanges();
            }
        }

        //This test will throw exception, as blank search strings are redirected to main index view
        [Test]
        public async Task TestSearchMethod_EmptySearchString_NullReferenceException()
        {
            _search = new Mock<ISearchService>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

            // Arrange
            var _context = new ApplicationDbContext(options);
            var controller = new HomeController(_context, _search.Object);
  
            // Act
            var result = await controller.Search("") as ViewResult;

            // Assert
            Assert.Throws<NullReferenceException>(() => result.ViewName.ToString());
        }

        //This test should return true
        [TestCase("Test")]
        public async Task TestSearchMethod_SearchWithValve_ReturnAllMatching(string searchString)
        {
            _search = new Mock<ISearchService>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

            // Arrange
            var _context = new ApplicationDbContext(options);

            var searchService = new SearchService(_context);
            // Act
            var result = await searchService.GetSearchResult(searchString);

            // Assert
            Assert.True(!result.ArtistList.IsNullOrEmpty());
        }
    }
}