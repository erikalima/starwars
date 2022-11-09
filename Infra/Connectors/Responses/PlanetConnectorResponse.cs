using System.Collections.Generic;

namespace StarWars.Api.Infra.Connectors.Responses
{
    public class PlanetConnectorResponse
    {
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public List<string> Films { get; set; }
    }
}