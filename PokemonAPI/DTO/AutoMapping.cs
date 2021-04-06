using System;
using AutoMapper;

using PokemonAPI.Models;

namespace PokemonAPI.DTO
{
    public class AutoMapping: Profile
    {
        public AutoMapping() {
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<PokemonDTO, Pokemon>();

            CreateMap<Typing, TypingDTO>();
            CreateMap<TypingDTO, Typing>();
            
            CreateMap<TypeEffect, TypeEffectDTO>();
            CreateMap<TypeEffect, TypeEffectDefenseDTO>();
            CreateMap<TypeEffect, TypeEffectOffenseDTO>();
        }
    }
}
