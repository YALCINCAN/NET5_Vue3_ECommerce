using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.DTOs;

namespace Business.ValidationRules.FluentValidation
{
    public class AddBrandValidator : AbstractValidator<BrandDTO>
    {
        public AddBrandValidator()
        {
            RuleFor(x => x.ImageFile).NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
    public class UpdateBrandValidator : AbstractValidator<BrandDTO>
    {
        public UpdateBrandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
