using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Pokemons.API.Models
{
    public class Pokemon
    {
        [Key]
        public Guid PokemonId { get; set; }

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
        public List<PokemonTyping> PokemonTypings { get; set; }

        public string Classification { get; set; }

        public string EggGroup { get; set; }

        [Range(1, int.MaxValue, ErrorMessage="Stats Hp - Cannot be 0 or lower")]
        public int Hp  { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage="Stats Attack - Cannot be 0 or lower")]
        public int Attack  { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage="Stats Defense - Cannot be 0 or lower")]
        public int Defense  { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage="Stats SpAtk - Cannot be 0 or lower")]
        public int SpAtk  { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage="Stats SpDef - Cannot be 0 or lower")]
        public int SpDef  { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage="Stats Speed - Cannot be 0 or lower")]
        public int Speed  { get; set; } = 1;
    }

    public class PokemonList
    {
        [Required]
        public List<string>  Names { get; set; }
    }
}
