using System;

namespace Pokemons.API.Data.CsvStream.CsvData
{
    public class PokemonData : CsvDataObject
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        public int PokedexEntry { get; set; }
        public string Name { get; set; }
        public int Generation { get; set; }
        public string Types { get; set; }
        public string Classification { get; set; }
        public string EggGroup { get; set; }
        

        // Stats //-------------------------------------------------------------------------------------------------------------------------------
        public int Hp  { get; set; } = 1;
        public int Attack  { get; set; } = 1;
        public int Defense  { get; set; } = 1;
        public int SpAtk  { get; set; } = 1;
        public int SpDef  { get; set; } = 1;
        public int Speed  { get; set; } = 1;
    }
}
