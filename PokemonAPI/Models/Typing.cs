using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class Typing
    {
        [Key]
        public int TypingId { get; set; }
        [Required] 
        public string  Name { get; set; }

        public virtual List<TypeEffect> TypeOffense { get; set; }
        public virtual List<TypeEffect> TypeDefense { get; set; }
        public virtual List<PokemonTyping> PokemonTypings { get; set; }
    }
}
