using System.Collections.Generic;
using System.Linq;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Application.Responses
{
    public class PlanetResponse
    {
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public List<FilmResponse> Films { get; set; }
    }

    public static class PlanetResponseAdapter
    {
        public static PlanetResponse ToResponse(this Planet planet)
        {
            return new PlanetResponse
            {
                Name = planet.Name,
                Climate = planet.Climate,
                Terrain = planet.Terrain,
                Films = planet.Films.ToResponse().ToList()
            };
        }
        
        public static IEnumerable<PlanetResponse> ToResponse(this IEnumerable<Planet> planets)
        {
            var planetsResponse = new List<PlanetResponse>();
            foreach (var planet in planets)
            {
                planetsResponse.Add(ToResponse(planet));
            }

            return planetsResponse;
        }
    }
}