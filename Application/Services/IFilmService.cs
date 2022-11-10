using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Api.Application.Services
{
    public interface IFilmService
    {
        ValueTask SaveFilm(IEnumerable<string> urlsFilms, int planetId);
    }
}