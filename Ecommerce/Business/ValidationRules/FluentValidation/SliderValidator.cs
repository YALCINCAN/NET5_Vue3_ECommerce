using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.DTOs;

namespace Business.ValidationRules.FluentValidation
{
    public class AddSliderValidator:AbstractValidator<SliderDTO>
    {
        public AddSliderValidator()
        {
            RuleFor(x => x.ImageFile).NotNull();
            RuleFor(x => x.Active).NotNull();
        }
    }

    public class UpdateSliderValidator : AbstractValidator<SliderDTO>
    {
        public UpdateSliderValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Active).NotNull();
        }
    }
}
