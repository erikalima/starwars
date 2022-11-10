using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StarWars.Api.Infra.Repositories.Statements;
using Dapper;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Infra.Repositories
{
    public class PlanetRepository: Repository, IPlanetRepository
    {
        private readonly IFilmRepository _filmRepository;
        
        public PlanetRepository(IOptions<ConnectionStringOption> connectionString,
                                IFilmRepository filmRepository) :
            base(connectionString)
        {
            _filmRepository = filmRepository;
        }

        public async ValueTask Insert(Planet planet)
        {
            using var connection = CreateSqlServerConnection();

            var parameters = new
            {
                id = planet.Id,
                name = planet.Name,
                climate = planet.Climate,
                terrain = planet.Terrain
            };
            
            await connection.ExecuteAsync(PlanetStatements.Insert, parameters);
        }
        
        public async ValueTask<IEnumerable<Planet>> GetAll()
        {
            using var connection = CreateSqlServerConnection();
            
            var planets = await connection.QueryAsync<Planet>(PlanetStatements.GetAll);
            if (planets.Any())
            {
                var films = await _filmRepository.GetAll();
                return BuildPlanets(planets, films);
            }
            return null;
        }
        
        public async ValueTask<Planet> GetById(int id)
        {
            using var connection = CreateSqlServerConnection();
            var planet = await connection.QueryFirstOrDefaultAsync<Planet>(PlanetStatements.GetById, new{id});
            if(planet is not null)
                planet.Films = new List<Film>(await _filmRepository.GetByPlanetId(planet.Id));

            return planet;
        }

        public async ValueTask<IEnumerable<Planet>> GetByName(string name)
        {
            using var connection = CreateSqlServerConnection();
            
            var planets = await connection.QueryAsync<Planet>(PlanetStatements.GetByName, new{name = $"%{name}%"});
            if (planets.Any())
            {
                var films = await _filmRepository.GetAll();
                return BuildPlanets(planets, films);
            }
            return null;
        }

        public async ValueTask Delete(int id)
        {
            using var connection = CreateSqlServerConnection();
            await connection.ExecuteAsync(PlanetStatements.Delete, new {id});
        }

        private IEnumerable<Planet> BuildPlanets(IEnumerable<Planet> planets, IEnumerable<Film> films)
        {
            foreach (var planet in planets)
            {
                var filmsPlanet = new List<Film>();
                foreach (var film in films)
                {
                    if (film.PlanetId == planet.Id)
                    {
                        filmsPlanet.Add(film);
                    }
                }
                planet.Films = new List<Film>(filmsPlanet);
            }
            return planets;
        }
    }
}