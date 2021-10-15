
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using System.Security.Claims;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;

namespace Business.Concrete
{
    public class ReviewManager : IReviewService
    {
        private IReviewRepository _reviewRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;
        public ReviewManager(IReviewRepository reviewRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(AddReviewValidator))]
        public async Task<IResponse> AddAsync(ReviewDTO model)
        {
            var review = _mapper.Map<Review>(model);
            review.ReviewDate = DateTime.Now;
            review.UserId = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            await _reviewRepository.AddAsync(review);
            return new SuccessResponse(200, Messages.AddedSuccesfully);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            await _reviewRepository.RemoveAsync(review);
            return new SuccessResponse(200, Messages.DeletedSuccessfully);
        }

        public async Task<IResponse> GetAdminReviews()
        {
            var adminreviews = await _reviewRepository.GetAdminReviews();
            return new DataResponse<IEnumerable<ReviewDetail>>(adminreviews, 200);
        }
    }
}