using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;
using StarWars.Api.Extensions;
using StarWars.Api.Infra.Connectors.Responses;

namespace StarWars.Api.Infra.Connectors
{
    public class PlanetConnector : IPlanetConnector
    {
        private readonly HttpClient _client;

        public PlanetConnector(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async ValueTask<PlanetConnectorResponse> GetPlanet(int id)
        {
            var endpoint = $"https://swapi.dev/api/people/{id}/";

            try
            {
                var response = await _client.GetFromJsonAsync<PlanetConnectorResponse>(endpoint);
                if (response is null)
                {
                    //log
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Erro ao buscar calendario no serviço {Endpoint} : {Messages}", endpoint, ex.Message);
                throw;
            }
        }
    }
}