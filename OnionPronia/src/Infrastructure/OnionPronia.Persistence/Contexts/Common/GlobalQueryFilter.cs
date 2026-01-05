using Microsoft.EntityFrameworkCore;
using OnionPronia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Persistence.Contexts.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyAllQueryFilters(this ModelBuilder builder)
        {
            builder.ApplyQueryFilter<Category>();
            builder.ApplyQueryFilter<Product>();
            builder.ApplyQueryFilter<Tag>();


        }
        private static void ApplyQueryFilter<T>(this ModelBuilder builder) where T:BaseEntity,new()
        {
            builder.Entity<T>().HasQueryFilter(x => x.IsDeleted == false);
        }





    }
}
