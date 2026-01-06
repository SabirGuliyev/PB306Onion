using AutoMapper;
using OnionPronia.Application.DTOs.Products;
using OnionPronia.Application.DTOs.Tags;
using OnionPronia.Domain.Entities;


namespace OnionPronia.Application.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductInCategoryDto>();

            CreateMap<Product, GetProductItemDto>()
                .ForCtorParam(nameof(GetProductItemDto.CategoryName),
                opt => opt.MapFrom(p => p.Category.Name));

            CreateMap<Product, GetProductDto>()
                .ForCtorParam(nameof(GetProductDto.CategoryDto),
                opt => opt.MapFrom(p => p.Category))

                .ForCtorParam(nameof(GetProductDto.TagDtos),
                opt => opt.MapFrom(p => p.ProductTags
                    .Select(pt => new GetTagInProductDto(pt.Tag.Id, pt.Tag.Name))
                    .ToList()));

            CreateMap<PostProductDto, Product>()
                .ForMember(
                p => p.ProductTags,
                opt => opt.MapFrom(pDto => pDto.TagIds
                                                  .Select(tId => new ProductTag { TagId = tId })
                                   )
                );

          
        }
    }
}
