using Domain.Models.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Validation
{
    public class ProductValidation : AbstractValidator<Products>
    {
        public ProductValidation()
        {
            RuleFor(x=>x.ProductPrice)
                .NotEmpty()
                .NotNull()
                .WithMessage("Price is not valid");
            RuleFor(x => x.ProductName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Name is not valid");
            RuleFor(x => x.ProductImg)
                .NotNull()
                .NotEmpty()
                .WithMessage("Pictures is not valid");
            RuleFor(x => x.CategoriesId)
                .NotEmpty()
                .NotNull()
                .WithMessage("CategoryId is not valid");
        }
    }
}
