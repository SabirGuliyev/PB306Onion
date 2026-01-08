using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Application.DTOs.AppUsers
{
    public record LoginDto(string UsernameOrEmail, string Password );
    
}
