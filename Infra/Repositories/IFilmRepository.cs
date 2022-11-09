using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Infra.Repositories
{
    public interface IFilmRepository
    {
        ValueTask<Film> GetById(int id);

        ValueTask<IEnumerable<Film>> GetByIds(List<int> ids);
    }
}