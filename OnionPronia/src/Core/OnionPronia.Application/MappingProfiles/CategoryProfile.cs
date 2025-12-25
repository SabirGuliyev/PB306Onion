using AutoMapper;
using OnionPronia.Application.DTOs;
using OnionPronia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Application.MappingProfiles
{
    internal class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            //CreateMap<Category, GetCategoryItemDto>().ReverseMap();

            CreateMap<Category, GetCategoryItemDto>()
                .ForCtorParam(
                nameof(GetCategoryItemDto.ProductCount),
                opt => opt.MapFrom(c=>c.Products.Count));



                //.ForMember(
                //c=>c.ProductCount,
                //opt=>opt.MapFrom(c=>c.Products.Count)
                //);


            CreateMap<Category, GetCategoryDto>()
                .ForCtorParam(
                nameof(GetCategoryDto.ProductDtos),
                opt=>opt.MapFrom(c=>c.Products));

            CreateMap<PostCategoryDto, Category>();

            CreateMap<PutCategoryDto, Category>();

        }
    }
}
