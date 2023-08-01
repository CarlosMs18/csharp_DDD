using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommand : IRequest<int> //en este caso queremos que nos devuielva el id del record que acaba de crearse

    {
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;


    }
}
