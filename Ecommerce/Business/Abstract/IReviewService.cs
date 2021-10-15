
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReviewService
    {
        Task<IResponse> AddAsync(ReviewDTO model);
        Task<IResponse> GetAdminReviews();
        Task<IResponse> RemoveAsync(int id);
    }
}
