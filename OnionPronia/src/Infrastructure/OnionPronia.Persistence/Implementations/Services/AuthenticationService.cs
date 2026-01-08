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
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Persistence.Implementations.Services
{
    internal class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task RegisterAsync(RegisterDto userDto)
        {
           IdentityResult result= await _userManager.CreateAsync(_mapper.Map<AppUser>(userDto), userDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder sb = new();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new Exception(sb.ToString());
            }

        }


        public async Task<string> LoginAsync(LoginDto userDto)
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

            IEnumerable<Claim> userClaims=new List<Claim> {
            
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.GivenName,user.Name)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"]));
            //AsymmetricSecurityKey
            SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires:DateTime.Now.AddMinutes(15),
                notBefore:DateTime.Now,
                claims:userClaims,
                signingCredentials:credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
           
            return handler.WriteToken(securityToken);



        }
    }
}
