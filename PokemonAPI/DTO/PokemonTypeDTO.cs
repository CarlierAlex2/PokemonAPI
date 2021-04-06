using System;
using System.Collections.Generic;

namespace PokemonAPI.DTO
{
    public class PokemonTypeDTO
    {
        public string  Name { get; set; }

        public List<TypeEffectOffenseDTO> TypeOffense { get; set; }
        public List<TypeEffectDefenseDTO> TypeDefense { get; set; }
    }
}
