using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.DTOs;

namespace Business.ValidationRules.FluentValidation
{
    public class AddAddressValidator : AbstractValidator<AddressDTO>
    {
        public AddAddressValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
    public class UpdateAddressValidator : AbstractValidator<AddressDTO>
    {
        public UpdateAddressValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
