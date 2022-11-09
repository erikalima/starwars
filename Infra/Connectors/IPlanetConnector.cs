using System.Threading.Tasks;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors.Responses;

namespace StarWars.Api.Infra.Connectors
{
    public interface IPlanetConnector
    {
        ValueTask<PlanetConnectorResponse> GetPlanet(int id);
    }
}