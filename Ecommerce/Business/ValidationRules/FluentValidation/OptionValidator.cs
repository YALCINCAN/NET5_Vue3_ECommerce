using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.DTOs;

namespace Business.ValidationRules.FluentValidation
{
    public class AddOptionValidator : AbstractValidator<OptionDTO>
    {
        public AddOptionValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
    public class UpdateOptionValidator : AbstractValidator<OptionDTO>
    {
        public UpdateOptionValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
