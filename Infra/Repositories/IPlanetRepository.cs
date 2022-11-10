using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Infra.Repositories
{
    public interface IPlanetRepository
    {
        ValueTask<Planet> GetById(int id);
        ValueTask Insert(Planet planet);
        ValueTask<IEnumerable<Planet>> GetAll();
        ValueTask<IEnumerable<Planet>> GetByName(string name);
        ValueTask Delete(int id);
    }   
}