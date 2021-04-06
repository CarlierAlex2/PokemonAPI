using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }
        [Required] 
        public int PokedexEntry { get; set; }
        [Required] 
        public string  Name { get; set; }
        [Required] 
        public int Generation { get; set; }
        //[Required] 
        //[Range(1,2)]
        public List<PokemonTyping> PokemonTypings { get; set; }
        public string Classification { get; set; }
        public string EggGroup { get; set; }
    }
}
