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
        public PlanetRepository(IOptions<ConnectionStringOption> connectionString) :
            base(connectionString)
        {
        }

        public async ValueTask<Planet> GetById(int id)
        {
            using var connection = CreateSqlServerConnection();
            return await connection.QueryFirstOrDefaultAsync<Planet>(PlanetStatements.GetById, new{id});
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
    }
}