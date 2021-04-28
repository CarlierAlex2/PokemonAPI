using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Pokemons.API.Helpers;

namespace Pokemons.API.Models
{
    public class TypeEffect : ModelObject
    {
        [Key]
        public int TypeEffectId  { get; set; }

        public int OffenseTypingId { get; set; }
        //[ForeignKey("TypingId")]
        public virtual Typing OffenseTyping {get; set;}

        public int DefenseTypingId { get; set; }
        //[ForeignKey("TargetTypingId")]
        public virtual Typing DefenseTyping {get; set;}

        public decimal Power { get; set; }
    }
}
