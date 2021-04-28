using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Pokemons.API.Helpers;

namespace Pokemons.API.DTO
{
    public class PokemonDTO : ModelObject
    {
        [Required(ErrorMessage="Pokedex Entry Required")] 
        [Range(1, int.MaxValue, ErrorMessage = "Pokedex Entry Required - Entry must be higher than 0")]
        public int PokedexEntry { get; set; }

        [Required(ErrorMessage="Name Required")] 
        [MinLength(3, ErrorMessage="Name Required - Name is not sufficiently long enough")]
        public string  Name { get; set; }

        [Required(ErrorMessage="Generation Required")] 
        [Range(1, int.MaxValue, ErrorMessage = "Generation Required - Generation must be higher than 0")]
        public int Generation { get; set; }

        [Required(ErrorMessage="List of Pokemon types Required")] 
        [MinLength(1, ErrorMessage="List of Pokemon types Required - Needs to hold at least 1 type")]
        [MaxLength(2, ErrorMessage="List of Pokemon types Required - Cannot hold more than 2 types")]
        public List<string> Types { get; set; }

        public string Classification { get; set; }

        public string EggGroup { get; set; }
    }
}
