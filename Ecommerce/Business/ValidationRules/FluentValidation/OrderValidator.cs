using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddOrderValidator:AbstractValidator<OrderDTO>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.Cvc).NotEmpty();
            RuleFor(x => x.CardNumber).NotEmpty();
            RuleFor(x => x.CardName).NotEmpty();
            RuleFor(x => x.ExpirationMonth).NotEmpty();
            RuleFor(x => x.ExpirationYear).NotEmpty();
            RuleFor(x => x.AddressId).NotEmpty();
        }
    }

    public class UpdateOrderValidator : AbstractValidator<AdminOrderDetailDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.OrderStatusId).NotEmpty();
            
        }
    }
}
