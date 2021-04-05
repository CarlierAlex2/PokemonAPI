using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class TypeEffect
    {
        public int PokemonTypeId { get; set; }
        public int TargetPokemonTypeId { get; set; }
        [JsonIgnore]
        public PokemonType TargetPokemonType {get; set;}
        public decimal Power { get; set; }
    }

    public class TypeEffectDTO
    {
        public string PokemonType { get; set; }
        public string TargetPokemonType { get; set; }
        public decimal Power { get; set; }
    }
}
