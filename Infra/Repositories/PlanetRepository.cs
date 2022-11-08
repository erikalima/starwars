using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StarWars.Api.Infra.Repositories.Statements;
using Dapper;

namespace StarWars.Api.Infra.Repositories
{
    public class PlanetRepository: Repository, IPlanetRepository
    {
        public PlanetRepository(IOptions<ConnectionStringOption> connectionString) :
            base(connectionString)
        {
        }

        public async ValueTask GetPlanet()
        {
            using var connection = CreateSqlServerConnection();

            var customerInfo = await connection.QueryAsync(PlanetStatements.GetPlanet);
        }
    }
}