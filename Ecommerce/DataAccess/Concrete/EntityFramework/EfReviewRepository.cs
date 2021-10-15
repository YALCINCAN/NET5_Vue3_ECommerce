
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using Entities.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReviewRepository : EfEntityRepositoryBase<Review, ECommerceContext>, IReviewRepository
    {
        public EfReviewRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ReviewDetail>> GetAdminReviews()
        {
            return await _context.Reviews.Select(review => new ReviewDetail()
            {
                Id = review.Id,
                Description = review.Description,
                ReviewDate = review.ReviewDate,
                RatingValue = review.RatingValue,
                User = new UserDetail()
                {
                    Id = review.User.Id,
                    FirstName = review.User.FirstName,
                    LastName = review.User.LastName
                }
            }).ToListAsync();
        }
    }
}
