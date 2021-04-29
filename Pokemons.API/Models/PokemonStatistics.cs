using System;
using System.Collections.Generic;

namespace Pokemons.API.Models
{
    public class PokemonStatisticsList
    {
        public List<string> Names {get; set;}
        public Dictionary<string, PokemonStatistics> Statistics {get; set;}
    }

    public class PokemonStatistics
    {
        public double Minimum {get; set;}
        public double Average {get; set;}
        public double Maximum {get; set;}
    }
}
