

using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideosVm>> //pide dos parametros , primero cual es el origen
                                                                                                 //el componente qie esta enviando el mensaje, segundo el resultado deseado
    {
        private readonly IVideoRepository _videoRepository; //inyectyar el metodo que nos devuelve la transaccion en este caso es la interfaz personalizada de video que creamos ultimo
        private readonly IMapper _mapper;                                        //necesitamos hjacer un mapeop porque esto retorna video pero el metodo una lista de videosVM

        public GetVideosListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideosVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        { //REQUEST ES UNA REFERENCIA DEL GETVIDEOlISTqUERY
            var videoList = await _videoRepository.GetVideoByUsername(request._Username);
            return _mapper.Map<List<VideosVm>>(videoList);  
        }
    }
}
