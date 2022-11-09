using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Api.Application.Commands;
using StarWars.Api.Domain.Models;
using StarWars.Api.Extensions;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Application.Handlers
{
    public class CreatePlanetCommandHandler: IRequestHandler<CreatePlanetCommand, Response>
    {
        private readonly IPlanetConnector _planetConnector;
        private readonly IFilmRepository _filmRepository;
        private readonly IFilmConnector _filmConnector;
        
        public CreatePlanetCommandHandler(IPlanetConnector planetConnector,
                                          IFilmConnector filmConnector,
                                          IFilmRepository filmRepository )
        {
            _planetConnector = planetConnector;
            _filmConnector = filmConnector;
            _filmRepository = filmRepository;
        }

        public async Task<Response> Handle(CreatePlanetCommand request, CancellationToken cancellationToken)
        {
            //consulta no banco se tem o planeta
            //sim? -> retorna ok
            
            //não? -> consulta planeta na api
            var planets = await _planetConnector.GetPlanet(request.Id);
            
            //pega os filmes
            
            //insere planeta
            //insere a relação de filmes 
            
            
            return Response.Ok();
        }

        private async ValueTask<IEnumerable<int>> GetFilms(IEnumerable<string> urlsFilms)
        {
            var films = new List<int>();
            foreach (var url in urlsFilms)
            {
                var urlSplited = url.Split("/");
                var id = int.Parse(urlSplited.LastOrDefault());

                await InsertFilm(id);
                films.Add(id);
            }
            return films;
        }

        private async ValueTask InsertFilm(int id)
        {
            var filmInDatabase = await _filmRepository.GetById(id);
            if (filmInDatabase is not null)
                return;

            var film = await _filmConnector.GetById(id);
            //insere o filme
            return;


        }
    }
}