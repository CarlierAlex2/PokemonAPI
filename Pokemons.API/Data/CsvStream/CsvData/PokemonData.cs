using System;

namespace Pokemons.API.Data.CsvStream.CsvData
{
    public class PokemonData : CsvDataObject
    {
        public int PokedexEntry { get; set; }

        public string Name { get; set; }

        public int Generation { get; set; }
        public string Types { get; set; }

        public string Classification { get; set; }

        public string EggGroup { get; set; }
    }
}
