using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StarWars.Api.Application.Commands;
using StarWars.Api.Application.Services;
using StarWars.Api.Extensions;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Application.Handlers
{
    public class CreatePlanetCommandHandler: IRequestHandler<CreatePlanetCommand, Response>
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IPlanetService _planetService;
        
        public CreatePlanetCommandHandler(IPlanetRepository planetRepository,
                                          IPlanetService planetService)
        {
            _planetRepository = planetRepository;
            _planetService = planetService;
        }

        public async Task<Response> Handle(CreatePlanetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var planetInDataBase = await _planetRepository.GetById(request.Id);
                if(planetInDataBase is not null)
                    return Response.Ok();

                await _planetService.SavePlanet(request);

                return Response.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
 
        }
    }
}