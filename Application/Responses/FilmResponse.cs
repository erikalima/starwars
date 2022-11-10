using System;
using System.Collections;
using System.Collections.Generic;
using StarWars.Api.Domain.Models;

namespace StarWars.Api.Application.Responses
{
    public class FilmResponse
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public static class FilmResponseAdapter
    {
        public static FilmResponse ToResponse(this Film film)
        {
            return new FilmResponse
            {
                Title = film.Title,
                Director = film.Director,
                ReleaseDate = film.ReleaseDate
            };
        }

        public static IEnumerable<FilmResponse> ToResponse(this IEnumerable<Film> films)
        {
            var filmsResponse = new List<FilmResponse>();
            foreach (var film in films)
            {
                filmsResponse.Add(ToResponse(film));
            }

            return filmsResponse;
        }
    }
}