using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using StarWars.Api.Application.Commands;
using StarWars.Api.Application.Services;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Connectors.Responses;
using StarWars.Api.Infra.Repositories;
using Xunit;

namespace StarWars.Test.Application.Services
{
    public class PlanetServiceTest
    { 
        private IPlanetRepository _planetRepository;
        private IPlanetConnector _planetConnector;
        private IFilmService _filmService;

        private PlanetService _service;
                
        public PlanetServiceTest()
        {
            _planetRepository = Substitute.For<IPlanetRepository>();
            _planetConnector = Substitute.For<IPlanetConnector>();
            _filmService = Substitute.For<IFilmService>();
            
            _service = new PlanetService(_planetRepository, _planetConnector, _filmService);
        }

        [Fact]
        public async Task Should_Save_Planet()
        {
            _planetConnector.GetPlanet(Arg.Any<int>()).Returns(new PlanetConnectorResponse());

            await _service.SavePlanet(new CreatePlanetCommand());
            
            await _planetRepository.Received().Insert(Arg.Any<Planet>());
            await _filmService.Received().SaveFilm(Arg.Any<IEnumerable<string>>(), Arg.Any<int>());
        }
        
        [Fact]
        public async Task Should_Do_Nothing_When_Planet_Not_Found_In_Public_Api()
        {
            _planetConnector.GetPlanet(Arg.Any<int>()).Returns(r => null);

            await _service.SavePlanet(new CreatePlanetCommand());
            
            await _planetRepository.DidNotReceive().Insert(Arg.Any<Planet>());
            await _filmService.DidNotReceive().SaveFilm(Arg.Any<IEnumerable<string>>(), Arg.Any<int>());
        }
    }
}