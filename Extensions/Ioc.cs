using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Extensions
{
    public static class RepositoriesContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddTransient<IPlanetRepository, PlanetRepository>();
        }
        
    }
}