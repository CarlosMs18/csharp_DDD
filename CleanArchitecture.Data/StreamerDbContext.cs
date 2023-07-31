

using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Streamer;Integrated Security=True")
                .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name},LogLevel.Information)
                .EnableSensitiveDataLogging();//descrbiri cada una de las operaciones
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
                    pt=> pt.HasKey( e => new {e.ActorId,e.VideoId}) 
                );
        }


        public DbSet<Streamer>? Streamers { get; set; }  

        public DbSet<Video>? Videos { get; set; }    
    }
}
