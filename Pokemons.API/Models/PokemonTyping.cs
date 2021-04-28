using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Pokemons.API.Helpers;

namespace Pokemons.API.Models
{
    public class PokemonTyping : ModelObject
    {
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int TypingId {get; set;}
        public Typing Typing {get; set;}
    }
}
