using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Controllers
{
    [Route("planet")]
    [ApiController]
    public class PlanetController: ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        public PlanetController(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
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
    }
}