using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Persistence.Contexts;
using OnionPronia.Persistence.Implementations.Repositories;
using OnionPronia.Persistence.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt => 

                    opt.UseSqlServer(config.GetConnectionString("default"))
            );

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();


            return services;
        }
    }
}
