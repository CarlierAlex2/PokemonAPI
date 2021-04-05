using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class PokemonType
    {
        [Key]
        public int PokemonTypeId { get; set; }
        [Required] 
        public string  Name { get; set; }
        public List<TypeEffectiveness> TypeEffectiveness { get; set; }
    }
}
