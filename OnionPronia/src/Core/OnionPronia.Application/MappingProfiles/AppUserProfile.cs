using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnionPronia.Application.DTOs.AppUsers;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Application.MappingProfiles
{
    internal class AppUserProfile:Profile    {

        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            //CreateMap<RegisterDto, AppUser>().ForMember(u=>u.Name,opt=>opt.MapFrom(r=>r.Name.ToUpper()));
        }
    }
}
