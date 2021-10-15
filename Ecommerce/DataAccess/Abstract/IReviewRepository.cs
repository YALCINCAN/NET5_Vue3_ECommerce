
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IReviewRepository : IEntityRepository<Review>
    {
        Task<IEnumerable<ReviewDetail>> GetAdminReviews();
    }
}
