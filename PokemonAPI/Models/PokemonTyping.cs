using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models
{
    public class PokemonTyping
    {
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int TypingId {get; set;}
        public Typing Typing {get; set;}
    }
}
