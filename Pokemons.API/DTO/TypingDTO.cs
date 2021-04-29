using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Pokemons.API.DTO
{
    // Basic DTO //-------------------------------------------------------------------------------------------------------------------------------
    public class TypingBaseDTO
    {
        public int TypingId { get; set; }
        public string  Name { get; set; }
    }


    // Full DTO //-------------------------------------------------------------------------------------------------------------------------------
    public class TypingDTO
    {
        public int TypingId { get; set; }
        public string  Name { get; set; }

        public List<TypeEffectOffenseDTO> TypeOffense { get; set; }
        public List<TypeEffectDefenseDTO> TypeDefense { get; set; }
    }
}
