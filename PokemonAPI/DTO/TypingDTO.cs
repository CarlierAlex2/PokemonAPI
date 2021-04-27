using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PokemonAPI.Models;
using PokemonAPI.Helpers;

namespace PokemonAPI.DTO
{
    public class TypingDTO : ModelObject
    {
        [Required]
        public string  Name { get; set; }

        public List<TypeEffectOffenseDTO> TypeOffense { get; set; }
        public List<TypeEffectDefenseDTO> TypeDefense { get; set; }
    }
}
