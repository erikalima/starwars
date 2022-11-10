using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories.Statements;

namespace StarWars.Api.Infra.Repositories
{
    public class FilmRepository: Repository, IFilmRepository
    {
        public FilmRepository(IOptions<ConnectionStringOption> connectionString) : 
            base(connectionString)
        {
        }

        public async ValueTask<Film> GetById(int id)
        {
            using var connection = CreateSqlServerConnection();
            
            return await connection.QueryFirstOrDefaultAsync<Film>(FilmStatements.GetByIds, new {ids = id});
        }
        
        public async ValueTask InsertFilm(Film film)
        {
            using var connection = CreateSqlServerConnection();

            var parameters = new
            {
                id = film.Id,
                title = film.Title,
                director = film.Director,
                releaseDate = film.ReleaseDate
            };
            await connection.ExecuteAsync(FilmStatements.Insert, parameters);
        }
        
        public async ValueTask InsertFilmsForPlanet(int planetId, IEnumerable<int> filmsId)
        {
            using var connection = CreateSqlServerConnection();

            var filmsArray = filmsId.ToArray();
            for (int i = 0; i < filmsId.Count(); i++)
            {
                await connection.ExecuteAsync(FilmStatements.InsertFilmsForPlanet, new {planetId, filmId = filmsArray[i]});
            }
        }
    }
}