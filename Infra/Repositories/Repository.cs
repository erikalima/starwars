using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace StarWars.Api.Infra.Repositories
{
    public class Repository
    {
        protected ConnectionStringOption ConnectionStringOptions { get; }

        protected Repository(IOptions<ConnectionStringOption> connectionString)
        {
            ConnectionStringOptions = connectionString.Value;
        }
        
        protected IDbConnection CreateSqlServerConnection()
            => new SqlConnection(ConnectionStringOptions.SqlServerConnection);
    }
}