using System;
using AutoMapper;

using PokemonAPI.Models;
using PokemonAPI.Data;
using PokemonAPI.Data.CsvStream;

namespace PokemonAPI.DTO
{
    public class AutoMapping: Profile
    {
        public AutoMapping() {
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<PokemonDTO, Pokemon>();

            CreateMap<PokemonDTO, PokemonData>();
            CreateMap<PokemonData, PokemonDTO>();

            CreateMap<Typing, TypingDTO>();
            CreateMap<TypingDTO, Typing>();
            
            CreateMap<TypeEffect, TypeEffectDTO>();
            CreateMap<TypeEffectDTO, TypeEffect>();
            
            CreateMap<TypeEffect, TypeEffectDefenseDTO>();
            CreateMap<TypeEffect, TypeEffectOffenseDTO>();
        }
    }
}
