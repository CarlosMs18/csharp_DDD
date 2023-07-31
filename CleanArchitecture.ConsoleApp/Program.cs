using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

await MultipeEntitiesQuery();

//await addNewDirectorWithVideo();
//await AddNewStreamerWithVideo();

//await TrackingAndNotTracking();
//await QueryLinq();

//await QueryMethods();
//await QueryFilter();

//await AddNewRecords();
//QueryStreaming();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();



async Task MultipeEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!.Include(q => q.Actores).FirstOrDefaultAsync( q => q.Id == 1);

    //var actor = await dbContext!.Actores.Select(q => q.Nombre).toListAsync();

    var videoWithDirector = await dbContext!.Videos!
                            .Where(q => q.Director != null)
                            .Include(q => q.Director)
                            .Select(q => new
                            {
                                Director_Nombre_Completo = $"{q.Director.Nombre} {q.Director.Apellido}",
                                Movie = q.Nombre
                                
                            }).ToListAsync();
    //PORDEFECTO EL SELECT SOLO DEVUELVE UN VALOR CON UNA COLUMNA SI QUEREMOS MAS DEBEMOS DE HACER UNA PROYECCION
    //EL q del select represtara a videos y dentro de este director porqu en esta estamos trabajando


    foreach(var pelicula in videoWithDirector)
    {
        Console.WriteLine($"{pelicula.Movie}  -{pelicula.Director_Nombre_Completo}");
    }

}

async Task addNewDirectorWithVideo() //insertando valor de 1 a 1
{
    var director = new Director()
    {
        Nombre = "Carlos",
        Apellido = "Melgarejo",
        VideoId = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer()
    {
        Nombre = "Pantaya"
    };

    var hungerGames = new Video()
    {
        Nombre = "Pantaya",
        Streamer = pantaya,
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();
}
async Task TrackingAndNotTracking()
{
    var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
    var streamerWithtNotracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
    //no se peude usar ek metodo notTracking cuando se utiliza el findAsync,
    //el notTracking aca no actualziara lo que hace es una vez realizado el primer query quedara libre de la memoria temmporal, se recominda
    //usar el at not trackinfgpara iberar recursos cuando no se necesita actualziar ni eliminar los datos resultado

    streamerWithTracking.Nombre = "Netflix Super";
    streamerWithtNotracking.Nombre = "Amazon Plus";

    await dbContext!.SaveChangesAsync();
}

async Task QueryLinq() //similinar  alas cosultar SQL
{

    Console.WriteLine($"Ingrese el servicio del streaming");

    var streamerNombre = Console.ReadLine();

    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();
    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");    
    }
}

async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;

    var streamers1 = streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();//el primero asume que existe la data y obtendra el primer record, en caso de que no exista disparara una excepcion

    var streamers2 = streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();//devuelve un valor n ull si no encuentra y no detiene la ejecucion del programa
    var streamers3 = streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    var streamer4 = streamer.SingleAsync(); //sila entidad donde se esta buascando es una coleccion boara un error tien que ser solo un unico valor a diferencia de los demas
                                    //si no encuentra tambien bota un error
}


async Task QueryFilter()
{
    Console.WriteLine("Ingrese una companeia de streaming");
    var streamingNombre = Console.ReadLine();
    //var streamers = await dbContext!.Streamers!.Where(x => x.Nombre == "Netflix").ToListAsync(); 
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre.Equals(streamingNombre)).ToListAsync(); //EQUALS EXACTAMERNTE IGUAL
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
    //var streamerPartialResults = await dbContext!.Streamers!.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync(); //parte de la cadena esta contenida

    var streamerPartialResults = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre,$"%{streamingNombre}%")).ToListAsync();
    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }


}


async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer); //! debemos indicarle que esta instanciado que el objeto existe

    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Nombre = "La cenicienta",
        StreamerId = streamer.Id

    },
      new Video
    {
        Nombre = "101 Dalmatas",
        StreamerId = streamer.Id

    },
        new Video
    {
        Nombre = "Bella y la bestia",
        StreamerId = streamer.Id

    },
          new Video
    {
        Nombre = "Blanca Nieves",
        StreamerId = streamer.Id

    }
};

    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();

}