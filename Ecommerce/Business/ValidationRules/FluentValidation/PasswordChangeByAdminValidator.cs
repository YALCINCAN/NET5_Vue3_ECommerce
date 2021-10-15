using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PasswordChangeByAdminValidator : AbstractValidator<PasswordChangeByAdminDTO>
    {
        public PasswordChangeByAdminValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.NewPassword != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.NewPassword), "Passwords should match");
                }
            });
        }
    }
}
