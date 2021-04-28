using System;
using AutoMapper;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream;

namespace Pokemons.API.DTO
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
