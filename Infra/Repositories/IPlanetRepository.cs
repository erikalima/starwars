using System.Threading.Tasks;

namespace StarWars.Api.Infra.Repositories
{
    public interface IPlanetRepository
    {
        ValueTask GetPlanet();
    }
}