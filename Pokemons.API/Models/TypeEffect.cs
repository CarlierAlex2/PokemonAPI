using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Pokemons.API.Models
{
    public class TypeEffect
    {
        [Key]
        public int TypeEffectId  { get; set; }

        public int OffenseTypingId { get; set; }
        public virtual Typing OffenseTyping {get; set;}

        public int DefenseTypingId { get; set; }
        public virtual Typing DefenseTyping {get; set;}

        public decimal Power { get; set; }
    }
}
