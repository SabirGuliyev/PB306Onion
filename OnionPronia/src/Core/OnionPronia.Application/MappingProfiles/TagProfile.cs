using AutoMapper;
using OnionPronia.Application.DTOs.Tags;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Application.MappingProfiles
{
    internal class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag,GetTagInProductDto>();
        }
    }
}
