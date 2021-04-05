using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class TypeEffect
    {
        public int PokemonTypeId { get; set; }
        //[ForeignKey("TargetPokemonType")]
        public int TargetPokemonTypeId { get; set; }
        //[ForeignKey("TargetPokemonTypeId")]
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
