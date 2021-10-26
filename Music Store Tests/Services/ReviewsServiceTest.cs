using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store_Tests.Services
{
    class ReviewsServiceTest
    {
        DbContextOptions<ApplicationDbContext> _options;
        Mock<SignInManager<User>> _mockSignInManager;
        IReviewsService _service;

        [SetUp]
        public void SetUp()
        {
            _options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "testDatabase")
                    .Options;

            _mockSignInManager = new Mock<SignInManager<User>>();

            using (var context = new ApplicationDbContext(_options))
            {
                var customers = new Customer[]
                {
                    new Customer
                    {
                        ID = 1
                    },
                    new Customer
                    {
                        ID = 2,
                        Reviews = new List<Review>
                        {
                            new Review
                            {
                                ID = 5
                            }
                        }
                    },
                    new Customer
                    {
                        ID = 3
                    },
                };

                context.AddRange(customers);
                context.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _service = null;
        }

        [Test]
        public async Task AddReviewToItemAsync_CustomerIDAndReviewViewModel_SameReviewViewModel()
        {
            // arrange
            int customerID = 3;
            string category = nameof(Song);
            string content = "It is good!";
            int itemID = 5;
            int rating = 3;

            ReviewViewModel rvm = new ReviewViewModel
            {
                Category = category,
                Content = content,
                ItemID = itemID,
                Rating = rating
            };

            using (var context = new ApplicationDbContext(_options))
            {
                _service = new ReviewsService(context);

                // act
                await _service.AddReviewToItemAsync(customerID, rvm);

                var result = context.Find<Customer>(customerID).Reviews.FirstOrDefault();

                // assert
                Assert.AreEqual(result.Content, content);
                Assert.AreEqual(result.SongID, itemID);
                Assert.AreEqual(result.Rating, rating);
            }
        }

        [Test]
        public async Task RemoveReviewFromItemAsync_CustomerIDAndReviewID_Null()
        {
            // arrange
            int customerID = 2;
            int reviewID = 5;

            using (var context = new ApplicationDbContext(_options))
            {
                _service = new ReviewsService(context);

                // act
                await _service.RemoveReviewFromItemAsync(customerID, reviewID);

                var result = context.Find<Customer>(customerID).Reviews.FirstOrDefault();

                // assert
                Assert.That(result is null);
            }
        }
    }
}
