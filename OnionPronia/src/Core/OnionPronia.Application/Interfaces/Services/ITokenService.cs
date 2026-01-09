using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionPronia.Application.DTOs.Tokens;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Application.Interfaces.Services
{
    public interface ITokenService
    {
        TokenResponseDto CreateAccessToken(AppUser user, int minutes);
    }
}
