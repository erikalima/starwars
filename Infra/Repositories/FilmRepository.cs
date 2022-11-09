using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories.Statements;

namespace StarWars.Api.Infra.Repositories
{
    public class FilmRepository: Repository, IFilmRepository
    {
        protected FilmRepository(IOptions<ConnectionStringOption> connectionString) : 
            base(connectionString)
        {
        }
        
        public async ValueTask<IEnumerable<Film>> GetByIds(List<int> ids)
        {
            using var connection = CreateSqlServerConnection();
            
            return await connection.QueryAsync<Film>(FilmStatements.GetByIds, new {ids});
        }
        
        public async ValueTask<Film> GetById(int id)
        {
            using var connection = CreateSqlServerConnection();
            
            return await connection.QueryFirstOrDefaultAsync<Film>(FilmStatements.GetByIds, id);
        }
    }
}