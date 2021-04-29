using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Pokemons.API.DTO
{
    // Complete TypeEffectDTO //-------------------------------------------------------------------------------------------------------------------------------
    public class TypeEffectDTO
    {
        public string Defend { get; set; }
        public string Attack { get; set; }
        public decimal Power { get; set; }
    }


    // Split TypeEffectDTO //-------------------------------------------------------------------------------------------------------------------------------
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
