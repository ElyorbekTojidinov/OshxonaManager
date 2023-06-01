using Domain.Models.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Validation
{
    public class CategoryValidation : AbstractValidator<Categories>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.CategoryName)
               .NotEmpty()
               .NotNull()
               .MaximumLength(20)
               .MinimumLength(2)
               .WithMessage("Name is not valid");
        }
    }
}
