using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using OnionPronia.Application.Validators;
using FluentValidation.AspNetCore;

namespace OnionPronia.Application
{
    public static class ServiceRegistration
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
              
    

            return services;
        }
    }
}
