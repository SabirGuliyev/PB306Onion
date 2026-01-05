

using OnionPronia.Application.DTOs.Categories;
using OnionPronia.Application.DTOs.Tags;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Application.DTOs.Products
{
    public record GetProductDto(
            long Id,
            string Name,
            decimal Price,
            string SKU,
            string Description,
            GetCategoryInProductDto CategoryDto,
            ICollection<GetTagInProductDto> TagDtos

        );
    
}
