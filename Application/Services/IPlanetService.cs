using System.Threading.Tasks;
using StarWars.Api.Application.Commands;

namespace StarWars.Api.Application.Services
{
    public interface IPlanetService
    {
        ValueTask SavePlanet(CreatePlanetCommand request);
    }
}