using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionPronia.Application.DTOs.AppUsers;
using OnionPronia.Application.DTOs.Tokens;

namespace OnionPronia.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto userDto);
        Task<TokenResponseDto> LoginAsync(LoginDto userDto);

    }
}
