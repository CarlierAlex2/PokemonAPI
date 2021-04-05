using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class TypeEffectiveness
    {
        [Key]
        public int TypeEffectivenessId { get; set; }
        public int UserTypeId { get; set; }
        public int TargetTypeId { get; set; }
        public int Power { get; set; }
    }
}
