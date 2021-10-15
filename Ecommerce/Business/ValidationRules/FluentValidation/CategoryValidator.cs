using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddCategoryValidator:AbstractValidator<CategoryDTO>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OptionIds).NotNull().NotEmpty();
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<CategoryDTO>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OptionIds).NotNull().NotEmpty();
        }
    }
}
