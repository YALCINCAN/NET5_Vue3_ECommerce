using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class ResetPasswordValidator:AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Token).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.NewPassword).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty();
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
