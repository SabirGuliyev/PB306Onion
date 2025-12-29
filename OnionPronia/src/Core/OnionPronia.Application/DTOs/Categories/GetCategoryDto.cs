using OnionPronia.Application.DTOs.Products;

namespace OnionPronia.Application.DTOs
{
    //Data Transer Object
    public record GetCategoryDto (
            long Id,
            string Name,
            ICollection<GetProductInCategoryDto> ProductDtos
        );
   
}
