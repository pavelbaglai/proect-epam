using Music_Store.Models;
using Music_Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.QueryObjects
{

    public static class ReviewSelect
    {
        public static IQueryable<ReviewViewModel> MapReviewToVM(this IQueryable<Review> reviews)
        {
            return reviews.Select(r => new ReviewViewModel
            {
                ID = r.ID,
                Rating = r.Rating,
                Content = r.Content,
                CustomerID = r.CustomerID,
                CustomerNickName = r.Customer.User.Nickname,
                CustomerImagePath = r.Customer.User.ImagePath
            });
        }
    }
}
