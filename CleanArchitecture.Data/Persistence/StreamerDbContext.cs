using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {

        }
        

        
        
       

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer
        //        ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Streamer;Integrated Security=True")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        //        .EnableSensitiveDataLogging();//descrbiri cada una de las operaciones
        //}


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //se dsiaprara en el momento de actualizar o crear un nuevo record
        {
            //primero que recorra las entidades antes de realizar la insercion
            foreach(var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:  
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);    
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>() //la clase streamer tendra muchas instancias de videos, con un streamer
                    .HasMany(m => m.Videos)
                    .WithOne(m => m.Streamer)
                    .HasForeignKey(m => m.StreamerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            //Si seguimos las convenciones de ENTITYFRAMEWORK no es necesario pero si usamos campos que no sigen
            //las convenciones de entityframework core, ahi aplicamos esto para forzar la llave foranea



            modelBuilder.Entity<Video>() //relacion muchos a muchos
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );
        }
        public DbSet<Director>? Directores { get; set; }

        public DbSet<Actor>? Actor { get; set; }

        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }
    }
}
