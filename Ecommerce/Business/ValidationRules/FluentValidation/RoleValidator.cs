using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddRoleValidator:AbstractValidator<RoleDTO>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class UpdateRoleValidator : AbstractValidator<RoleDTO>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
