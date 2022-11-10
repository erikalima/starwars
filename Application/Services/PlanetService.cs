using System.Threading.Tasks;
using StarWars.Api.Application.Commands;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Connectors.Responses;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Application.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IPlanetConnector _planetConnector;
        private readonly IFilmService _filmeService;

        public PlanetService(IPlanetRepository planetRepository,
                             IPlanetConnector planetConnector,
                             IFilmService filmeService)
        {
            _planetRepository = planetRepository;
            _planetConnector = planetConnector;
            _filmeService = filmeService;
        }

        public async ValueTask SavePlanet(CreatePlanetCommand request)
        {
            var planet = await _planetConnector.GetPlanet(request.Id);
            await SaveInternalPlanet(planet, request.Id);
            
            if(planet is not null)
                await _filmeService.SaveFilm(planet.Films, request.Id);
        }
        
        private async ValueTask SaveInternalPlanet(PlanetConnectorResponse planet, int id)
        {
            if (planet is null)
            {
                //log warning de planeta não localizado na consulta da api publica
            }

            var planetDomain = PlanetDomain(planet, id);
            await _planetRepository.Insert(planetDomain);
        }
        
        private Planet PlanetDomain(PlanetConnectorResponse planetConnectorResponse, int id)
        {
            return new Planet
            {
                Id = id,
                Name = planetConnectorResponse.Name,
                Climate = planetConnectorResponse.Climate,
                Terrain = planetConnectorResponse.Terrain
            };
        }
    }
}