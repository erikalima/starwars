using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Application.Commands;
using StarWars.Api.Application.Handlers;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Controllers
{
    [Route("planet")]
    [ApiController]
    public class PlanetController: ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IMediator _mediator;

        public PlanetController(IPlanetRepository planetRepository,
                                IMediator mediator)
        {
            _planetRepository = planetRepository;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get()
        {
            await _planetRepository.GetPlanet();
            return Ok();
        }
        
        [HttpPost]
        [Route("create/{id}")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromRoute]int id)
        {
            var response = await _mediator.Send(new CreatePlanetCommand{Id = id});
            if (response.IsFailure)
                return BadRequest(response.Message);
            
            return Created("Planet:", $" {id}");
        }
    }
}