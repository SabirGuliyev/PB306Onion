using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;
using OnionPronia.Persistence.Contexts;
using OnionPronia.Persistence.Implementations.Repositories;
using OnionPronia.Persistence.Implementations.Services;


namespace OnionPronia.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>  
            opt.UseSqlServer(config.GetConnectionString("default"))
            );


            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(3);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();


            return services;
        }
    }
}
