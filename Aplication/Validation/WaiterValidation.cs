using Domain.Models.Models;
using FluentValidation;

namespace Aplication.Validation
{
    public class WaiterValidation : AbstractValidator<Waiter>
    {
        public WaiterValidation()
        {
            RuleFor(x => x.WaiterName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(20)
                .WithMessage("Name is not vaid");
            RuleFor(x => x.WaiterPhone)
                .Matches(@"^\+998\d{9}$")
                .WithMessage("PhoneNumbers is not valid");
        }
    }
}
