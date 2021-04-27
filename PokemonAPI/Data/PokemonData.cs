using System;

namespace PokemonAPI.Data
{
    public class PokemonData
    {
        public int PokedexEntry { get; set; }

        public string Name { get; set; }

        public int Generation { get; set; }
        public string Types { get; set; }

        public string Classification { get; set; }

        public string EggGroup { get; set; }
    }
}
