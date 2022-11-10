using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ClearExtensions;
using StarWars.Api.Application.Services;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories;
using Xunit;

namespace StarWars.Test.Application.Services
{
    public class FilmServiceTest
    {
        private IFilmRepository _filmRepository;
        private IFilmConnector _filmConnector;
        private FilmService _service;
        
        public FilmServiceTest()
        {
            _filmRepository = Substitute.For<IFilmRepository>();
            _filmConnector = Substitute.For<IFilmConnector>();
        
            _service = new FilmService(_filmRepository, _filmConnector);
        }

        [Fact]
        public async Task Should_Do_Nothing_When_Film_Existing_In_Database()
        {
            _filmRepository.GetById(Arg.Any<int>()).Returns(new Film());

            var requestService = new List<string>{"url/film/1", "url/film/2"};
            await _service.SaveFilm(requestService, 1);

            await _filmConnector.DidNotReceive().GetById(Arg.Any<int>());
            await _filmRepository.DidNotReceive().InsertFilm(Arg.Any<Film>());
            await _filmRepository.Received(2).GetById(Arg.Any<int>());
        }
        
        [Fact]
        public async Task Should_Save_Film_When_Dont_Existing_In_Database()
        {
            _filmRepository.GetById(Arg.Any<int>()).Returns(r => null);
            _filmConnector.GetById(Arg.Any<int>()).Returns(new Film());

            var requestService = new List<string>{"url/film/1", "url/film/2", "url/film/3"};
            await _service.SaveFilm(requestService, 1);

            await _filmConnector.Received(3).GetById(Arg.Any<int>());
            await _filmRepository.Received(3).InsertFilm(Arg.Any<Film>());
            await _filmRepository.Received(3).GetById(Arg.Any<int>());
        }
    }
}