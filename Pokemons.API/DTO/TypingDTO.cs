using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Pokemons.API.Helpers;

namespace Pokemons.API.DTO
{
    public class TypingDTO : ModelObject
    {
        [Required]
        public string  Name { get; set; }

        public List<TypeEffectOffenseDTO> TypeOffense { get; set; }
        public List<TypeEffectDefenseDTO> TypeDefense { get; set; }
    }
}
