using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OnionPronia.Application.DTOs.Products;

namespace OnionPronia.Application.Validators
{
    public class PutProductDtoValidator:AbstractValidator<PutProductDto>
    {
        public PutProductDtoValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(150);

            RuleFor(p => p.Description)
                .NotEmpty();

            RuleFor(p => p.SKU)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThan(0m)
                .LessThanOrEqualTo(999999.99m);

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.TagIds) 
                .NotEmpty()
                .Must(tgIds => tgIds.Count > 0);

            RuleForEach(p => p.TagIds) 
                .GreaterThan(0);

        }
    }
}
