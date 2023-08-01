using CleanArchitecture.Domain;


namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository: IAsyncRepository<Video> 
    {
        Task<Video> GetVideoByNombre(string nombreVideo); //BUSQUEDA DE VIDEO POR TITULO AL SER UNO PERSNALIZADA QUE HEREDE DE LA INTERFAZ GENERICA

        Task<IEnumerable<Video>> GetVideoByUsername(string username);
    }
}
