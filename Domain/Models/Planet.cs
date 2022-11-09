using System.Collections.Generic;

namespace StarWars.Api.Domain.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrains { get; set; }
        public List<Film> Films { get; set; }
    }
}