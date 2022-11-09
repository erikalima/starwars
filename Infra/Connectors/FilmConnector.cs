using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StarWars.Api.Domain.Models;
using StarWars.Api.Infra.Connectors.Responses;

namespace StarWars.Api.Infra.Connectors
{
    public class FilmConnector: IFilmConnector
    {
        private readonly HttpClient _client;

        public FilmConnector(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async ValueTask<IEnumerable<Film>> GetFilmsByUrls(IEnumerable<string> urlsFilms)
        {
            try
            {
                var films = new List<Film>();
                foreach (var endpoint in urlsFilms)
                {
                    var response = await _client.GetFromJsonAsync<Film>(endpoint);
                    films.Add(response);
                }
                return films;
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Erro ao buscar calendario no serviço {Endpoint} : {Messages}", endpoint, ex.Message);
                throw;
            }
        }
        
        public async ValueTask<Film> GetById(int id)
        {
            var endpoint = $"https://swapi.dev/api/films/{id}/";

            try
            {
                var response = await _client.GetFromJsonAsync<Film>(endpoint);
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