using System.Collections.Generic;
using System.Net;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Api.Application.Services;
using StarWars.Api.Infra;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Extensions
{
    public static class RepositoriesContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IPlanetRepository, PlanetRepository>()
                .AddTransient<IFilmRepository, FilmRepository>();
        }
        
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ConnectionStringOption>(option =>
                    configuration.GetSection("ConnectionStrings").Bind(option));
        }
        
        public static IServiceCollection AddConnectors(this IServiceCollection services)
        {
            return services
                .AddTransient<IPlanetConnector, PlanetConnector>()
                .AddTransient<IFilmConnector, FilmConnector>();
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IPlanetService, PlanetService>()
                .AddTransient<IFilmService, FilmService>();
        }
        
        public static IServiceCollection RegisterHandlers(this IServiceCollection services)
        {
            return 
                services.AddMediatR(typeof(Response).Assembly);
        }
    }
}