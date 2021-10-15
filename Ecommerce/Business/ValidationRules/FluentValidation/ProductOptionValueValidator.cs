using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddProductOptionValueValidator:AbstractValidator<ProductOptionValueDTO>
    {
        public AddProductOptionValueValidator()
        {
            RuleFor(x => x.OptionId).NotEmpty();
            RuleFor(x => x.OptionValueId).NotEmpty();
        }
    }

    public class UpdateProductOptionValueValidator : AbstractValidator<ProductOptionValueDTO>
    {
        public UpdateProductOptionValueValidator()
        {
            RuleFor(x => x.OptionId).NotEmpty();
            RuleFor(x => x.OptionValueId).NotEmpty();
        }
    }
}
