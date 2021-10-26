using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Music_Store_Tests.Services
{
    public class SongsServiceTest
    {
        DbContextOptions<ApplicationDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;


            using (var context = new ApplicationDbContext(_options))
            {
                var songs = new Song[]
                {
                    new Song
                    {
                        Name = "Kings & Queens",
                        ReleaseDate = new DateTime(2020, 3, 12),
                        RuntimeInSec = 162,
                        Price = 3.99f,
                    },
                    new Song
                    {
                        Name = "Break My Heart",
                        ReleaseDate = new DateTime(2020, 3, 25),
                        RuntimeInSec = 221,
                        Price = 3.99f,
                    }
                };

                context.AddRange(songs);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task TestGetSong_Zero_ReturnNull()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                ISongsService service = new SongsService(context);

                var result = await service.GetSongOrNullAsync(0);

                Assert.AreEqual(null, result);
            }
        }

        [Test]
        public async Task TestGetSong_One_ReturnNull()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                ISongsService service = new SongsService(context);

                var result = await service.GetSongOrNullAsync(1);

                Assert.AreEqual(null, result);
            }
        }
    }
}
