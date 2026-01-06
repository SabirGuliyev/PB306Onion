
using System.Data;
using FluentValidation;
using OnionPronia.Application.DTOs.Products;

namespace OnionPronia.Application.Validators
{
    public class PostProductDtoValidator:AbstractValidator<PostProductDto>
    {
        public PostProductDtoValidator()
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

            RuleFor(p => p.TagIds)  //{1 ,2 ,-70, 0}
                .NotEmpty()
                .Must(tgIds => tgIds.Count > 0);

            RuleForEach(p=>p.TagIds)  //{1 ,2 ,-70, 0}
                .GreaterThan(0);

            //RuleFor(p => p)
            //    .Must(Test); takes whole object as argument
                
                



        }

        //public bool Test(PostProductDto productDto)
        //{

        //    return true;
        //}

       
    }
}
