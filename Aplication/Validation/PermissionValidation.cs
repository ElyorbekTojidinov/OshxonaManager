using Domain.Models.ModelsJwt;
using FluentValidation;

namespace Aplication.Validation
{
    public class PermissionValidation : AbstractValidator<Permission>
    {
        public PermissionValidation()
        {
            RuleFor(x => x.PermissionName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .MinimumLength(5)
                .WithMessage("Username is not valid");
           
        }

    }
}
