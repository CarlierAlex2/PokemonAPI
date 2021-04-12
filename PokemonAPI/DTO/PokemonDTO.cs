using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.DTO
{
    public class PokemonDTO
    {
        [Required] 
        public int PokedexEntry { get; set; }
        [Required] 
        public string  Name { get; set; }
        [Required] 
        public int Generation { get; set; }
        [Required] 
        //[Range(1,2)]
        public List<string> Types { get; set; }
        public string Classification { get; set; }
        public string EggGroup { get; set; }
    }
}
