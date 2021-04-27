using System;
using System.ComponentModel.DataAnnotations;
using PokemonAPI.Helpers;

namespace PokemonAPI.Models
{
    public class PokemonTyping : ModelObject
    {
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int TypingId {get; set;}
        public Typing Typing {get; set;}
    }
}
