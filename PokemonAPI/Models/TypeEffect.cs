using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class TypeEffect
    {
        [Key]
        public int TypeEffectId  { get; set; }

        public int OffensePokemonTypeId { get; set; }
        //[ForeignKey("PokemonTypeId")]
        public virtual PokemonType OffensePokemonType {get; set;}

        public int DefensePokemonTypeId { get; set; }
        //[ForeignKey("TargetPokemonTypeId")]
        public virtual PokemonType DefensePokemonType {get; set;}

        public decimal Power { get; set; }
    }
}
