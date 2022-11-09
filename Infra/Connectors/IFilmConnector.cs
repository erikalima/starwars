using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Infra.Connectors
{
    public interface IFilmConnector
    {
        ValueTask<Film> GetById(int id);
        ValueTask<IEnumerable<Film>> GetFilmsByUrls(IEnumerable<string> urlsFilms);
    }
}