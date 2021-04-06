using System;
using AutoMapper;

using PokemonAPI.Models;

namespace PokemonAPI.DTO
{
    public class AutoMapping: Profile
    {
        public AutoMapping() {
            CreateMap<PokemonType, PokemonTypeDTO>();
            
            CreateMap<TypeEffect, TypeEffectDTO>();
            CreateMap<TypeEffect, TypeEffectDefenseDTO>();
            CreateMap<TypeEffect, TypeEffectOffenseDTO>();
        }
    }
}
