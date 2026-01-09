using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionPronia.Application.DTOs.Tokens;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Infrastructure.Implementations.Services
{
    internal class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponseDto CreateAccessToken(AppUser user,int minutes)
        {

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.UtcNow.AddMinutes(minutes),
                notBefore: DateTime.UtcNow,
                claims: new List<Claim> {

                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.GivenName,user.Name)
                },
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"])), 
                    SecurityAlgorithms.HmacSha256
                    )
                );

            return new TokenResponseDto(
                new JwtSecurityTokenHandler().WriteToken(securityToken), 
                user.UserName,
                securityToken.ValidTo);
        }




    


    }
}







//public TokenResponseDto CreateAccessToken(AppUser user, int minutes)
//{
//    IEnumerable<Claim> userClaims = new List<Claim> {

//                new Claim(ClaimTypes.NameIdentifier,user.Id),
//                new Claim(ClaimTypes.Name,user.UserName),
//                new Claim(ClaimTypes.Email,user.Email),
//                new Claim(ClaimTypes.Surname,user.Surname),
//                new Claim(ClaimTypes.GivenName,user.Name)
//            };

//    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"]));
//    //AsymmetricSecurityKey
//    SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//    JwtSecurityToken securityToken = new JwtSecurityToken(
//        issuer: _configuration["JWT:issuer"],
//        audience: _configuration["JWT:audience"],
//        expires: DateTime.Now.AddMinutes(minutes),
//        notBefore: DateTime.Now,
//        claims: userClaims,
//        signingCredentials: credentials
//        );

//    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

//    string token = handler.WriteToken(securityToken);

//    return new TokenResponseDto(token, user.UserName, securityToken.ValidTo);
//}
