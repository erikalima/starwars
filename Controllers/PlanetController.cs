using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Application.Commands;
using StarWars.Api.Application.Responses;
using StarWars.Api.Domain.Models;
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

        [HttpPost]
        [Route("create/{id}")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromRoute]int id)
        {
            var response = await _mediator.Send(new CreatePlanetCommand{Id = id});
            if (response.IsFailure)
                return BadRequest(response.Message);
            
            return Created("Planet:", $" {id}");
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Planet>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var planets = await _planetRepository.GetAll();
            if (!planets.Any())
                return NoContent();
            
            return Ok(planets.ToResponse());
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Planet), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var planet = await _planetRepository.GetById(id);
            
            if (planet is null)
                return BadRequest("Id do planeta inexistente.");
            
            return Ok(planet.ToResponse());
        }
        
        [HttpGet]
        [Route("name/{name}")]
        [ProducesResponseType(typeof(Planet), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByName([FromRoute]string name)
        {
            var planets = await _planetRepository.GetByName(name);
            
            if (!planets.Any())
                return NoContent();
            
            return Ok(planets.ToResponse());
        }
        
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _planetRepository.Delete(id);
            return Ok();
        }
    }
}