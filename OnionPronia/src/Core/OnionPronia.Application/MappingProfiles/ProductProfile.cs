using AutoMapper;
using OnionPronia.Application.DTOs;
using OnionPronia.Domain.Entities;


namespace OnionPronia.Application.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductInCategoryDto>();
            //CreateMap<>
        }
    }
}
