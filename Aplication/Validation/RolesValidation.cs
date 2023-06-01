using Domain.Models.ModelsJwt;
using FluentValidation;

namespace Aplication.Validation
{
    public class RolesValidation : AbstractValidator<Roles>
    {
        public RolesValidation() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .MinimumLength(3)
                .WithMessage("Role is not valid");
        }
    }
}
