using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Pokemons.API.Helpers;

namespace Pokemons.API.DTO
{
    public class TypeEffectDTO : ModelObject
    {
        public string Defend { get; set; }
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }

    public class TypeEffectOffenseDTO : ModelObject
    {
        public string Defend { get; set; }
        public decimal Power { get; set; }
    }

    public class TypeEffectDefenseDTO : ModelObject
    {
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }
}
