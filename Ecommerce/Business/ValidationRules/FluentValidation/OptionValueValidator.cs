using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.DTOs;

namespace Business.ValidationRules.FluentValidation
{
    public class AddOptionValueValidator : AbstractValidator<OptionValueDTO>
    {
        public AddOptionValueValidator()
        {
            RuleFor(x => x.OptionId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
    public class UpdateOptionValueValidator : AbstractValidator<OptionValueDTO>
    {
        public UpdateOptionValueValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.OptionId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
