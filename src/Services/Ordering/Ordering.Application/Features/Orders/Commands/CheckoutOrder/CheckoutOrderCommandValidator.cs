using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(q => q.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .NotNull()
                .MaximumLength(50).WithMessage("UserName required from 3 to 50 symbols")
                .MinimumLength(3).WithMessage("UserName required from 3 to 50 symbols");

            RuleFor(q => q.EmailAddress)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(q => q.TotalPrice)
                .NotEmpty().WithMessage("TotalPrice is required")
                .GreaterThan(0).WithMessage("TotalPrice must be greater that zero");
        }
        
    }
}
