using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionPronia.Domain.Entities;

namespace OnionPronia.Persistence.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u=>u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(50);


        }
    }
}
