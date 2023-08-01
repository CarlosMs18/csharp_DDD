using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<VideosVm>>  //tenemos que informarle que tipo de data devolvera
    {
        public string? _Username { get; set; } = String.Empty;

        public GetVideosListQuery(string username)
        {
            _Username = username ?? throw new ArgumentNullException(nameof(username));//en caso no esta lanzo la excepcion
        }
    }
}
