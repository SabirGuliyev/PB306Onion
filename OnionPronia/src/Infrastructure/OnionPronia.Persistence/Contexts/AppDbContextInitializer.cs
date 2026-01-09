using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionPronia.Domain.Entities;
using OnionPronia.Domain.Enums;

namespace OnionPronia.Persistence.Contexts
{
    internal class AppDbContextInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AppDbContextInitializer(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            IConfiguration configuration
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task InitializeAdmin()
        {
            bool result=await _userManager.Users.AnyAsync(u=>u.UserName==_configuration["AdminSettings:userName"] ||u.Email== _configuration["AdminSettings:email"]);


            if (!result)
            {
                AppUser user = new AppUser
                {

                    Name = "admin",
                    Surname = "admin",
                    UserName = _configuration["AdminSettings:userName"],
                    Email = _configuration["AdminSettings:email"],
                    EmailConfirmed = true

                };

                await _userManager.CreateAsync(user, _configuration["AdminSettings:password"]);

                await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            }


        }
        public async Task InitializeRolesAsync()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
                
            }
        }
    }
}
