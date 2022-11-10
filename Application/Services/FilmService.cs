using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWars.Api.Infra.Connectors;
using StarWars.Api.Infra.Repositories;

namespace StarWars.Api.Application.Services
{
    public class FilmService: IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IFilmConnector _filmConnector;
        
        public FilmService(IFilmRepository filmRepository,
                           IFilmConnector filmConnector)
        {
            _filmRepository = filmRepository;
            _filmConnector = filmConnector;
        }

        public async ValueTask SaveFilm(IEnumerable<string> urlsFilms, int planetId)
        {
            var filmsIds = await GetIdsFilms(urlsFilms);
            await _filmRepository.InsertFilmsForPlanet(planetId, filmsIds);
        }
        
        private async ValueTask<IEnumerable<int>> GetIdsFilms(IEnumerable<string> urlsFilms)
        {
            var films = new List<int>();
            foreach (var url in urlsFilms)
            {
                var id = GetFilmIdByUrl(url);
                
                if(! await ThisFilmExist(id))
                    await SaveInternalFilm(id);
                
                films.Add(id);
            }
            return films;
        }

        private async ValueTask<bool> ThisFilmExist(int id)
        {
            var filmInDatabase = await _filmRepository.GetById(id);
            return filmInDatabase is not null;
        }
        private async ValueTask SaveInternalFilm(int id)
        {
            var film = await _filmConnector.GetById(id);
            if (film is not null)
            {
                film.Id = id; 
                await _filmRepository.InsertFilm(film);
            }

            //log warning de filme não localizado na consulta da api publica
            return;
        }

        private int GetFilmIdByUrl(string urlFilm)
        {
            var urlSplited = urlFilm.Split("/");

            if (urlSplited.Last() == String.Empty)
                urlSplited = urlSplited.Take(urlSplited.Count() - 1).ToArray();
                
            return int.Parse(urlSplited.LastOrDefault());
        }
    }
}