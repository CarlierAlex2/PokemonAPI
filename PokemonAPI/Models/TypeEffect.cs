using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class TypeEffect
    {
        public int UserPokemonTypeId { get; set; }
        public int TargetPokemonTypeId { get; set; }
        public PokemonType TargetPokemonType {get; set;}
        public decimal Power { get; set; }
    }

    public class TypeEffectDTO
    {
        public string UserPokemonType { get; set; }
        public string TargetPokemonType { get; set; }
        public decimal Power { get; set; }
    }
}
