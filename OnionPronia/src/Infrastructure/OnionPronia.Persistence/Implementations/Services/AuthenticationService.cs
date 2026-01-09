using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionPronia.Application.DTOs.AppUsers;
using OnionPronia.Application.DTOs.Tokens;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;
using OnionPronia.Domain.Enums;

namespace OnionPronia.Persistence.Implementations.Services
{
    internal class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public async Task RegisterAsync(RegisterDto userDto)
        {
            AppUser user= _mapper.Map<AppUser>(userDto);
           IdentityResult result= await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder sb = new();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new Exception(sb.ToString());
            }

            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());

        }


        public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
        {
           AppUser user= await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UsernameOrEmail || u.Email == userDto.UsernameOrEmail);

            if (user == null) {

                throw new Exception("Username, Email or Password is invalid");
     
            }
        
            bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result) {

                await _userManager.AccessFailedAsync(user);
                throw new Exception("Username, Email or Password is invalid");

            }

            var roles=await _userManager.GetRolesAsync(user);
            return _tokenService.CreateAccessToken(user,roles, 15);

        }
    }
}
