using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using StarWars.Api.Application.Commands;
using StarWars.Api.Application.Handlers;
using StarWars.Api.Application.Services;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Repositories;
using Xunit;

namespace StarWars.Test.Application.Handlers
{
    public class CreatePlanetCommandHandlerTest
    {
        private IPlanetRepository _planetRepository;
        private IPlanetService _planetService;
        private CreatePlanetCommandHandler _handler;
        
        public CreatePlanetCommandHandlerTest()
        {
            _planetRepository = Substitute.For<IPlanetRepository>();
            _planetService = Substitute.For<IPlanetService>();

            _handler = new CreatePlanetCommandHandler(_planetRepository, _planetService);
        }

        [Fact]
        public async Task Should_Do_Nothing_When_Planet_Existing_In_Database()
        {
            _planetRepository.GetById(Arg.Any<int>()).Returns(new Planet());

            var command = new CreatePlanetCommand {Id = 1};
            var response = _handler.Handle(command, CancellationToken.None);

            response.Result.IsSuccess.Should().BeTrue();
            await _planetService.DidNotReceive().SavePlanet(Arg.Any<CreatePlanetCommand>());
        }
        
        [Fact]
        public async Task Should_Create_Planet_When_Dont_Existing_In_Database()
        {
            _planetRepository.GetById(Arg.Any<int>()).Returns(r => null);

            var command = new CreatePlanetCommand {Id = 1};
            var response = _handler.Handle(command, CancellationToken.None);

            response.Result.IsSuccess.Should().BeTrue();
            await _planetService.Received().SavePlanet(Arg.Any<CreatePlanetCommand>());
        }
    }
}