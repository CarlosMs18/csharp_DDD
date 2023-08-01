using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if(!context.Streamers!.Any())  //si no tiene datos en su interior
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
               await  context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al DDBB {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer {CreatedBy = "vaxidrez", Nombre = "maxi HBP", Url = "http://www.jp.com"},
                new Streamer {CreatedBy = "vaxidrez2", Nombre = "maxi HBP2", Url = "http://www.jasdasp.com"}
            };
        }
    }
}
