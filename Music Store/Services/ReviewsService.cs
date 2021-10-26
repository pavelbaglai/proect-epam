using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly ApplicationDbContext _context;

        public ReviewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsFromSongAsync(int songID, int startIndex, int reviewsCount)
        {
            return await _context.Songs
                .AsNoTracking()
                .Where(s => s.ID == songID)
                .SelectMany(s => s.Reviews)
                .OrderByDescending(r => r.WrittenDate)
                .Skip(startIndex)
                .Take(reviewsCount)
                .MapReviewToVM()
                .ToListAsync();
        }

        public async Task AddReviewToItemAsync(int customerID, ReviewViewModel reviewViewModel)
        {
            int? songID = null;
            int? albumID = null;
            switch(reviewViewModel.Category)
            {
                case nameof(Song):
                    songID = reviewViewModel.ItemID;
                    break;
                case nameof(Album):
                    albumID = reviewViewModel.ItemID;
                    break;
                default:
                    break;
            }

            var customer = await _context.Customers
                                    .Where(c => c.ID == customerID)
                                    .FirstOrDefaultAsync();
            customer.AddReview(songID, albumID, reviewViewModel.Rating, reviewViewModel.Content);
            
            await _context.SaveChangesAsync();
        }

        public async Task RemoveReviewFromItemAsync(int customerID, int reviewID)
        {
            var customer = _context.Customers.Where(c => c.ID == customerID);
            var review = await customer.SelectMany(c => c.Reviews)
                                .Where(r => r.ID == reviewID)
                                .FirstOrDefaultAsync();
            
            _context.Remove(review);
            
            await _context.SaveChangesAsync();
        }
    }
}
