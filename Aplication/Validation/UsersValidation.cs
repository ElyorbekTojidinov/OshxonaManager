using Domain.Models.ModelsJwt;
using FluentValidation;

namespace Aplication.Validation
{
    public class UsersValidation : AbstractValidator<Users>
    {
        public UsersValidation()
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .MinimumLength(5)
            .WithMessage("Username is not valid");
            
            RuleFor(x => x.Password)
              .NotEmpty()
              .NotNull()
              .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
              .WithMessage("Password is not valid")
              .MinimumLength(6)
              .WithMessage("Password is not valid");
        }
    }
}
