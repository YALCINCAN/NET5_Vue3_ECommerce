using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddReviewValidator:AbstractValidator<ReviewDTO>
    {
        public AddReviewValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.RatingValue).NotEmpty();
        }
    }
}
