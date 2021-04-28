using System;

namespace Pokemons.API.Data.CsvStream.CsvData
{
    public class TypeEffectData : CsvDataObject
    {
        public string Defend { get; set; }
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }
}
