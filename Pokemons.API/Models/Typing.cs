using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Pokemons.API.Helpers;

namespace Pokemons.API.Models
{
    public class Typing : ModelObject
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
