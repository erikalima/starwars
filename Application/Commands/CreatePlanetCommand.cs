using MediatR;
using StarWars.Api.Extensions;

namespace StarWars.Api.Application.Commands
{
    public class CreatePlanetCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}