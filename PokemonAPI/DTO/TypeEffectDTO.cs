using System;

namespace PokemonAPI.DTO
{
    public class TypeEffectDTO
    {
        public string Defend { get; set; }
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }

    public class TypeEffectOffenseDTO
    {
        public string Defend { get; set; }
        public decimal Power { get; set; }
    }

    public class TypeEffectDefenseDTO
    {
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }
}
