using CleanArchitecture.Identity.Models;
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
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

           var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "f3ee6018-bb98-4333-9458-39fb2a9f31be",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombre = "carlos",
                    Apellidos = "tommy",
                    UserName = "vaxidrez",
                    NormalizedUserName = "vaxidrez",
                    PasswordHash = hasher.HashPassword(null, "Vaxidez2025$"),
                    EmailConfirmed = true
                },
                 new ApplicationUser
                 {
                     Id = "8c3164d4-ba00-45f9-b2de-772beef75c73",
                     Email = "admin@localhost.com",
                     NormalizedEmail = "admin@localhost.com",
                     Nombre = "carlos",
                     Apellidos = "tommy",
                     UserName = "vaxidrez",
                     NormalizedUserName = "vaxidrez",
                     PasswordHash = hasher.HashPassword(null, "Vaxidez2025$"),
                     EmailConfirmed = true
                 },
                  new ApplicationUser
                  {
                      Id = "8c3164d4-ba00-45f9-b2de-772beef75c73",
                      Email = "admin@localhost.com",
                      NormalizedEmail = "admin@localhost.com",
                      Nombre = "carlos",
                      Apellidos = "tommy",
                      UserName = "vaxidrez",
                      NormalizedUserName = "vaxidrez",
                      PasswordHash = hasher.HashPassword(null, "Vaxidez2025$"),
                      EmailConfirmed = true
                  }
                );
        }
    }
}
