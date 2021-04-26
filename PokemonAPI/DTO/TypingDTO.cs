using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.DTO
{
    public class TypingDTO
    {
        [Required]
        public string  Name { get; set; }

        public List<TypeEffectOffenseDTO> TypeOffense { get; set; }
        public List<TypeEffectDefenseDTO> TypeDefense { get; set; }
    }
}
