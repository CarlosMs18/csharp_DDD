using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "1a070c89-2ddc-47f5-8a88-949ec6d5065f",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRADOR"
                },

                 new IdentityRole
                 {
                     Id = "1a070c89-2ddc-47f5-8a88-949ec6d5065f",
                     Name = "Operator",
                     NormalizedName = "OPERATOR"
                 }
                );
        }
    }
}
