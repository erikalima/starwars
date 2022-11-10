using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Infra.Repositories
{
    public interface IFilmRepository
    {
        ValueTask<Film> GetById(int id);
        ValueTask InsertFilm(Film film);
        ValueTask InsertFilmsForPlanet(int planetId, IEnumerable<int> filmsId);
    }
}