namespace OnionPronia.Application.DTOs
{
    //Data Transer Object
    public record GetCategoryDto (
            int Id,
            string Name,
            IEnumerable<GetProductInCategoryDto> ProductDtos
        );
   
}
