using Business.Abstract;
using Core.ActionFilters;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IResponse> GetReviews()
        {
            return await _reviewService.GetAdminReviews();
        }

        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddReview(ReviewDTO model)
        {
            return await _reviewService.AddAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveReview(int id)
        {
            return await _reviewService.RemoveAsync(id);
        }
    }
}
