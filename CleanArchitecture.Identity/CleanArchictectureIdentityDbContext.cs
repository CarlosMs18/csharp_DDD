using CleanArchitecture.Identity.Configurations;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity
{
    public class CleanArchictectureIdentityDbContext : IdentityDbContext<ApplicationUser>
    //ponemos el generico para gregarle datos adicionalkes a los que me trae indetity por defecto
    {
        public CleanArchictectureIdentityDbContext(DbContextOptions<CleanArchictectureIdentityDbContext> options) : base(options)
        //DbContextOptions<CleanArchictectureIdentityDbContext -> hacemos esto pq queremos que automaticamente la cadena de conexion se inyecte al constructor
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
