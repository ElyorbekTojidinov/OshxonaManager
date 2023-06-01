using Domain.Models.Models;
using FluentValidation;

namespace Aplication.Validation
{
    public class OrderValidation : AbstractValidator<Orders>
    {
        public OrderValidation() 
        {
            RuleFor(x => x.OrderTable)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage("Role is not valid");
           
        }

    }
}
