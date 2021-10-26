using Music_Store.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface IReviewsService
    {
        Task<IEnumerable<ReviewViewModel>> GetReviewsFromSongAsync(int songID, int startIndex, int reviewsCount);
        Task AddReviewToItemAsync(int customerID, ReviewViewModel reviewViewModel);
        Task RemoveReviewFromItemAsync(int customerID, int reviewID);
    }
}